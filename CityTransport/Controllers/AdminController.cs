using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CityTransport.Data.Models;
using CityTransport.Models;
using CityTransport.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityTransport.Models.UserModels;
using CityTransport.Services;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using CityTransport.Models.AdminModels;

namespace CityTransport.Controllers
{
   [Authorize]
    public class AdminController : Controller
    {

        private readonly IHttpContextAccessor httpaccessor;
        private readonly IUsersService usersService;
        private readonly ICardService cardService;
        private readonly ISpecialOffersService specialOffersService;
        private readonly IMyInvoicesService myInvoicesService;
        private readonly IOrderService orderService;
        private readonly IParentService parentService;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
       

        public AdminController(
           IUsersService usersService,
           ICardService cardService,
           ISpecialOffersService specialOffersService,
           IMyInvoicesService myInvoicesService,
           IOrderService orderService,
           IParentService parentService,
        IHttpContextAccessor httpContextAccessor,
           IMapper mapper,
            UserManager<User> userManager)
        {
            this.usersService = usersService;
            this.cardService = cardService;
            this.specialOffersService = specialOffersService;
            this.myInvoicesService = myInvoicesService;
            this.orderService = orderService;
            this.parentService = parentService;
            this.httpaccessor = httpContextAccessor;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        private string GetCurrentUserId() => this.userManager.GetUserId(HttpContext.User);

        // [AllowAnonymous]
        public IActionResult AdminHomePage()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ViewChildren(string id)
        {
            if (id == null)
            {
                return RedirectToAction("ViewChildren", "Admin");
            }
            string childrenId = id;
            // var offers = specialOffersService.GetOffersById(offerId);

            //string userRole 
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            //Find Offers By OfferId
            var parent = this.parentService.GetAllParents().FirstOrDefault(p => p.ChildrenId == childrenId);

            var viewModel = new ViewChildrenModel()
            {
                User = this.usersService.GetAllUsers().FirstOrDefault(),
                //SpecialOffers = user.Role,
                ChildrenFirstName = parent.ChildrenFirstName,
                ChildrenLastName = parent.ChildrenLastName,
                ChildrenCardNumber = parent.ChildrenCardNumber,
                ChildrenId = parent.ChildrenId

            };
            if (this.ModelState.IsValid)
            {
                this.parentService.Delete(parent);
            };
            return this.RedirectToAction("AdminHomePage", "Admin");
            //return View(viewModel);

        }
       
        [HttpGet]
        public IActionResult DeleteChildren(DeleteChildrenModel model)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            // var parent = this.parentService.GetAllParents().FirstOrDefault(p => p.UserId == userId);
                DeleteChildrenModel inputModel = new DeleteChildrenModel()
            {

                ChildrenFirstName = model.ChildrenFirstName,
                ChildrenLastName = model.ChildrenLastName,
               // UserId = user.Id

            };


            var children = new List<Parent>();
            if (model != null)
            {
                bool ChildrenFirstName = !String.IsNullOrEmpty(inputModel.ChildrenFirstName);
                
                bool ChildrenLastName = !String.IsNullOrEmpty(inputModel.ChildrenLastName);
                children = parentService.GetAllParents().ToList();
                if (ChildrenFirstName)
                    children = children.Where(u => u.ChildrenFirstName == inputModel.ChildrenFirstName).ToList();
                if(ChildrenLastName)
                    children = children.Where(u => u.ChildrenLastName == inputModel.ChildrenLastName).ToList();

            }
            if (children == null)
            {
                return View(inputModel);
            }
            else
            {
               
                return View(new DeleteChildrenModel()
                {
                    
                    ParenChildrenList = children


                });

            }
        }
       
        public IActionResult AddSpecialOffer(string returnUrl = null)
        {
            var viewModel = new AddSpecialOfferModel()
            {
                
            };
            return View(viewModel);
           // return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSpecialOffer(AddSpecialOfferModel model)
        {
            if (this.ModelState.IsValid)
            {

                var Offer = new SpecialOffers()
                {
                    SpecialOffersID = model.SpecialOffersID,
                    OfferName = model.OfferName,
                    OfferDescription = model.OfferDescription,
                    OfferPrice = model.OfferPrice,
                    RoleID = model.Role
                };

                this.specialOffersService.Add(Offer);
            };
            return this.RedirectToAction("AdminHomePage", "Admin");
        }


    }
}
