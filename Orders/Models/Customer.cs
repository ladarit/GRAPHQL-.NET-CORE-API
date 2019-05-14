namespace Orders.Models
{
    public class Customer
    {
        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; set; }

        public int Id { get; private set; }
    }
}
