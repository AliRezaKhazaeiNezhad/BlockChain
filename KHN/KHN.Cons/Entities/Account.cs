namespace KHN.Cons.Entities
{
    public class Account : BaseEntity
    {
        public Account(string address)
        {
            Address = address;
        }

        public string Address { get; }
    }
}
