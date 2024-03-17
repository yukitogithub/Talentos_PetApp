using BusinessAccessLayer.Dto;
using BusinessAccessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet("GetPets")]
        public async Task<ActionResult> GetPets()
        {
            var pets = await _petService.GetPets();
            return Ok(pets);
        }

        [HttpGet("GetPet/{petId}")]
        public async Task<ActionResult> GetPet(int petId)
        {
            var pet = await _petService.GetPet(petId);
            return Ok(pet);
        }

        [HttpPost("AddPet")]
        public async Task<ActionResult> AddPet(PetDto pet)
        {
            try
            {
                await _petService.AddPet(pet);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdatePet/{petId}")]
        public async Task<ActionResult> UpdatePet(PetDto pet, int petId)
        {
            try
            {
                await _petService.UpdatePet(pet, petId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeletePet/{petId}")]
        public async Task<ActionResult> DeletePet(int petId)
        {
            try
            {
                await _petService.DeletePet(petId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
