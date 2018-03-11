using Repository.Pattern;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public interface INotificationsService : IService<Notifications>
    {
        IEnumerable<Notifications> GetNotificationByUserId(long userId);
    }
}
