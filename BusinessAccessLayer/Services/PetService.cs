using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public PetService(BootcampDbContext bootcampDbContext, IMapper mapper, ICurrentUserService currentUserService)
        {
            _db = bootcampDbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task AddPet(PetDto petDto)
        {
            var userId = _currentUserService.UserId;
            var username = _currentUserService.Username;
            if (userId == 0 || username == string.Empty)
            {
                throw new Exception("User required");
            }

            var pet = new Pet
            {
                Name = petDto.Name,
                Description = petDto.Description,
                Birthday = petDto.Birthday,
                Type = petDto.Type,
                Breed = petDto.Breed,
                UserId = userId,
                ImageUrl = petDto.ImageUrl
            };
            _db.Pets.Add(pet);
            
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdatePet(PetDto petDto, int petId)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeletePet(int petId)
        {
            try
            {
                var pet = await _db.Pets.FirstOrDefaultAsync(x => x.Id == petId);
                if (pet == null) throw new Exception("Pet not found");
                _db.Pets.Remove(pet);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PetDto> GetPet(int petId)
        {
            if (petId == 0) throw new Exception("PetId required");

            //try-catch
            //try-catch-finally
            try
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
                        UserId = pet.UserId,
                        ImageUrl = pet.ImageUrl
                    };

                var serviceResult = new ServiceResult<PetDto>()
                {
                    IsSuccess = pet != null,
                    ErrorMessage = pet == null ? "No pet found" : string.Empty,
                    Data = petDto
                };

                return petDto;
            }
            catch (Exception ex)
            {
                var ex2 = new Exception("Error getting pet", ex);
                throw ex2;
            }
        }

        public async Task<List<PetDto>> GetPets()
        {
            try
            {
                var pets = await _db.Pets.ToListAsync();

                var petDtos = new List<PetDto>();

                //foreach (var pet in pets)
                //{
                //    var petDto = new PetDto
                //    {
                //        Name = pet.Name,
                //        Description = pet.Description,
                //        Birthday = pet.Birthday,
                //        Type = pet.Type,
                //        Breed = pet.Breed,
                //        UserId = pet.UserId
                //    };
                //    petDtos.Add(petDto);
                //}

                petDtos = _mapper.Map<List<PetDto>>(pets);

                var serviceResult = new ServiceResult<List<PetDto>>()
                {
                    IsSuccess = pets != null,
                    ErrorMessage = pets == null ? "No pets found" : string.Empty,
                    Data = petDtos
                };

                return petDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
