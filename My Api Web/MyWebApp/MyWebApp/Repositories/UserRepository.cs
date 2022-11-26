using Microsoft.EntityFrameworkCore;
using MyWebApp.DTO.User.Request;
using MyWebApp.Interface;
using MyWebApp.Models;

namespace MyWebApp.Repositories
{
    public class UserRepository : IUserService
    {
        private readonly int _perPage = 5;

        private readonly List<string> _listSortOrder = new List<string>(){ "username","fullname","email" };

        //private method
        private IQueryable<UserModel> orderList(IQueryable<UserModel> userList, bool isDesc, string sortBy)
        {
            if (sortBy == _listSortOrder[0])
            {
                return isDesc ? userList.OrderByDescending(x => x.UserName) : userList.OrderBy(x => x.UserName);
            }

            if (sortBy == _listSortOrder[1])
            {
                return isDesc ? userList.OrderByDescending(x => x.FullName) : userList.OrderBy(x => x.FullName);
            }

            return isDesc ? userList.OrderByDescending(x => x.Email) : userList.OrderBy(x => x.Email);
        }

        private List<UserModel> getOrderPagiList(IQueryable<UserModel> userList, int currentPage, bool isDesc, string sortBy)
        {
            return orderList(userList, isDesc, sortBy).Skip(currentPage * _perPage).Take(_perPage).ToList();
        }

        //public method
        public List<string> GetListSortOrder()
        {
            return _listSortOrder;
        }

        public Tuple<int,List<UserModel>> getAllUsersWithFilters(StateModel stateModel)
        {
            try
            {
                int pages = 0;

                using (var context = new DBContext())
                {
                    var pagiList = context.Users;

                    if (!string.IsNullOrEmpty(stateModel.FilterParam))
                    {
                        var filterList = from users in pagiList where  
                                         EF.Functions.Like(users.UserName, String.Format("%{0}%", stateModel.FilterParam)) ||
                                         EF.Functions.Like(users.FullName, String.Format("%{0}%", stateModel.FilterParam))
                                         select users;

                        pages = ((filterList.Count()) % _perPage == 0) ? filterList.Count() / _perPage : filterList.Count() / _perPage + 1;

                        return new Tuple<int, List<UserModel>>(pages, getOrderPagiList(filterList, stateModel.Page, stateModel.IsDesc, stateModel.SortBy));
                    }

                    pages = ((pagiList.Count()) % _perPage == 0) ? pagiList.Count() / _perPage : pagiList.Count() / _perPage + 1;

                    return new Tuple<int, List<UserModel>>(pages, getOrderPagiList(pagiList, stateModel.Page, stateModel.IsDesc, stateModel.SortBy));
                }
            }
            catch
            {
                throw new Exception("getAllUserWithFilters");
            }
        }

        public UserModel? getUserByUserName(string userName)
        {
            try
            {
                using (var context = new DBContext())
                {
                    var getUser = from user in context.Users
                               where user.UserName == userName
                                  select user;
                    if (!getUser.Any()) return null;
                    return getUser.First();
                }
            }
            catch
            {
                throw new System.Exception("getUserByUserName");
            }
        }

        public UserModel? getUserByLogin(RequestUserLoginDTO requestUserDTO)
        {
            try
            {
                using (var context = new DBContext())
                {
                    var getUser = from user in context.Users
                                  where user.UserName == requestUserDTO.UserName &&
                                        user.Password == requestUserDTO.UserPassword
                                  select user;
                    if (!getUser.Any()) return null;
                    return getUser.First();
                }
            }
            catch
            {
                throw new System.Exception("getUserByLogin");
            }
        }

        public UserModel? RegisterNewUser(RequestRegisterUserDTO requestUserDTO)
        {
            try
            {
                UserModel userModel = new UserModel()
                {
                    UserName = requestUserDTO.UserName,
                    FullName = requestUserDTO.FullName,
                    Email = requestUserDTO.Email,
                    Password = requestUserDTO.UserPassword,
                };

                using (var context = new DBContext())
                {
                    context.Users.Add(userModel);

                    context.SaveChanges();
                }

                return userModel;
            }
            catch
            {
                throw new System.Exception("RegisterNewUser");
            }
        }

        public async Task<UserModel?> getUserByUserIdAsync(int userId)
        {
            try
            {
                using (var context = new DBContext())
                {
                    var getUser = await context.Users.FindAsync(userId);
                    return getUser;
                }
            }
            catch
            {
                throw new System.Exception("getUserByUserId");
            }
        }
    }
}
