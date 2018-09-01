using CompraApp.Backend.DataAccess;
using CompraApp.Backend.Domain;
using CompraApp.Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompraApp.Backend.Service.Services
{
    public class PublicationService
    {
        public PublicationService() { }

        public IEnumerable<Publication> GetAll(int state = (int)Publication.STATE.OPEN)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                List<Publication> publications = db.Publications.Where(x => x.State == state).Include(b => b.Buyer).ToList();

                foreach (Publication publication in publications)
                {
                    publication.NameBuyer = publication.Buyer.Name;
                }

                return publications;
            }
        }

        public IEnumerable<Publication> GetOfBuyer(int idBuyer, int status = 0)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                var query =
                from p in db.Publications
                select new
                {
                    Publication = p,
                    OffersCount = db.Offers.Where(o=> o.State == (int)Offer.ACTION.OPEN).Count(o => o.IdPublication == p.Id)
                };

                List<Publication> publications = new List<Publication>();
                foreach(var row in query){
                    Publication p = row.Publication;
                    p.CountOffers = row.OffersCount;
                    if((p.IdBuyer == idBuyer) && (p.State == status))
                        publications.Add(p);
                }

                //return db.Publications.Where(p => p.IdBuyer == idBuyer).ToList();
                return publications;
            }
        }

        public object MakeAction(int id, Publication.STATE action)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                Publication publicationForUpdate = db.Publications.Find(id);
                if (Exists(publicationForUpdate))
                {
                    if (checkAction(publicationForUpdate, action))
                    {
                        if (action == Publication.STATE.CLOSE_BY_USER)
                        {
                            
                        }
                        publicationForUpdate.State = (int)action;
                        db.Publications.Attach(publicationForUpdate);
                        db.Entry(publicationForUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                        return publicationForUpdate;
                    }
                    else
                        throw new Exception("Error: Publicación ya cerrada.");
                }
                else
                    throw new Exception("Error: No existe la publicación.");

            }
        }

        private bool checkAction(Publication currentPublication, Publication.STATE action)
        {
            if (currentPublication.State == (int)Publication.STATE.OPEN)
                return true;
            else
                return false;
        }

        public Publication Get(int id)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                return db.Publications.Find(id);
            }
        }

        public IEnumerable<Offer> GetOffers(int idPublication, int state = 0)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

                var offers = db.Offers.Where(o => o.State == state).Where(o => o.IdPublication == idPublication).ToList();

                foreach(Offer o in offers)
                {
                    o.Seller = db.Sellers.Find(o.IdSeller);
                }

                return offers;
            }
        }

        public Publication Add(Publication publication)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                publication.Buyer = db.Buyers.Find(publication.IdBuyer);
                db.Buyers.Attach(publication.Buyer);
                db.Publications.Add(publication);
                db.SaveChanges();
                return publication;
            }
        }

        public Publication Edit(int id, Publication publication)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                Publication publicationForUpdate = db.Publications.Find(id);
                if (Exists(publicationForUpdate))
                {
                    publicationForUpdate.IdBuyer = publication.IdBuyer;
                    publicationForUpdate.Price = publication.Price;
                    publicationForUpdate.PriceMaxItem = publication.PriceMaxItem;
                    publicationForUpdate.PriceMinItem = publication.PriceMinItem;
                    publicationForUpdate.State = publication.State;
                    publicationForUpdate.StateItem = publication.StateItem;
                    db.Publications.Attach(publicationForUpdate);
                    db.Entry(publicationForUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return publicationForUpdate;
            }
        }

        private bool Exists(Publication publication)
        {
            return publication != null;
        }

        public bool Delete(int id)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                try
                {
                    Publication publication = db.Publications.Find(id);
                    db.Publications.Remove(publication);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
