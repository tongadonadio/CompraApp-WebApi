using CompraApp.Backend.DataAccess;
using CompraApp.Backend.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompraApp.Backend.Service.Services
{
    public class BuyerService
    {
        public BuyerService() { }
        public IEnumerable<Buyer> GetAll()
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                return db.Buyers.ToList();
            }
        }

        public Buyer Get(int id)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                return db.Buyers.Find(id);
            }
        }

        public Buyer Get(string email)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                return db.Buyers.Where(s => s.Email == email).FirstOrDefault();
            }
        }

        public Buyer Add(Buyer buyer)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                db.Buyers.Add(buyer);
                db.SaveChanges();
                return buyer;
            }
        }

        public Buyer Edit(int id, Buyer buyer)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                try
                {
                    Buyer buyerForUpdate = db.Buyers.Find(id);
                    if (Exists(buyerForUpdate))
                    {
                        buyerForUpdate.Address = buyer.Address;
                        buyerForUpdate.Email = buyer.Email;
                        buyerForUpdate.Name = buyer.Name;
                        buyerForUpdate.Notifications = buyer.Notifications;
                        buyerForUpdate.Phone = buyer.Phone;
                        buyerForUpdate.Latitud = buyer.Latitud;
                        buyerForUpdate.Longitud = buyer.Longitud;
                        db.Buyers.Attach(buyerForUpdate);
                        db.Entry(buyerForUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                        return buyerForUpdate;
                    }
                }catch(Exception e){
                    var a = e;
                    
                }
                return null;


            }
        }

        private bool Exists(Buyer buyer)
        {
            return buyer != null;
        }

        public bool Delete(int id)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                try
                {
                    Buyer buyer = db.Buyers.Find(id);
                    db.Buyers.Remove(buyer);
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
