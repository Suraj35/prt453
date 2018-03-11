using Repository.Pattern;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using WebApp.HelperClass;
using WebApp.Models;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class TicketsController : BaseController
    {
        Result<int> saveResult = new Result<int>();
        ITicketsService _ticketsService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public TicketsController(ITicketsService ticketsService, IUnitOfWorkAsync unitOfWork)
        {
            _ticketsService = ticketsService;
            _unitOfWork = unitOfWork;
        }
        // GET: Tickets
        public ActionResult Buy(int id)
        {
            Tickets model = new Tickets();
            if (id > 0)
            {
                model.EventId = id;
            }
            else
            {
                AddErrors(null, "Please try to buy ticket for some event");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Buy(Tickets model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Common.CurrentUser.Id;
                _ticketsService.Insert(model);
                saveResult= _unitOfWork.SaveChanges();
                if (!saveResult.success)
                {
                    AddErrors(saveResult.errors, saveResult.ErrorMessage);
                    return View(model);
                }
            }
            else
            {
                AddErrors(null, "Please provide required info.");
                return View(model);
            }
            return RedirectToAction("Detail", new { id = model.TicketId });
        }

        public ActionResult Detail(int id)
        {
            TicketViewModels model = new TicketViewModels();
            model = _ticketsService.QueryableCustom().Select(s =>
            new TicketViewModels {
                Address = s.Address,
                CardNumber = s.CardNumber,
                EventName = s.Event.EventName,
                FirstName = s.FirstName,
                LastName = s.LastName,
                TicketId = s.TicketId,
                UserName = s.User.UserName
            }).FirstOrDefault();
            return View(model);
        }
    }
}