using Repository.Pattern;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Repositories
{
    public static class NotificationsRepository
    {
        public static IEnumerable<Notifications> GetNotificationByUserId(this IRepositoryAsync<Notifications> repository, long userId)
        {
            var query = repository.QueryableCustom().Where(n => n.UserId == userId);
            return query;
        }
    }
}