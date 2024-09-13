
using KHN.Cons.Infrastructure;

namespace KHN.Cons.Entities
{
    public class Contract : BaseEntity
    {
        public Contract(
            int initialDificulty = 0,
            double currentMiningReward = 0,
            double currentMinimumTransactionFee = 0)
        {
            _blocks = new List<Block>();
            CurrentDificulty = initialDificulty;
            _pendingTransactions = new List<Transaction>();
            CurrentMiningReward = currentMiningReward;
            CurrentMinimumTransactionFee = currentMinimumTransactionFee;
        }

        public int CurrentDificulty { get; set; }
        public double CurrentMiningReward { get; set; }
        public double CurrentMinimumTransactionFee { get; set; }
        private readonly List<Block> _blocks;
        public IReadOnlyList<Block> Blocks
        {
            get
            {
                return _blocks.AsReadOnly();
            }
        }
        private List<Transaction> _pendingTransactions;
        public IReadOnlyList<Transaction> PendingTransactions
        {
            get
            {
                return _pendingTransactions;
            }
        }





        public bool AddTransaction(Transaction transaction)
        {
            if (transaction.Fee < CurrentMinimumTransactionFee)
            {
                return false;
            }

            switch (transaction.Type)
            {
                case TransactionType.Withdrawing:
                case TransactionType.Transfering:

                    double senderBalance = GetAccountBalance(transaction.SenderAddress);

                    if (senderBalance < transaction.Amout)
                    {
                        return false;
                    }

                    break;
            }

            _pendingTransactions.Add(transaction);

            return true;
        }

        private Block GetNewBlock()
        {
            Block? parentBlock = null;
            int blockNumber = Blocks.Count;

            if (blockNumber != 0)
            {
                parentBlock = Blocks[blockNumber - 1];
            }

            var newBlock = new Block(blockNumber, CurrentDificulty, parentBlock?.MixHash);

            return newBlock;
        }


        public Block Mine(string accountAddress)
        {
            var block = GetNewBlock();

            double totalTransactionFee = 0;

            foreach (Transaction transaction in PendingTransactions)
            {
                block.AddTransaction(transaction);
                totalTransactionFee += transaction.Fee; 
            }

            double totalAmountForMiner = CurrentMiningReward + totalTransactionFee;

            var minerTransaction = new Transaction(
                TransactionType.Minig,
                0,
                totalAmountForMiner,
                null,
                accountAddress
                );

            block.AddTransaction(minerTransaction);

            block.Mine();

            _blocks.Add(block); 

            _pendingTransactions = new List<Transaction>();

            return block;   
        }


        public bool IsValid()
        {
            for (int index = 1; index <= Blocks.Count - 1; index++)
            {
                var currentBlock = Blocks[index];
                var parentBlock = Blocks[index - 1];

                var currentMixHash = currentBlock.CalculateMixHash();

                if (currentBlock.MixHash != currentMixHash)
                {
                    return false;
                }

                if (currentBlock.ParentHash != parentBlock.MixHash)
                {
                    return false;
                }
            }

            return true;
        }


        public double GetAccountBalance(string accountAddress)
        {
            if (IsValid() == false)
            {
                return 0;
            }

            double balance = 0;

            foreach (var block in _blocks)
            {
                foreach (var transaction in block.Transactions)
                {
                    if (transaction.ReceptionAddress == accountAddress)
                    {
                        balance += transaction.Amout;
                    }

                    if (transaction.SenderAddress == accountAddress)
                    {
                        balance -= transaction.Amout;
                    }
                }
            }

            foreach (var transaction in _pendingTransactions)
            {
                if (transaction.SenderAddress == accountAddress)
                {
                    balance -= transaction.Amout;
                }
            }

            return balance;
        }

        public override string ToString()
        {
            return Utility.ConvertObjectToJson(this);
        }
    }
}
