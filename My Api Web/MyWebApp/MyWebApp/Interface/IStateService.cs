using MyWebApp.DTO.State.Request;
using MyWebApp.Models;

namespace MyWebApp.Interface
{
    public interface IStateService
    {
        StateModel GetState(StateRequestDto? stateRequestDto, int userId);
    }
}
