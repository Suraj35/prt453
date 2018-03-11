using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Repositories;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class UserChallengesService : Service<UserChallenges>, IUserChallengesService
    {
        private readonly IRepositoryAsync<UserChallenges> _repository;
        public UserChallengesService(IRepositoryAsync<UserChallenges> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}