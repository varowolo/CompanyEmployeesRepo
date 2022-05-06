using CompanyEmployees.GraphQL.GraphQLQueries;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees.GraphQL.GraphQLSchema
{
    public class AppSchema : Schema
    {

        public AppSchema(IServiceProvider provider)
       : base(provider)
        {
            Query = provider.GetRequiredService<AppQuery>();
        }
    }
}
