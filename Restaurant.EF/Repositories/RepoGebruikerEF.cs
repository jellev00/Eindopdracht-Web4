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
                GebruikerEF gebruikerEF = MapGebruikerEF.MapToDB(gebruiker, ctx);
                ctx.Gebruiker.Add(gebruikerEF);
                SaveAndClear();
                return gebruiker;
            } catch (Exception ex)
            {
                throw new RepositoryException("AddGebruiker", ex);
            }
        }

        public void DeleteGebruiker(Gebruiker gebruiker)
        {
            try
            {
                ctx.Gebruiker.Remove(new GebruikerEF() { KlantenNummer = gebruiker.KlantenNr});
                SaveAndClear();

            } catch (Exception ex)
            {
                throw new RepositoryException("DeleteGebruiker", ex);
            }
        }

        public bool GebruikerExists(string email)
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

        public List<Gebruiker> GetGebruikers()
        {
            try
            {
                return ctx.Gebruiker.Select(x => MapGebruikerEF.MapToDomain(x)).AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetGebruikers", ex);
            }
        }

        public Gebruiker GetGebruikersByKlantNr(int klantNr)
        {
            try
            {
                return ctx.Gebruiker.Where(x => x.KlantenNummer == klantNr).Select(x => MapGebruikerEF.MapToDomain(x)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetGebruikers", ex);
            }
        }

        public Gebruiker GetGebruikersByEmail(string email)
        {
            try
            {
                return ctx.Gebruiker.Where(x => x.Email == email).Select(x => MapGebruikerEF.MapToDomain(x)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("GetGebruikers", ex);
            }
        }

        public void UpdateGebruiker(Gebruiker gebruiker)
        {
            try
            {
                ctx.Gebruiker.Update(MapGebruikerEF.MapToDB(gebruiker, ctx));
                SaveAndClear();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("UpdateGebruiker", ex);
            }
        }
    }
}
