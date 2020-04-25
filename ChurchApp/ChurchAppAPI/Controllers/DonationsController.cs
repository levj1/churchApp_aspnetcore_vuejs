using AutoMapper;
using ChurchAppAPI.Entities;
using ChurchAppAPI.Models;
using ChurchAppAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChurchAppAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [ApiController]
    [Route("api/donations")]
    public class DonationsController: ControllerBase
    {
        private IDonationRepository _donationRepository;
        private IMapper _mapper;
        public DonationsController(IDonationRepository donationRepository,
            IMapper mapper)
        {
            _donationRepository = donationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetDonations()
        {
            var donationsEntity = _donationRepository.GetAll();
            var donationDtos = _mapper.Map<List<DonationDto>>(donationsEntity);

            return Ok(donationDtos);
            
        }

        [HttpGet("{id}")]
        public IActionResult GetDonation(int id)
        {
            var donationEntity = _donationRepository.Get(id);
            if (donationEntity == null) return NotFound();

            var donationDto = _mapper.Map<DonationDto>(donationEntity);

            return Ok(donationDto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] DonationCreateDto donation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var donationEntity = _mapper.Map<Donation>(donation);

            _donationRepository.Create(donationEntity);
            if (!_donationRepository.Save())
            {
                return StatusCode(500, "An error occured while processing your request");
            }

            var donationCreated = _mapper.Map<DonationDto>(donationEntity);

            return CreatedAtAction("Post", new { id = donationEntity.ID }, donationCreated);
        }

        [HttpPost("CreateDonations")]
        public IActionResult Post([FromBody] ICollection<DonationCreateDto> donations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var donationEntities = _mapper.Map<List<Donation>>(donations);

            _donationRepository.Create(donationEntities);
            if (!_donationRepository.Save())
            {
                return StatusCode(500, "An error occured while processing your request");
            }

            var donationCreated = _mapper.Map<List<DonationDto>>(donationEntities);

            return CreatedAtAction("Post", donationCreated);
        }

        [HttpPut]
        public IActionResult Put([FromBody] DonationToUpdate donation)
        {
            var donationInDb = _donationRepository.Get(donation.ID);

            if (donationInDb == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _mapper.Map(donation, donationInDb);
            if (!_donationRepository.Save())
            {
                return StatusCode(500, "An error occured while processing your request");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch([FromBody] JsonPatchDocument<DonationToUpdate> patchDoc, int id)
        {
            if (patchDoc == null) return BadRequest();

            var donationEntity = _donationRepository.Get(id);

            if (donationEntity == null) return NotFound();

            var donationToPatch = _mapper.Map<DonationToUpdate>(donationEntity);

            patchDoc.ApplyTo(donationToPatch, ModelState);
            TryValidateModel(donationToPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(donationToPatch, donationEntity);

            if (!_donationRepository.Save())
            {
                return StatusCode(500, "Something went wrong.");
            }

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var donationInDb = _donationRepository.Get(id);

            if (donationInDb == null) return NotFound();

            _donationRepository.Delete(donationInDb);

            if (!_donationRepository.Save())
            {
                return StatusCode(500, "An error occured while processing your request");
            }

            return NoContent();
        }

        [HttpGet("reports")]
        public IActionResult GetReport(DateTime fromDate, DateTime toDate)
        {
            var donations = _donationRepository.GetAll().Where(d => d.DonationDate >= fromDate.Date
            && d.DonationDate < toDate.AddDays(1).Date);
     
            return Ok();
        }
    }
}
