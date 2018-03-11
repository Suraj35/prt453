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
    public class TicketsService : Service<Tickets>, ITicketsService
    {
        private readonly IRepositoryAsync<Tickets> _repository;
        public TicketsService(IRepositoryAsync<Tickets> repository) : base(repository)
        {
            _repository = repository;
        }

    }
}