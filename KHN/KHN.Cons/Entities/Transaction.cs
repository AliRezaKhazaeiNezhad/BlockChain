namespace KHN.Cons.Entities
{
    public class Transaction : BaseEntity
    {
        public Transaction(int count, string senderAddress, string receptionAddress)
        {
            Amout = count;  
            SenderAddress = senderAddress;
            ReceptionAddress = receptionAddress;
        }


        public string SenderAddress { get; }
        public string ReceptionAddress { get; }

        public int Amout { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
