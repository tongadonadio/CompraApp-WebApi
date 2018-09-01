using CompraApp.Backend.Domain.Entities;
using CompraApp.Backend.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CompraApp.Backend.Api.Controllers
{
    public class SellerController : ApiController
    {
        private readonly SellerService sellerService;

        public SellerController()
        {
            sellerService = new SellerService();
        }

        // GET: api/Seller
        public IHttpActionResult Get()
        {
            return Ok(sellerService.GetAll());
        }

        // GET: api/Seller/5
        public IHttpActionResult Get(int id)
        {
            return Ok(sellerService.Get(id));
        }

        [HttpGet]
        [Route("api/Seller/GetByEmail/{email}/")]
        public IHttpActionResult Get(string email)
        {
            return Ok(sellerService.Get(email));
        }

        // POST: api/Seller
        public IHttpActionResult Post(Seller newSeller)
        {
            if (ModelState.IsValid)
            {
                return Ok(sellerService.Add(newSeller));
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/Seller/5
        public IHttpActionResult Put(int id, Seller seller)
        {
            if (ModelState.IsValid)
            {
                return Ok(sellerService.Edit(id, seller));
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Seller/5
        public bool Delete(int id)
        {
            return sellerService.Delete(id);
        }
    }
}
