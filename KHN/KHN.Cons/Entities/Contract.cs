using System.Security;

namespace KHN.Cons.Entities
{
    public class Contract : BaseEntity
    {
        public Contract(int initialDificulty = 0)
        {

            _blocks = new List<Block>();
            CurrentDificulty = initialDificulty;    
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



        public void AddTransactionAndMineBlock(Transaction transaction)
        {
            Block? parentBlock = null;
            int blockNumber = Blocks.Count;

            if (blockNumber != 0)
            {
                parentBlock = Blocks[blockNumber - 1];
            }

            var newBlock = new Block(blockNumber, transaction, parentBlock?.MixHash, CurrentDificulty);
            newBlock.Mine();

            _blocks.Add(newBlock);
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

            foreach (var block in Blocks)
            {
                if (block.Transaction.ReceptionAddress == accountAddress)
                {
                    balance += block.Transaction.Amout;
                }

                if (block.Transaction.SenderAddress == accountAddress)
                {
                    balance -= block.Transaction.Amout;
                }
            }

            return balance;
        }
    }
}
