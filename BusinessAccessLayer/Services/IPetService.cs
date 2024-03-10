using BusinessAccessLayer.Dto;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public interface IPetService
    {
        Task AddPet(PetDto petDto);
        Task<PetDto> GetPet(int petId);
        Task<List<PetDto>> GetPets();
        Task UpdatePet(PetDto petDto, int petId);
        Task DeletePet(int petId);
    }
}
