using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.User;
using TopLearn.Data.Entities.User;
using TopLearn.Data.Entities.Wallet;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IUserService
    {
        #region RegisterLogin
        bool IsExistUserByUserName(string userName);
        bool IsExistUserByEmail(string email);
        void AddUser(User user);
        bool ActiveUserAccount(string activeCode);
        User GetUserForLogin(string email, string password);
        User GetUserByEmail(string email);
        bool IsExistUserByActiveCode(string activeCode);
        bool ResetUserPassword(ResetPasswordViewModel reset);
        User GetUserByUserName(string userName);
        void UpdateUser(User user);
        int GetUserIdByUserName(string userName);
        #endregion

        #region UserPanel

        UserInformationsForShowViewModel GetUserInformationsForShow(string userName);
        SideBarInformationsForShowViewModel GetSideBarInformationsForShow(string userName);
        EditUserProfileViewModel GetUserInformationsForEdit(string userName);
        void EditUserProfile(string userName,EditUserProfileViewModel editUserProfile);


        #endregion

        #region Wallet

        int BalanceUserWallet(string userName);
        List<UserWalletsForShowViewModel> GetUserWalletsForShow(string userName);
        int AddWallet(Wallet wallet);
        Wallet GetWalletById(int walletId);
        void UpdateWallet(Wallet wallet);

        #endregion

        #region Admin

        UsersForShowInAdminViewModel GetUsersForShowInAdmin(int pageId = 1, string filterUserName = "", string filterEmail = "");
        int AddUserFromAdmin(CreateUserViewModel user);
        #endregion

    }
}
