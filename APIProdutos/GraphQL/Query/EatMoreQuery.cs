using APIProdutos.Data;
using APIProdutos.Models.Types;
using GraphQL.Types;
using System.Linq;

namespace APIProdutos.GraphQL.Query
{
    public class EatMoreQuery : ObjectGraphType
    {
        public EatMoreQuery(CatalogoDbContext db)
        {

            Field<ListGraphType<ProdutoType>>(
                "produtos",
                resolve: context =>
                {
                    var produtos = db
                    .Produtos;
                    return produtos;
                });

        }
    }
}
