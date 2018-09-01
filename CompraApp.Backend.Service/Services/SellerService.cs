using CompraApp.Backend.DataAccess;
using CompraApp.Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompraApp.Backend.Service.Services
{
    public class SellerService
    {
        public SellerService() { }
        public IEnumerable<Seller> GetAll()
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                return db.Sellers.ToList();
            }
        }

        public Seller Get(int id)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                return db.Sellers.Find(id);
            }
        }
       
        public Seller Get(string email)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                return db.Sellers.Where(s => s.Email == email).FirstOrDefault();
            }
        }

        public Seller Add(Seller seller)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                db.Sellers.Add(seller);
                db.SaveChanges();
                return seller;
            }
        }

        public Seller Edit(int id, Seller seller)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                Seller sellerForUpdate = db.Sellers.Find(id);
                if (Exists(sellerForUpdate))
                {
                    sellerForUpdate.Address = seller.Address;
                    sellerForUpdate.Email = seller.Email;
                    sellerForUpdate.Name = seller.Name;
                    sellerForUpdate.Latitud = seller.Latitud;
                    sellerForUpdate.Longitud = seller.Longitud;
                    sellerForUpdate.Notifications = seller.Notifications;
                    sellerForUpdate.Phone = seller.Phone;
                    db.Sellers.Attach(sellerForUpdate);
                    db.Entry(sellerForUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }
                
                return sellerForUpdate;
            }
        }

        private bool Exists(Seller seller)
        {
            return seller != null;
        }

        public bool Delete(int id)
        {
            using (CompraAppContext db = new CompraAppContext())
            {
                try
                {
                    Seller seller = db.Sellers.Find(id);
                    db.Sellers.Remove(seller);
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
