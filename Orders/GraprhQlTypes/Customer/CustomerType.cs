using GraphQL.Types;

namespace Orders.GraprhQlTypes.Customer
{
    public class CustomerType : ObjectGraphType<Models.Customer>
    {
        public CustomerType()
        {
            Field(c => c.Id);
            Field(c => c.Name);
        }
    }
}
