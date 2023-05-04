using System.Collections.Generic;
using System.Linq;
using VDMJasminka.Application.Dtos.Ambulance;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.WebClient.Models;

namespace VDMJasminka.WebClient.Controllers.Helpers
{
    public class OwnerMapper
    {
        private readonly PetMapper _petMapper;

        public OwnerMapper()
        {
            _petMapper = new PetMapper();
        }
        //public Owner FromOwnerModelToOwner(OwnerModel model)
        //{
        //    return new Owner
        //    {
        //        //PetId = (int)model.PetId,
        //        //PetName = model.PetName,
        //        //SSN = model.SSN,
        //        //AdditionalInfo = model.AdditionalInfo,
        //        //StreetAddress = model.StreetAddress,
        //        //PhoneNumber = model.PhoneNumber,
        //        //Municipality = model.Municipality,
        //        //Location = model.Location,
        //    };



        //public CreateOrUpdateOwnerDto FromOwnerModelToCreateOwnerDto(OwnerModel model)
        //{
        //    return new CreateOrUpdateOwnerDto
        //    {
        //        PetId = model.PetId,
        //        PetName = model.PetName,
        //        SSN = model.SSN,
        //        AdditionalInfo = model.AdditionalInfo,
        //        StreetAddress = model.StreetAddress,
        //        PhoneNumber = model.PhoneNumber,
        //        Municipality = model.Municipality,
        //        Location = model.Location,
        //        Pets = model.Pets != null ? model.Pets.Select(p => _petMapper.FromPetModelToCreatePetDto(p))
        //        : new List<CreatePetDto>(),

        //    };
        //}



        //public OwnerModel FromOwnerDtoToOwnerModel(ReadOwnerDto dto)
        //{
        //    return new OwnerModel
        //    {
        //        PetId = (int)dto.PetId,
        //        PetName = dto.PetName,
        //        SSN = dto.SSN,
        //        AdditionalInfo = dto.AdditionalInfo,
        //        StreetAddress = dto.StreetAddress,
        //        PhoneNumber = dto.PhoneNumber,
        //        Municipality = dto.Municipality,
        //        Location = dto.Location,
        //        Pets = dto.Pets != null
        //        ? dto.Pets.Select(p => _petMapper.FromPetDtoToPetModel(p))
        //        : new List<PetModel>(),

        //    };
        //}
    }
}
