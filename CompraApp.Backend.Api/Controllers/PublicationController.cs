using CompraApp.Backend.Domain;
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
    public class PublicationController : ApiController
    {
        private readonly PublicationService publicationService;

        public PublicationController()
        {
            this.publicationService = new PublicationService();
        }

        // GET: api/Publication
        // GET: api/Publication?idBuyer=6
        // GET: api/Publication?status=0    //Publication.STATE.OPEN
        // GET: api/Publication?status=1    //Publication.STATE.CLOSE_BY_ACEPTED_OFFER
        // GET: api/Publication?status=2    //Publication.STATE.CLOSE_BY_USER
        public IHttpActionResult Get(int status = 0, int idBuyer = 0)
        {
            if (idBuyer > 0)
                return Ok(publicationService.GetOfBuyer(idBuyer, status));
            else
            {
                return Ok(publicationService.GetAll(status));
            }
        }


        // POST: api/publication/5?action=2    Publication.CLOSE_BY_USER
        [HttpPost]
        [Route("api/publication/{id}")]
        public IHttpActionResult PostOfferAction(int id, Publication.STATE action)
        {
            try
            {
                return Ok(publicationService.MakeAction(id, action));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Publication/5/Offer
        [HttpGet]
        [Route("api/publication/{id}/offer")] 
        public IHttpActionResult GetOffers(int id)
        {
            return Ok(publicationService.GetOffers(id));
        }

        // GET: api/Publication/5
        public IHttpActionResult Get(int id)
        {
            return Ok(publicationService.Get(id));
        }

        // POST: api/Publication
        public IHttpActionResult Post(Publication newPublication)
        {
            if (ModelState.IsValid)
            {
                return Ok(publicationService.Add(newPublication));
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/Publication/5
        public IHttpActionResult Put(int id, Publication publication)
        {
            if (ModelState.IsValid)
            {
                return Ok(publicationService.Edit(id, publication));
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Publication/5
        public bool Delete(int id)
        {
            return publicationService.Delete(id);
        }

        [HttpPost]
        [Route("api/test")]
        public IHttpActionResult Test()
        {
            try
            {
                Buyer buyer1 = new Buyer();
                buyer1.Email = "pabluc2016@gmail.com";
                buyer1.Name = "Pablo";
                buyer1.Phone = "123456789";
                Buyer buyer2 = new Buyer();
                buyer2.Email = "pabluc@gmail.com";
                buyer2.Name = "Pablo U.";
                buyer2.Phone = "897654123";
                Buyer buyer3 = new Buyer();
                buyer3.Email = "Suarez@gmail.com";
                buyer3.Name = "Luis";
                buyer3.Phone = "22233666";
                BuyerService buyerService = new BuyerService();
                buyerService.Add(buyer1);
                buyerService.Add(buyer2);
                buyerService.Add(buyer3);

                Seller seller1 = new Seller();
                seller1.Email = "diego@gmail.com";
                seller1.Name = "Diego";
                seller1.Phone = "456666";
                Seller seller2 = new Seller();
                seller2.Email = "godin@gmail.com";
                seller2.Name = "Godin";
                seller2.Phone = "357159";
                Seller seller3 = new Seller();
                seller3.Email = "carlos@gmail.com";
                seller3.Name = "Carlitos";
                seller3.Phone = "5254";
                SellerService sellerService = new SellerService();
                sellerService.Add(seller1);
                sellerService.Add(seller2);
                sellerService.Add(seller3);


                Publication publication = new Publication();
                publication.IdBuyer = 1;
                publication.Buyer = buyer1;
                publication.Description = "Computador I7";
                publication.DescriptionItem = "Compu I7";
                publication.Price = 1200;
                publication.PriceMinItem = 800;
                publication.PriceMaxItem = 1200;

                publicationService.Add(publication);

                Publication publication2 = new Publication();
                publication2.IdBuyer = 1;
                publication2.Buyer = buyer1;
                publication2.Description = "Computador I5";
                publication2.DescriptionItem = "Compu I5";
                publication2.Price = 1000;
                publication2.PriceMinItem = 500;
                publication2.PriceMaxItem = 1000;

                publicationService.Add(publication2);


                Publication publication3 = new Publication();
                publication3.IdBuyer = 1;
                publication3.Buyer = buyer1;
                publication3.Description = "Computador I3";
                publication3.DescriptionItem = "Compu I3";
                publication3.Price = 800;
                publication3.PriceMinItem = 500;
                publication3.PriceMaxItem = 800;

                publicationService.Add(publication3);

                Offer offer1 = new Offer();
                offer1.IdPublication = 1;
                offer1.IdSeller = 1;
                offer1.DescriptionItem = "Una compu I7";
                offer1.PriceItem = 1100;


                Offer offer2 = new Offer();
                offer2.IdPublication = 1;
                offer2.IdSeller = 2;
                offer2.DescriptionItem = "Una compu I7";
                offer2.PriceItem = 1150;

                Offer offer3 = new Offer();
                offer3.IdPublication = 2;
                offer3.IdSeller = 3;
                offer3.DescriptionItem = "Una compu I5";
                offer3.PriceItem = 900;


                OfferService offerService = new OfferService();
                offerService.Add(offer1);
                offerService.Add(offer2);
                offerService.Add(offer3);


                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}