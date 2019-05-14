namespace GRAPHQL_.NET_API.GrapthQl
{
	// ReSharper disable once InconsistentNaming

	public class GraphQLQuery
	{
		public string OperationName { get; set; }

		public string NamedQuery { get; set; }

		public string Query { get; set; }

		public Newtonsoft.Json.Linq.JObject Variables { get; set; }
	}
}
