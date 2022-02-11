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
using MimeKit;
using MailKit;

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
        private readonly INotificationsService notificationService;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
       

        public AdminController(
           IUsersService usersService,
           ICardService cardService,
           ISpecialOffersService specialOffersService,
           IMyInvoicesService myInvoicesService,
           IOrderService orderService,
           IParentService parentService,
           INotificationsService notificationService,
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
            this.notificationService = notificationService;
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

        public IActionResult SendNotification(string returnUrl = null)
        {
            var viewModel = new SendNotificationModel()
            {

            };
            return View(viewModel);
            // return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendNotification(SendNotificationModel model)
        {
            var not = this.notificationService.GetAllNotifications();
            var myInvoice = this.myInvoicesService.GetAllInvoices().Where(i => i.TransportKind == "All Kind" && i.TransportKind == "One Kind");
            var user = this.usersService.GetAllUsers().Where(u => u.Email != null);
           
            if (this.ModelState.IsValid)
            {

                var Notification = new Notifications()
                {
                    TransportNumber = model.TransportNumber,
                    TransportType = model.TransportType,
                    Message = model.Message,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                };

                this.notificationService.Add(Notification);

                //Sending email notification to All Users
                //==============================================================

                var emailList = user.Where(u => u.Email != "citytransportfinalproject@gmail.com").Select(u => u.Email).ToList();

                var emails = String.Join(",", emailList);
               
                var message = new MimeMessage();

                    message.From.Add(new MailboxAddress("City Transport", "citytransportfinalproject@gmail.com"));

               
                foreach (string toAddresses in emails.Split(','))
                {
                    message.Cc.Add(new MailboxAddress("",toAddresses));
                }

                message.Subject = "Transport Change Notification";



                    message.Body = new TextPart("plain")
                    {
                        Text = "Hi, " + "\n\n" + model.TransportNumber + " " + model.TransportType + "\n" + model.Message + "\n\n" + "Regards, " + "\n" + "City Transport"
                    };


                    using (var client = new MailKit.Net.Smtp.SmtpClient(new ProtocolLogger("smtp.log")))
                    {

                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                        client.Connect("smtp.gmail.com", 587, false);

                        client.Authenticate("citytransportfinalproject@gmail.com", "CityTransport1@");

                        client.Send(message);

                        client.Disconnect(true);
                    }

                this.notificationService.Delete(Notification);
                }
            
            return this.RedirectToAction("AdminHomePage", "Admin");
        }
    }
}
