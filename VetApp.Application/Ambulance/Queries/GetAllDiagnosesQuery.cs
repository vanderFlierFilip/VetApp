using MediatR;
using System.Collections.Generic;

namespace VDMJasminka.Application.Ambulance.Queries
{
    public class GetAllDiagnosesQuery : IRequest<IEnumerable<string>>
    {
    }
}
