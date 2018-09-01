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
    public class OfferController : ApiController
    {
        private readonly OfferService offerService;

        public OfferController()
        {
            offerService = new OfferService();
        }

        // GET: api/Offer
        public IHttpActionResult Get(int status = 0, int idBuyer = 0)
        {
            if (idBuyer > 0)
                return Ok(offerService.GetToABuyerByStatus(idBuyer, status));
            else
                return Ok(offerService.GetAll(status));
        }

        // GET: api/Offer
        public IHttpActionResult GetBySeller(int idSeller)
        {
            if (idSeller > 0)
                return Ok(offerService.GetBySeller(idSeller));
            else
                return Ok(offerService.GetAll(0));
        }

        // GET: api/Offer/5
        public IHttpActionResult Get(int id)
        {
            return Ok(offerService.Get(id));
        }

        // POST: api/offer/5?action=1    Offer.REJECTED
        // POST: api/offer/5?action=2    Offer.ACCEPTED
        // POST: api/offer/5?action=3    Offer.CANCEL
        [HttpPost]
        [Route("api/offer/{id}")]
        public IHttpActionResult PostOfferAction(int id, Offer.ACTION action)
        {
            try
            {
                return Ok(offerService.MakeAction(id, action));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Offer
        public IHttpActionResult Post(Offer newOffer)
        {
            if (ModelState.IsValid)
            {
                return Ok(offerService.Add(newOffer));
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/Offer/5
        public IHttpActionResult Put(int id, Offer offer)
        {
            if (ModelState.IsValid)
            {
                return Ok(offerService.Edit(id, offer));
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Offer/5
        public bool Delete(int id)
        {
            return offerService.Delete(id);
        }
    }
}
