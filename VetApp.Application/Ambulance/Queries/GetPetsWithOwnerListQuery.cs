using MediatR;
using System.Collections.Generic;
using VDMJasminka.Application.Dtos.Ambulance;

namespace VDMJasminka.Application.Ambulance.Queries
{
    public class GetPetsWithOwnerListQuery : IRequest<IEnumerable<PetSearchViewModel>>
    {



    }
}

