using VDMJasminka.Application.Dtos.Ambulance;
using VDMJasminka.WebClient.Models;

namespace VDMJasminka.WebClient.Controllers.Helpers
{
    public class PetMapper
    {
        public PetMapper()
        {
        }


        public CreatePetDto FromPetModelToCreatePetDto(PetModel model)
        {
            return new CreatePetDto
            {
                Name = model.Name,
                ChipNumber = model.ChipNumber,
                BreedId = model.BreedId,
                DateOfBirth = model.DateOfBirth,
                Sex = model.Sex,
                Description = model.Description,
                Alergy = model.Alergy,
                OwnerId = model.OwnerId,
                AnimalTypeId = model.SpeciesId,
            };
        }

        public PetModel FromPetDtoToPetModel(ReadPetDto dto)
        {
            return new PetModel
            {
                PetId = dto.Id,
                Name = dto.Name,
                ChipNumber = dto.ChipNumber,
                DateOfBirth = dto.DateOfBirth,
                Sex = dto.Sex,
                Description = dto.Description,
                Alergy = dto.Alergy,

                LastCheckup = dto.LastCheckup
            };
        }
    }
}
