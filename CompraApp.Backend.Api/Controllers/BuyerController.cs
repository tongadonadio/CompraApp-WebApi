using CompraApp.Backend.Domain;
using CompraApp.Backend.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CompraApp.Backend.Api.Controllers
{
    public class BuyerController : ApiController
    {
        private readonly BuyerService buyerService;

        public BuyerController()
        {
            buyerService = new BuyerService();
        }

        // GET: api/Buyer
        public IHttpActionResult Get()
        {
            return Ok(buyerService.GetAll());
        }

        // GET: api/Buyer/5
        public IHttpActionResult Get(int id)
        {
            return Ok(buyerService.Get(id));
        }

        [HttpGet]
        [Route("api/Buyer/GetByEmail/{email}/")]
        public IHttpActionResult Get(string email)
        {
            return Ok(buyerService.Get(email));
        }

        // POST: api/Buyer
        public IHttpActionResult Post(Buyer newBuyer)
        {
            if (ModelState.IsValid)
            {
                return Ok(buyerService.Add(newBuyer));
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/Buyer/5
        public IHttpActionResult Put(int id, Buyer buyer)
        {
            if (ModelState.IsValid)
            {
                return Ok(buyerService.Edit(id, buyer));
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Buyer/5
        public bool Delete(int id)
        {
            return buyerService.Delete(id);
        }
    }
}
