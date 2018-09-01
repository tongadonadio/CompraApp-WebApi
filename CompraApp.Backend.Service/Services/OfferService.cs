using CompraApp.Backend.DataAccess;
using CompraApp.Backend.Domain;
using CompraApp.Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompraApp.Backend.Service.Services
{
    public class OfferService
    {
        public OfferService() { }
        public IEnumerable<Offer> GetAll()
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                return db.Offers.ToList();
            }
        }

        public Offer Get(int id)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                return db.Offers.Find(id);
            }
        }


        public IEnumerable<Offer> GetAll(int state = (int)Offer.ACTION.OPEN)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                return db.Offers.Where(o => o.State == state).ToList();
            }
        }

        public IEnumerable<Offer> GetBySeller(int idSeller)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                return db.Offers.Where(o => o.IdSeller == idSeller).ToList();
            }
        }

        public IEnumerable<Offer> GetToABuyerByStatus(int idBuyer, int status = 0)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                List<Offer> query = (from o in db.Offers
                                  join p in db.Publications on o.IdPublication equals p.Id
                                  where o.State == status
                                  select o).ToList();


                foreach (Offer o in query)
                {
                    o.Seller = db.Sellers.Find(o.IdSeller);
                }
                return query;
            }
        }

        public Offer Add(Offer offer)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                offer.Publication = db.Publications.Find(offer.IdPublication);
                db.Publications.Attach(offer.Publication);

                offer.Seller = db.Sellers.Find(offer.IdSeller);
                db.Sellers.Attach(offer.Seller);
                
                db.Offers.Add(offer);
                db.SaveChanges();
                return offer;
            }
        }

        public Offer Edit(int id, Offer offer)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                Offer offerForUpdate = db.Offers.Find(id);
                if (Exists(offerForUpdate))
                {
                    offerForUpdate.DeliveryItem = offer.DeliveryItem;
                    offerForUpdate.DeliveryZoneItem = offer.DeliveryZoneItem;
                    offerForUpdate.DescriptionItem = offer.DescriptionItem;
                    offerForUpdate.IdPublication = offer.IdPublication;
                    offerForUpdate.IdSeller = offer.IdSeller;
                    offerForUpdate.PhotoItem = offer.PhotoItem;
                    offerForUpdate.PriceItem = offer.PriceItem;
                    offerForUpdate.State = offer.State;
                    offerForUpdate.StateItem = offer.StateItem;
                    db.Offers.Attach(offerForUpdate);
                    db.Entry(offerForUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return offerForUpdate;
            }
        }

        private bool Exists(Offer offer)
        {
            return offer != null;
        }

        public bool Delete(int id)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                try
                {
                    Offer offer = db.Offers.Find(id);
                    db.Offers.Remove(offer);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }


        public Offer MakeAction(int id, Offer.ACTION action)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                Offer offerForUpdate = db.Offers.Find(id);
                if (Exists(offerForUpdate))
                {
                    if (checkAction(offerForUpdate, action))
                    {
                        if (action == Offer.ACTION.ACCEPTED)
                        {
                            setRejectedToOffersLosers(db, offerForUpdate);
                            closePublicationByAcceptedOffer(db, offerForUpdate);
                        }
                        offerForUpdate.State = (int)action;
                        db.Offers.Attach(offerForUpdate);
                        db.Entry(offerForUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                        return offerForUpdate;
                    }
                    else
                        throw new Exception("Error: Offerta ya procesada como aceptada, cancelada o rechazada.");
                }
                else
                    throw new Exception("Error: No existe la oferta.");
                
            }
        }

        private void closePublicationByAcceptedOffer(CompraAppContext db, Offer offerForUpdate)
        {
            Publication publication = db.Publications.Find(offerForUpdate.IdPublication);
            publication.State = (int)Publication.STATE.CLOSE_BY_ACEPTED_OFFER;
            db.Publications.Attach(publication);
            db.Entry(publication).State = EntityState.Modified;
        }

        private bool checkAction(Offer currentOffer, Offer.ACTION action)
        {
            if (currentOffer.State == (int)Offer.ACTION.OPEN)
                return true;
            else
                return false;
        }

        private void setRejectedToOffersLosers(CompraAppContext db, Offer offerWin)
        {
            List<Offer> offersToClose = db.Offers.Where(o => o.IdPublication == offerWin.IdPublication && o.Id != offerWin.Id).ToList();
            foreach(Offer offer in offersToClose)
            {
                offer.State = (int)Offer.ACTION.REJECTED;
                db.Offers.Attach(offer);
                db.Entry(offer).State = EntityState.Modified;
            }
        }

    }
}
