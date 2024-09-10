
using KHN.Cons.Infrastructure;

namespace KHN.Cons.Entities
{
    public class Contract : BaseEntity
    {
        public Contract(int initialDificulty = 0)
        {
            _blocks = new List<Block>();
            CurrentDificulty = initialDificulty;
            _pendingTransactions = new List<Transaction>();
        }

        public int CurrentDificulty { get; set; }

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



        public void AddTransactionAndMineBlock(Transaction transaction)
        {
            switch (transaction.Type)
            {
                case TransactionType.Withdrawing:
                case TransactionType.Transfering:

                    int senderBalance = GetAccountBalance(transaction.SenderAddress);

                    if (senderBalance < transaction.Amout)
                    {
                        return;
                    }

                    break;
            }

            _pendingTransactions.Add(transaction);

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


        public Block? Mine()
        {
            var block = GetNewBlock();

            foreach (Transaction transaction in PendingTransactions)
            {
                block.AddTransaction(transaction);
            }

            _pendingTransactions = new List<Transaction>();

            block.Mine();

            _blocks.Add(block);

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


        public int GetAccountBalance(string accountAddress)
        {
            if (IsValid() == false)
            {
                return 0;
            }

            int balance = 0;

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
