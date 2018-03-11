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
    public class NotificationsService : Service<Notifications>, INotificationsService
    {
        private readonly IRepositoryAsync<Notifications> _repository;
        public NotificationsService(IRepositoryAsync<Notifications> repository)
            : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<Notifications> GetNotificationByUserId(long userId)
        {
            return _repository.GetNotificationByUserId(userId);
        }

    }
}