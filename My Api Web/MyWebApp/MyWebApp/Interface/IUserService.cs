using MyWebApp.DTO.User.Request;
using MyWebApp.Models;

namespace MyWebApp.Interface
{
    public interface IUserService
    {
        List<string> GetListSortOrder();
        
        //Users Method
        UserModel? RegisterNewUser(RequestRegisterUserDTO requestUserDTO);
        UserModel? getUserByUserName(string userId);
        UserModel? getUserByLogin(RequestUserLoginDTO requestUserDTO);

        //List Users
        Tuple<int, List<UserModel>> getAllUsersWithFilters(StateModel stateModel);

        //Aync Method
        Task<UserModel?> getUserByUserIdAsync(int userID);
    }
}
