using GraphQL;
using Orders.GraprhQlTypes.Order;

namespace Orders.Schema
{
	public class Scheme : GraphQL.Types.Schema
	{
		public Scheme(OrdersQuery query, OrdersMutation mutation, OrdersSubscription subscription, IDependencyResolver resolver) : base(resolver)
		{
			Query = query;
			Mutation = mutation;
			Subscription = subscription;
			DependencyResolver = resolver;
		}
	}
}
