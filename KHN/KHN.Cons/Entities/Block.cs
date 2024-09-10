
using System.Text;
using KHN.Cons.Infrastructure;

namespace KHN.Cons.Entities
{
    public class Block : BaseEntity
    {
        public Block(int blockNumber, int dificulty = 0, string? parentHash = null)
        {
            Dificulty = dificulty;
            ParentHash = parentHash;
            BlockNumber = blockNumber;
            _transactions = new List<Transaction>();
        }


        public int BlockNumber { get; }
        public string? ParentHash { get; }
        public string? MixHash { get; protected set; }
        public DateTime? Timestamp { get; protected set; }

        private readonly List<Transaction> _transactions;
        public IReadOnlyList<Transaction> Transactions { get
            {
                return _transactions;
            }
        }


        public int Dificulty { get; }
        public int Nonce { get; protected set; }
        public TimeSpan? Duration { get; protected set; }


        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public bool IsMined()
        {
            if (string.IsNullOrEmpty(MixHash))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Mine()
        {
            if (IsMined())
            {
                return;
            }


            Timestamp = Utility.Now;

            var leadingZeros = new string('0', Dificulty);

            MixHash = CalculateMixHash();


            var startTime = Utility.Now;

            Nonce = -1;
            string mixhash;

            do
            {
                Nonce++;

                mixhash = CalculateMixHash();   
                
            } while (mixhash.StartsWith(leadingZeros) == false);


            var finishTime = Utility.Now;

            Duration = finishTime - startTime;


            MixHash = mixhash;
        }


        public string CalculateMixHash()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"{nameof(Nonce)}:{Nonce}");
            stringBuilder.Append($"|");
            stringBuilder.Append($"{nameof(Dificulty)}:{Dificulty}");
            stringBuilder.Append($"|");
            stringBuilder.Append($"{nameof(Timestamp)}:{Timestamp}");
            stringBuilder.Append($"|");
            stringBuilder.Append($"{nameof(ParentHash)}:{ParentHash}");
            stringBuilder.Append($"|");
            stringBuilder.Append($"{nameof(BlockNumber)}:{BlockNumber}");
            stringBuilder.Append($"|");

            var transactionsString = Utility.ConvertObjectToJson(Transactions);
            stringBuilder.Append($"|");
            stringBuilder.Append($"{nameof(Transaction)}:{transactionsString}");

            var text = stringBuilder.ToString();

            var result = Utility.GetSha256(text);

            return result;
        }
    }
}
