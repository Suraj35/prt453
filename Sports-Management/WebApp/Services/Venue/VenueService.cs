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
    public class VenueService : Service<Venues>, IVenueService
    {
        private readonly IRepositoryAsync<Venues> _repository;
        public VenueService(IRepositoryAsync<Venues> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}