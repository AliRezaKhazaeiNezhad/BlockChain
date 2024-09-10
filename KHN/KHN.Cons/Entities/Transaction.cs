namespace KHN.Cons.Entities
{
    public class Transaction : BaseEntity
    {
        public Transaction(
            TransactionType type,
            int count,
            string? senderAddress = null,
            string? receptionAddress = null)
        {
            Amout = count;
            SenderAddress = senderAddress;
            ReceptionAddress = receptionAddress;
            switch (type)
            {
                case TransactionType.Minig:
                case TransactionType.Charging:
                    {
                        senderAddress = null;
                        if (receptionAddress == null)
                        {
                            throw new ArgumentNullException(nameof(receptionAddress));
                        }
                        break;
                    }
                case TransactionType.Withdrawing:
                    {
                        receptionAddress = null;
                        if (senderAddress == null)
                        {
                            throw new ArgumentNullException(nameof(senderAddress));
                        }
                        break;
                    }
                case TransactionType.Transfering:
                    {
                        if (receptionAddress == null)
                        {
                            throw new ArgumentNullException(nameof(receptionAddress));
                        }

                        if (senderAddress == null)
                        {
                            throw new ArgumentNullException(nameof(senderAddress));
                        }
                        break;
                    }

            }
        }


        public string? SenderAddress { get; }
        public string? ReceptionAddress { get; }
        public TransactionType Type { get; }

        public int Amout { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
