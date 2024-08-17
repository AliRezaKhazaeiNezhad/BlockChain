
using System.Text;
using KHN.Cons.Infrastructure;

namespace KHN.Cons.Entities
{
    public class Block : BaseEntity
    {
        public Block(int blockNumber, Transaction transaction, string? parentHash = null, int dificulty = 0)
        {
            Dificulty = dificulty;
            ParentHash = parentHash;
            Transaction = transaction;
            BlockNumber = blockNumber;
        }


        public int BlockNumber { get; }
        public string? ParentHash { get; }
        public Transaction Transaction { get; }
        public string? MixHash { get; protected set; }
        public DateTime? Timestamp { get; protected set; }



        public int Dificulty { get; }
        public int Nonce { get; protected set; }
        public TimeSpan? Duration { get; protected set; }



        public void Mine()
        {
            if (string.IsNullOrWhiteSpace(MixHash) == false)
            {
                return;
            }


            Timestamp = Utility.Now;

            var leadingZeros = string.Empty.PadLeft(Dificulty, '0');

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
            stringBuilder.Append($"{nameof(Transaction)}:{Transaction}");

            var text = stringBuilder.ToString();

            var result = Utility.GetSha256(text);

            return result;
        }
    }
}
