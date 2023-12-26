using Microsoft.EntityFrameworkCore;
using Restaurant.BL.Interfaces;
using Restaurant.BL.Models;
using Restaurant.EF.Exceptions;
using Restaurant.EF.Mappers;
using Restaurant.EF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.EF.Repositories
{
    public class RepoGebruikerEF : IGebruikerRepo
    {
        private readonly RestaurantContext ctx;

        public RepoGebruikerEF(string connectionString)
        {
            this.ctx = new RestaurantContext(connectionString);
        }

        private void SaveAndClear()
        {
            ctx.SaveChanges();
            ctx.ChangeTracker.Clear();
        }

        public Gebruiker AddGebruiker(Gebruiker gebruiker)
        {
            try
            {
                GebruikerEF gebruikerEF = MapGebruikerEF.MapToDB(0, gebruiker, ctx);
                ctx.Gebruiker.Add(gebruikerEF);
                SaveAndClear();
                return gebruiker;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("AddGebruiker", ex);
            }
        }

        public void DeleteGebruiker(string email)
        {
            try
            {
                var gebruikerToDelete = ctx.Gebruiker.SingleOrDefault(x => x.Email == email);

                if (gebruikerToDelete != null)
                {
                    ctx.Gebruiker.Remove(gebruikerToDelete);
                    SaveAndClear();
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException("DeleteGebruiker", ex);
            }
        }

        public bool ExistsGebruiker(string email)
        {
            try
            {
                return ctx.Gebruiker.Any(x => x.Email == email);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GebruikerExists", ex);
            }
        }

        public Gebruiker GetGebruikerByEmail(string email)
        {
            try
            {
                return ctx.Gebruiker.Where(x => x.Email == email).Include(x => x.Reservaties).ThenInclude(x => x.Restaurant).Select(x => MapGebruikerEF.MapToDomain(x)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetGebruikers", ex);
            }
        }

        public void UpdateGebruiker(int klantenNr, Gebruiker gebruiker)
        {
            try
            {
                GebruikerEF gEF = ctx.Gebruiker.Find(klantenNr);

                if (gEF == null)
                {
                    throw new RepositoryException($"Gebruiker met ID {klantenNr} is niet gevonden!");
                }
                else
                {
                    gEF = MapGebruikerEF.MapToDB(klantenNr, gebruiker, ctx);
                }

                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateGebruiker", ex);
            }
        }
    }
}
