using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using APIProdutos.Security;
using APIProdutos.Data;
using System.Threading.Tasks;
using APIProdutos.GraphQL.Query;
using GraphQL.Types;
using GraphQL;

namespace APIProdutos.Controllers
{

   
    [Route("graphql")]
    public class GraphQLController : ControllerBase
    {

        private readonly CatalogoDbContext _db;

        public GraphQLController(CatalogoDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Post([FromBody]GraphQLQuery query)
        {
            var inputs = query.Variables.ToInputs();

            var schema = new Schema()
            {
                Query = new EatMoreQuery(_db)
            };

            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
            }).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result.Data);
        }
    }
}