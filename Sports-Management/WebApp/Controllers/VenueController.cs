using Repository.Pattern;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Helpers;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class VenueController : BaseController
    {
        Result<int> saveResult = new Result<int>();
        IVenueService _venueService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        public VenueController(IVenueService venueService, IUnitOfWorkAsync unitOfWork)
        {
            _venueService = venueService;
            _unitOfWork = unitOfWork;
        }
        // GET: Venue
        public ActionResult Index()
        {
            IEnumerable<Venues> model = new List<Venues>();
            model = _venueService.Queryable().data;
            return View(model);
        }



        [SuperAdminAuthorizeAttribute]
        public ActionResult Detail(int id = 0)
        {
            Result<Venues> model = new Result<Venues>();
            if (id > 0)
            {
                model = _venueService.Find(id);
                return View(model.data);
            }
            
            return View(model.data);
        }

        [SuperAdminAuthorizeAttribute]
        [HttpPost]
        public ActionResult Detail(Venues model)
        {
            model.ObjectState = model.VenueId > 0 ? ObjectState.Modified : ObjectState.Added;
            _venueService.InsertOrUpdateGraph(model);
            saveResult = _unitOfWork.SaveChanges();
            if (saveResult.success)
            {
                TempData["successmessage"] = "Saved Successfully.";
            }
            else
            {
                AddErrors(saveResult.errors, saveResult.ErrorMessage);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [SuperAdminAuthorizeAttribute]
        public ActionResult Delete(int id)
        {
            var result = _venueService.Delete(id);
            saveResult = _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}