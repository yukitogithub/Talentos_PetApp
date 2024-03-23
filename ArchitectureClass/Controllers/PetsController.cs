using BusinessAccessLayer.Dto;
using BusinessAccessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Infrastucture.Dto;

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
            try
            {
                var pets = await _petService.GetPets();
                return Ok(pets);
            }
            catch (Exception ex)
            {
                //var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPet/{petId}")]
        public async Task<ActionResult> GetPet(int petId)
        {
            try
            {
                var pet = await _petService.GetPet(petId);
                return Ok(pet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Crear endpoint para devolver imagen de mascota dado el nombre de la imagen
        [HttpGet("GetPetImage/{imageName}")]
        public async Task<ActionResult> GetPetImage(string imageName)
        {
            try
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", imageName);
                
                if (!System.IO.File.Exists(imagePath))
                {
                    return NotFound("Image not found");
                }
                
                var image = System.IO.File.OpenRead(imagePath);
                return File(image, "image/jpeg");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddPet")]
        public async Task<ActionResult> AddPet([FromForm] AddPetDto petDto)
        {
            //Dos opciones para guardar imagen
            //1-Imagen a base de datos: byte[]
            //2-Imagen a servidor: Guardar en carpeta

            //Usaremos la opcion 2

            //Crear nombre de archivo unico
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(petDto.Image.FileName);

            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

            //Verificar que la carpeta exista
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
            }
            
            //Guardar imagen en carpeta
            using (var stream = System.IO.File.Create(imagePath)) 
            { 
                await petDto.Image.CopyToAsync(stream);
            }

            try
            {
                var pet = new PetDto
                {
                    Name = petDto.Name,
                    Description = petDto.Description,
                    Birthday = petDto.Birthday,
                    Type = petDto.Type,
                    Breed = petDto.Breed,
                    ImageUrl = fileName
                };
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
