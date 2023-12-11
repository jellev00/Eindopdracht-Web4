using EFDatalayerProvider.Exceptions;
using Restaurant.BL.Interfaces;
using Restaurant.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDatalayerProvider
{
    public class EFRepositories
    {
        public EFRepositories(string connectionString, RepositoryType repositoryType)
        {
            try
            {
                switch (repositoryType)
                {
                    case RepositoryType.EFCore:
                        gebruikerRepo = new RepoGebruikerEF(connectionString);
                        beheerderRepo = new RepoRestaurantEF(connectionString);
                        reservatieRepo = new RepoReservatiesEF(connectionString);
                        break;
                    default: throw new EFDatalayerFactoryException("GetRepo");

                }
            }
            catch (Exception ex)
            {
                throw new EFDatalayerFactoryException("GetRepo", ex);
            }
        }

        public IGebruikerRepo gebruikerRepo { get; set; }
        public IRestaurantRepo beheerderRepo { get; set; }
        public IReservatieRepo reservatieRepo { get; set; }
    }
}
