using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDatalayerProvider
{
    public class EFDatalayerFactory
    {
        public static EFRepositories GEtRepo(string connectionString, RepositoryType repositoryType)
        {
            return new EFRepositories(connectionString, repositoryType);
        }
    }
}
