using GraphQL.Types;
using Orders.Models;

namespace Orders.GraprhQlTypes.Order
{
	public class OrderEventType : ObjectGraphType<OrderEvent>
	{
		public OrderEventType()
		{
			Field(e => e.Id);
			Field(e => e.Name);
			Field(e => e.OrderId);
			Field(e => e.TimeStamp);
			Field<OrderStatusesEnum>("status", resolve: context => context.Source.Status);
		}
	}
}
