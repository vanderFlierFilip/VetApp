using System.Threading.Tasks;

namespace VDMJasminka.Application.Common
{
    public interface IDiagnosisService
    {
        Task CheckIfDiagnosisExistsOrPersistAsync(string diagnosis);
    }
}