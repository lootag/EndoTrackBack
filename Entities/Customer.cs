namespace Entities
{
    public class Customer
    {
        public Customer(long? id, string customerName)
        {
            this.Id = id;
            this.Name = customerName;
        }

        public long? Id { get; }
        public string Name { get; }
    }
}