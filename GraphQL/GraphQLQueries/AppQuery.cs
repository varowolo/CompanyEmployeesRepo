using CompanyEmployees.GraphQL.GraphQLTypes;
using Contracts;
using Entities.Models;
using GraphQL.Types;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees.GraphQL.GraphQLQueries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(ICompanyRepository repository)
    {
            Field<ListGraphType<CompanyType>>(
               "companies",

               resolve: repositoryContext => repository.GetAllCompanies()
           ); ;
    }
    }
}
