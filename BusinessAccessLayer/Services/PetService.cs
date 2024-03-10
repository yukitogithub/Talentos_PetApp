using BusinessAccessLayer.Dto;
using DataAccessLayer.BootcampDbContext;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public class PetService : IPetService
    {
        private readonly BootcampDbContext _db;
        public PetService(BootcampDbContext bootcampDbContext)
        {
            _db = bootcampDbContext;        
        }

        public async Task AddPet(PetDto petDto)
        {
            if (petDto.UserId == 0) throw new Exception("User required");
            var pet = new Pet
            {
                Name = petDto.Name,
                Description = petDto.Description,
                Birthday = petDto.Birthday,
                Type = petDto.Type,
                Breed = petDto.Breed,
                UserId = petDto.UserId
            };
            _db.Pets.Add(pet);
            await _db.SaveChangesAsync();
        }

        public async Task UpdatePet(PetDto petDto, int petId)
        {
            var pet = await _db.Pets.FirstOrDefaultAsync(x => x.Id == petId);
            
            if (pet == null) throw new Exception("Pet not found");
            
            pet.Name = petDto.Name;
            pet.Description = petDto.Description;
            pet.Birthday = petDto.Birthday;
            pet.Type = petDto.Type;
            pet.Breed = petDto.Breed;
            
            _db.Pets.Update(pet);
            await _db.SaveChangesAsync();
        }

        public async Task DeletePet(int petId)
        {
            var pet = await _db.Pets.FirstOrDefaultAsync(x => x.Id == petId);
            if (pet == null) throw new Exception("Pet not found");
            _db.Pets.Remove(pet);
            await _db.SaveChangesAsync();
        }

        public async Task<PetDto> GetPet(int petId)
        {
            var pet = await _db.Pets.FirstOrDefaultAsync(x => x.Id == petId);
            
            var petDto = new PetDto();
            
            if (pet != null) 
                petDto = new PetDto
                {
                    Name = pet.Name,
                    Description = pet.Description,
                    Birthday = pet.Birthday,
                    Type = pet.Type,
                    Breed = pet.Breed,
                    UserId = pet.UserId
                };
            
            return petDto;
        }

        public async Task<List<PetDto>> GetPets()
        {
            var pets = await _db.Pets.ToListAsync();
            
            var petDtos = new List<PetDto>();
            
            foreach (var pet in pets)
            {
                var petDto = new PetDto
                {
                    Name = pet.Name,
                    Description = pet.Description,
                    Birthday = pet.Birthday,
                    Type = pet.Type,
                    Breed = pet.Breed,
                    UserId = pet.UserId
                };
                petDtos.Add(petDto);
            }
            
            return petDtos;
        }
    }
}
