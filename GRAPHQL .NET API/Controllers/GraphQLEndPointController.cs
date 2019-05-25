using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Http;
using GraphQL.Instrumentation;
using GraphQL.Types;
using GraphQL.Validation.Complexity;
using GRAPHQL_.NET_API.GrapthQl;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable InconsistentNaming

namespace GRAPHQL_.NET_API.Controllers
{
	[EnableCors("AllowMyOrigin")]
	[Route("api/[controller]")]
	[ApiController]
	public class GraphQLEndPointController : ControllerBase
	{
		private readonly ISchema _schema;
		private readonly IDocumentExecuter _executer;
		private readonly IDocumentWriter _writer;
		private readonly IDictionary<string, string> _namedQueries;


		public GraphQLEndPointController(IDocumentExecuter executer, IDocumentWriter writer, ISchema schema)
		{
			_executer = executer;
			_writer = writer;
			_schema = schema;
			_namedQueries = new Dictionary<string, string>
			{
				["a-query"] = @"query foo { hero { name } }"
			};
		}

		[Route("getSheme")]
		[HttpPost]
		public async Task<string> Post(GraphQLQuery query)
		{
			var inputs = query.Variables.ToInputs();
			var queryToExecute = query.Query;

			if (!string.IsNullOrWhiteSpace(query.NamedQuery))
			{
				queryToExecute = _namedQueries[query.NamedQuery];
			}

			var result = await _executer.ExecuteAsync(_ =>
			{
				_.Schema = _schema;
				_.Query = queryToExecute;
				_.OperationName = query.OperationName;
				_.Inputs = inputs;
				////_.UserContext = userInfo;
				_.ComplexityConfiguration = new ComplexityConfiguration { MaxDepth = 15 };
				_.FieldMiddleware.Use<InstrumentFieldsMiddleware>();
				////_.UserContext = userInfo;
				////_.Root = new
				////{
				   //// nSession = NHibernateSessionManager.GetInstance().GetSession()
				////};

			}).ConfigureAwait(false);

			var json = _writer.Write(result);
			return json;
		}
	}
}