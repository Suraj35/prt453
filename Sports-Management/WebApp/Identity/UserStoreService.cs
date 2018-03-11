
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using WebApp.Models;
using WebApp.Helpers;
using Repository.Pattern;
using System.Data.Entity.Validation;
using AutoMapper;
using System.Web;
using Microsoft.Owin.Security;

namespace WebApp.Identity
{
    public class UserStoreService : IUserStore<Users, long>,
        IUserPasswordStore<Users, long>,
        IUserEmailStore<Users, long>,

        IDisposable
    {
        ApplicationDbContext _context;
        private bool _disposed;


        public UserStoreService()
        {
            _context = new ApplicationDbContext();
        }

        public UserStoreService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region IUserStore<TUser,TKey>

        public Task CreateAsync(Users user)
        {
            _context.User.Add(user);
            return _context.SaveChangesAsync();
        }
        public Task DeleteAsync(Users user)
        {
            throw new NotImplementedException();
        }
        public Task<Users> FindByIdAsync(long userId)
        {
            Task<Users> task =
            _context.User.Where(apu => apu.Id == userId && apu.IsActive)
            .FirstOrDefaultAsync();

            return task;
        }
        public Task<Users> FindByNameAsync(string userName)
        {
            Task<Users> task =
                _context.User.Where(apu => apu.UserName == userName && apu.IsActive)
            .FirstOrDefaultAsync();

            return task;
        }
        public Task UpdateAsync(Users user)
        {
            Users oldUser = _context.User.Where(w => w.Id == user.Id).FirstOrDefault();
            Users nameUser = _context.User.Where(w => w.UserName == user.UserName && w.IsActive && w.Id != user.Id).FirstOrDefault();

            if (nameUser != null)
            {
                //sorry app nhi kr skty pehly mojood hy
            }
            else
            {
                //oldUser = user;
                //user.Address = oldUser.Address;
                //user.City = oldUser.Address;
                user.DateCreated = oldUser.DateCreated;
                user.DateModified = DateTime.Now;
                //user.Email = oldUser.Email;
                user.IsActive = oldUser.IsActive;
                user.Id = oldUser.Id;
                user.IsPasswordResetRequested = oldUser.IsPasswordResetRequested;
                //user.IsTeam = oldUser.IsTeam;
                //user.LastLogin = oldUser.LastLogin;
                //user.Name = oldUser.Name;
                user.Password = oldUser.Password;
                //user.ProfilePic = oldUser.ProfilePic;
                user.TempPassword = oldUser.TempPassword;
                //user.TotalMembers = oldUser.TotalMembers;
                //user.UserName = oldUser.UserName;

                _context.Entry(oldUser).CurrentValues.SetValues(user);
                oldUser.DateModified = DateTime.Now;
            }
            _context.SaveChanges();
            return Task.FromResult(0);

        }

        public Result<long> Delete(long userId)
        {
            Result<long> result = new Result<long>();
            try
            {
                Users user = _context.User.Where(u => u.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    user.IsActive = false;
                    _context.Entry(user).State = EntityState.Modified;
                    _context.SaveChanges();

                }
                else
                {
                    result.success = false;
                    result.AddError("User does not exist in system");
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.AddError(ex.Message);
            }

            return result;
        }


        public Result<long> Activate(long userId)
        {
            Result<long> result = new Result<long>();
            try
            {
                Users user = _context.User.Where(u => u.Id == userId).FirstOrDefault();
                Users nameUser = _context.User.Where(w => w.UserName == user.UserName && w.IsActive && w.Id != user.Id).FirstOrDefault();
                if (user != null && nameUser==null)
                {

                    user.IsActive = true;
                    _context.Entry(user).State = EntityState.Modified;
                    _context.SaveChanges();

                }
                else
                {
                    result.success = false;
                    result.AddError("User does not exist in system");
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.AddError(ex.Message);
            }

            return result;
        }






        #endregion


        #region IUserPasswordStore

        public Task<string> GetPasswordHashAsync(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            return Task.FromResult(user.Password);
        }
        public Task<bool> HasPasswordAsync(Users user)
        {
            return Task.FromResult(user.Password != null);
        }
        public Task SetPasswordHashAsync(
          Users user, string passwordHash)
        {
            user.Password = passwordHash;
            return Task.FromResult(0);
        }
        #endregion


        #region IUserEmailStore

        public Task SetEmailAsync(Users user, string email)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            user.Email = email;
            _context.SaveChanges();

            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(Users user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(Users user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(true);
        }

        public Task SetEmailConfirmedAsync(Users user, bool confirmed)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            //    user.Email = true;
            _context.SaveChanges();

            return Task.FromResult(0);
        }

        public Task<Users> FindByEmailAsync(string email)
        {
            return _context.User.FirstOrDefaultAsync(u => u.Email.ToUpper() == email.ToUpper());
        }

        public Users FindByEmail(string email)
        {
            return _context.User.Where(u => u.Email.ToUpper() == email.ToUpper() && u.IsActive == true).FirstOrDefault();
        }

        #endregion

        #region IUserClaimStore


        #endregion


        #region CustomMethod

        #endregion

        public Result<List<Users>> GetAllUsersPaged(int pageId, int pageSize, ref int count)
        {
            Result<List<Users>> result = new Result<List<Users>>();
            try
            {
                List<Users> users = _context.User.Where(u => u.IsActive).OrderByDescending(i => i.DateCreated).ToList();
                if (users != null && users.Count > 0)
                    result.data = users;
                else
                {
                    result.success = false;
                    result.AddError("No user found");
                }


            }
            catch (Exception ex)
            {
                result.success = false;
                result.AddError(ex.Message);
            }
            return result;
        }

        public Result<List<Users>> GetAllUsersWithoutCurrent(int EventID, long currentUserId, bool team = false)
        {
            Result<List<Users>> result = new Result<List<Users>>();
            try
            {
                List<Users> users = new List<Users>();
                users = _context.User.Where(u => u.IsSuperAdmin == false && u.Id != currentUserId && u.IsActive && u.IsTeam == team).OrderByDescending(i => i.Name).ToList();
                var toRemoveUserIds = _context.UserChallenges.Where(w => w.EventId == EventID && w.IsActive).Select(s => new {
                    id = s.ToChallengeId
                }).ToList(); // && !w.IsAccepted && w.User.IsSuperAdmin == false && w.User.Id != currentUserId && w.User.IsActive && w.User.IsTeam == team
                foreach (var item in toRemoveUserIds)
                {
                    users.RemoveAll(ra => ra.Id == item.id);
                }
                if (users != null && users.Count > 0)
                    result.data = users;
                else
                {
                    result.success = false;
                    result.AddError("No user found");
                }


            }
            catch (Exception ex)
            {
                result.success = false;
                result.AddError(ex.Message);
            }
            return result;
        }

        public Result<List<Users>> GetAllUsers()
        {
            Result<List<Users>> result = new Result<List<Users>>();
            try
            {
                List<Users> users = new List<Users>();
                users = _context.User.Where(u => u.IsSuperAdmin == false).OrderByDescending(i => i.Name).ToList();
                if (users != null && users.Count > 0)
                    result.data = users;
                else
                {
                    result.success = false;
                    result.AddError("No user found");
                }


            }
            catch (Exception ex)
            {
                result.success = false;
                result.AddError(ex.Message);
            }
            return result;
        }


        public void Dispose()
        {
            //  _context.Dispose();
        }
    }
}