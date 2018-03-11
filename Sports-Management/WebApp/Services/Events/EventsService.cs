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
    public class EventsService : Service<Events>, IEventsService
    {
        private readonly IRepositoryAsync<Events> _repository;
        public EventsService(IRepositoryAsync<Events> repository) : base(repository)
        {
            _repository = repository;
        }

    }
}