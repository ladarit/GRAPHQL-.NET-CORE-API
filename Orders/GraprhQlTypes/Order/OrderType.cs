using GraphQL.Types;
using Orders.Schema;
using Orders.Services;

namespace Orders.GraprhQlTypes.Order
{
	public class OrderType : ObjectGraphType<Models.Order>
	{
		public OrderType(ICustomerService customers)
		{
			Field(o => o.Id);
			Field(o => o.Name);
			Field(o => o.Description);
			Field<CustomerType>("customer", resolve: context => customers.GetCustomerByIdAsync(context.Source.CustomerId));
			Field(o => o.Created);
			Field<OrderStatusesEnum>("status", resolve: context => context.Source.Status);
		}
	}
}
