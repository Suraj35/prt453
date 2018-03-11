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
    public static class SportsRepository
    {
        //public static IEnumerable<MenuItems> GetAllGroups(this IRepositoryAsync<MenuItems> repository)
        //{
        //    return repository.QueryableCustom().Where(w => w.GroupId == null || w.GroupId == 0);
        //}

        //public static IEnumerable<MenuItemsListViewModels> GetAllResourcesByGroupId(this IRepositoryAsync<MenuItems> repository, int? groupId)
        //{
        //    return repository.QueryableCustom().Where(w => w.GroupId == groupId || groupId==0 && w.IsActive).Select(s=> new MenuItemsListViewModels
        //    { 
        //    GroupName=s.MenuItem.MenuItemName,
        //    IconClass=s.IconClass,
        //    IsParent=s.IsParent,
        //    MenuItemName=s.MenuItemName,
        //    SortOrder=s.SortOrder,
        //    MenuItemId=s.MenuItemId,
        //    GroupId=s.MenuItem.MenuItemId
        //    });
        //}
    }
}