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
    public class SportsService : Service<Sports>, ISportsService
    {
        private readonly IRepositoryAsync<Sports> _repository;
        public SportsService(IRepositoryAsync<Sports> repository) : base(repository)
        {
            _repository = repository;
        }

    }
}