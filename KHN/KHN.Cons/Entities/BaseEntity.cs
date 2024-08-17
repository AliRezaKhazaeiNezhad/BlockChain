
using KHN.Cons.Infrastructure;

namespace KHN.Cons.Entities
{
    public class BaseEntity : object
    {
        public BaseEntity() : base()
        {
                
        }


        public override string ToString()
        {
            return Utility.ConvertObjectToJson(this, true);
        }
    }
}
