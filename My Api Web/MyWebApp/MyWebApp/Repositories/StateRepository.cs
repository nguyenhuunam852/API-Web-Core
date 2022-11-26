using MyWebApp.DTO.State.Request;
using MyWebApp.Interface;
using MyWebApp.Models;

#nullable disable

namespace MyWebApp.Repositories
{
    public class StateRepository : IStateService
    {
        private readonly IUserService _userInterface;

        public StateRepository(IUserService userService)
        {
            _userInterface = userService;
        }

        public StateModel GetState(StateRequestDto stateRequestDto, int userId)
        {
            try
            {
                bool firstLogin = stateRequestDto.FirstLogin ?? false;
                bool sortOrder = stateRequestDto.DESC ?? false;

                string sortBy = "";
                if (string.IsNullOrEmpty(stateRequestDto.SortBy))
                {
                    sortBy = _userInterface.GetListSortOrder()[0];
                }
                else
                {
                    if (!_userInterface.GetListSortOrder().Contains(sortBy)) return null;
                }

                StateModel returnState;

                using (var context = new DBContext())
                {
                    var getState = from states in context.States
                                   where states.UserId == userId
                                   select states;

                    if (getState.Any())
                    {
                        var updateState = getState.First();

                        if(firstLogin) return updateState;

                        if (stateRequestDto.Page - 1 != updateState.Page || stateRequestDto.Filter != updateState.FilterParam)
                        {
                            updateState.FilterParam = stateRequestDto.Filter;
                            updateState.Page = (int)((stateRequestDto.Page == null) ? 0 : stateRequestDto.Page-1);
                            updateState.IsDesc = sortOrder;
                            updateState.SortBy = sortBy;

                            context.States.Update(updateState);
                        }

                        returnState = updateState;
                    }
                    else
                    {
                        var newState = new StateModel()
                        {
                            UserId = userId,
                            Page = (int)((stateRequestDto.Page == null) ? 0 : stateRequestDto.Page-1),
                            FilterParam = stateRequestDto.Filter,
                            IsDesc = sortOrder,
                            SortBy = sortBy
                        };

                        context.States.Add(newState);

                        returnState = newState;
                    }

                    context.SaveChanges();

                    return returnState;
                }
            }
            catch(Exception e)
            {
                throw new Exception("CreateNewState");
            }
        }
    }
}
