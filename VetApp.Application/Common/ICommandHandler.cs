using System.Threading.Tasks;

namespace VDMJasminka.Application.Common
{
    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
