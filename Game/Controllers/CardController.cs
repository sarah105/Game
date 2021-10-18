using AutoMapper;
using Game_DataAccess.Repositories.IRepositories;
using Game_Models.Models.Card;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Controllers
{
    [Route("api/v{version:apiVersion}/cards")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICURT<Card> cardRepo;
        private readonly IMapper mapper;

        public CardController(ICURT<Card> cardRepo, IMapper mapper)
        {
            this.cardRepo = cardRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]

        public IActionResult GetAll()
        {
            List<Card> cards = cardRepo.List().ToList();
            return Ok(cards);
        }

        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]

        public IActionResult GetById(int id)
        {
            Card card = cardRepo.Find(id);
            if (card == null) return NotFound("Not Exist!");
            return Ok(card);
        }

        [HttpPost("add")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]

        public IActionResult Add([FromBody] CardDto cardDto)
        {
            if (cardDto == null || !ModelState.IsValid) return BadRequest();
            //get photo and copy it in my folder
            Card card = mapper.Map<Card>(cardDto);
            Card addedCard = cardRepo.Add(card);
            if (addedCard == null) return BadRequest("Something went wrong!");
            return Ok(addedCard);
        }

        [HttpPatch]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]

        public IActionResult Update([FromBody] Card card)
        {
            if (card == null || !ModelState.IsValid) return BadRequest();
            if (cardRepo.Find(card.Id) == null) return NotFound("This card is not exist!");
            if (!cardRepo.Update(card)) return BadRequest("Something went wrong!");
            return NoContent();
        }

        [HttpDelete]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]

        public IActionResult Delete([FromBody] Card card)
        {
            if (card == null || !ModelState.IsValid) return BadRequest();
            //remove photo if it is changed then get photo and copy it in my folder 
            //Card cardUpdate = cardRepo.Find(id);
            if (cardRepo.Find(card.Id) == null) return NotFound("This card is not exist!");
            bool isDeleted = cardRepo.Delete(card);
            if (!isDeleted) return BadRequest("Something went wrong!");
            return Ok();
        }
    }
}
