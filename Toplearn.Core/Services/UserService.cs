using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Data.Context;
using TopLearn.Core.Convertors;
using TopLearn.Core.Generators;
using TopLearn.Data.Entities.User;
using TopLearn.Core.Security;
using TopLearn.Core.DTOs.User;

namespace TopLearn.Core.Services
{
    public class UserService : IUserService
    {
        private TopLearnContext _context;
        public UserService(TopLearnContext context)
        {
            _context = context;
        }

        public bool ActiveUserAccount(string activeCode)
        {
            var user=_context.Users.SingleOrDefault(u=> u.ActiveCode == activeCode);
            if (user != null)
            {
                user.IsActive= true;
                user.ActiveCode = NameGenerator.GenerateUniqName();
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool IsExistUserByEmail(string email)
        {
            return _context.Users.Any(u=> u.Email == EmailConvertor.FixEmail(email));
        }

        public bool IsExistUserByUserName(string userName)
        {
            return _context.Users.Any(u=>u.UserName == userName);
        }

        public User GetUserForLogin(string email, string password)
        {
            return _context.Users.SingleOrDefault(u => u.Email == EmailConvertor.FixEmail(email) && u.Password == PasswordHasher.HashPasswrodMD5(password));
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u=>u.Email == EmailConvertor.FixEmail(email));
        }

        public bool ResetUserPassword(ResetPasswordViewModel reset)
        {
            var user=_context.Users.SingleOrDefault(u=>u.ActiveCode == reset.ActiveCode);
            if (user != null)
            {
                user.Password = PasswordHasher.HashPasswrodMD5(reset.Password);
                user.ActiveCode = NameGenerator.GenerateUniqName();
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool IsExistUserByActiveCode(string activeCode)
        {
            return _context.Users.Any(u=>u.ActiveCode==activeCode);
        }

        public UserInformationsForShowViewModel GetUserInformationsForShow(string userName)
        {
            return _context.Users.
                Where(u => u.UserName == userName)
                .Select(u => new UserInformationsForShowViewModel()
                {
                    UserName = u.UserName,
                    Email = u.Email,
                    RegisterDate = u.RegisterDate,
                    Wallet = 0
                }).Single();
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(u=>u.UserName==userName);
        }

        public SideBarInformationsForShowViewModel GetSideBarInformationsForShow(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName)
                .Select(u => new SideBarInformationsForShowViewModel()
                {
                    UserName = u.UserName,
                    RegisterDate = u.RegisterDate,
                    AvatarName = u.AvatarName,
                }).Single();
        }

        public EditUserProfileViewModel GetUserInformationsForEdit(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName)
                .Select(u => new EditUserProfileViewModel()
                {
                    UserName = u.UserName,
                    Email = u.Email,
                    AvatarName = u.AvatarName,
                }).Single();
        }

        public void EditUserProfile(string userName, EditUserProfileViewModel editUserProfile)
        {
            var user=GetUserByUserName(userName);
            if (editUserProfile.UserAvatar != null)
            {
                string imagePath = "";
                if (editUserProfile.AvatarName == "Default.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "UserAvatar",
                        editUserProfile.AvatarName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                user.AvatarName = NameGenerator.GenerateUniqName()+Path.GetExtension(editUserProfile.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "UserAvatar",
                    user.AvatarName);
                using(var stream=new FileStream(imagePath, FileMode.Create))
                {
                    editUserProfile.UserAvatar.CopyTo(stream);
                }
            }
            user.UserName=editUserProfile.UserName;
            user.Email = EmailConvertor.FixEmail(editUserProfile.Email);
            UpdateUser(user);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
