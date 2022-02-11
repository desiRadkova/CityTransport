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
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MailKit;

namespace CityTransport.Controllers
{
   [Authorize]
    public class UsersController : Controller
    {

        private readonly IHttpContextAccessor httpaccessor;
        private readonly IUsersService usersService;
        private readonly ICardService cardService;
        private readonly ISpecialOffersService specialOffersService;
        private readonly IMyInvoicesService myInvoicesService;
        private readonly IOrderService orderService;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
       

        public UsersController(
           IUsersService usersService,
           ICardService cardService,
           ISpecialOffersService specialOffersService,
           IMyInvoicesService myInvoicesService,
           IOrderService orderService,
           IHttpContextAccessor httpContextAccessor,
           IMapper mapper,
            UserManager<User> userManager)
        {
            this.usersService = usersService;
            this.cardService = cardService;
            this.specialOffersService = specialOffersService;
            this.myInvoicesService = myInvoicesService;
            this.orderService = orderService;
            this.httpaccessor = httpContextAccessor;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        private string GetCurrentUserId() => this.userManager.GetUserId(HttpContext.User);

       // [AllowAnonymous]
 
        public IActionResult UserHomePage(string id)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            var card = this.cardService.GetAllCards().FirstOrDefault(c => c.UserId == userId);

           

       if (card == null)
            {
                return this.RedirectToAction("AddCard", "Users");
            }
            var viewModel = new UserHomePageModel()
            {
                User = this.usersService.GetAllUsers().FirstOrDefault(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                Role = user.Role,
                
            
                EndDate = card.EndDate,
                City = user.City

            };

          
            //Sending email notification
            //==============================================================
            if (card.EndDate.AddDays(-3) == DateTime.Today)
            {
                
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("City Transport", "citytransportfinalproject@gmail.com"));

                message.To.Add(new MailboxAddress("", user.Email));

                message.Subject = "Transport Card Expire";

               

                message.Body = new TextPart("plain")
                {
                    Text = "Hi "+ user.FirstName + " " + user.LastName + ", " +
                    "\n\n" +
                    "Your card for city transport will expire in three days!" +
                    "\n\n" +
                    "Regards," +
                    "\n"+
                    "City Transport"
                };

             
                using (var client = new MailKit.Net.Smtp.SmtpClient(new ProtocolLogger("smtp.log")))
                {

                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect("smtp.gmail.com", 587, false);

                    client.Authenticate("citytransportfinalproject@gmail.com", "CityTransport1@");
                    
                    client.Send(message);
                   
                    client.Disconnect(true);
                }
            }

            return View(viewModel);
        }
       
        [HttpGet]
        public IActionResult ViewOffer(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("ViewOffer", "Users");
            }
            int offerId = id;
          
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            //Find Offers By OfferId
            var offer = this.specialOffersService.GetAllOffers().FirstOrDefault(s => s.Id == offerId);

            var viewModel = new ViewOfferModel()
            {
                User = this.usersService.GetAllUsers().FirstOrDefault(),
              
                OfferName = offer.OfferName,
                OfferDescription = offer.OfferDescription,
                OfferPrice = offer.OfferPrice,
                RoleID = offer.RoleID

            };
            return View(viewModel);
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ViewOffer(ViewOfferModel model)
        {
           
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            //Find Offers By Role
            var offer = this.specialOffersService.GetAllOffers().FirstOrDefault(o => o.RoleID == user.Role);


            if (this.ModelState.IsValid)
            {
             
                user.SpecialOfferId  = offer.SpecialOffersID;//saves the record about the first offer's id

                this.usersService.Edit(user);
            };
            return this.RedirectToAction("UserHomePage", "Users");

        }

        public IActionResult SpecialOffers(SpecialOffersModel model)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            SpecialOffersModel inputModel = new SpecialOffersModel()
            {
                 
            OfferName = model.OfferName,
                OfferDescription = model.OfferDescription,
                OfferPrice = model.OfferPrice,
                RoleID = model.RoleID,
                //Getting the current user role
                Role = user.Role
            };

            
            var offers = new List<SpecialOffers>();
           
            if (model != null)
            {
                bool Role = !String.IsNullOrEmpty(inputModel.Role);
                offers = specialOffersService.GetAllOffers().ToList();
                if(Role)
                offers = offers.Where(u => u.RoleID == inputModel.Role).ToList();
                
            }


            // If there is no input the list is empty (No offers are found)
            if (offers == null)
            {
                return View(inputModel);
            }
            else
            {
            //Find Offers By Role
            var offer = this.specialOffersService.GetAllOffers().FirstOrDefault(o => o.RoleID == user.Role);

            return View(new SpecialOffersModel()
                {
                OfferName = model.OfferName,
                OfferDescription = model.OfferDescription,
                OfferPrice = model.OfferPrice,
                RoleID = model.RoleID,
                Offers = offers
                
            });
                
            }
        }


        [HttpGet]
        public IActionResult ChangeInformation(string returnUrl = null)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);

            var card = this.cardService
               .GetAllCards().FirstOrDefault(c => c.UserId == user.Id);//For Null Card Data there is an Exception for NullReferenceException   

            var viewModel = new ChangeInformationModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                Role = user.Role,
                City = user.City,
               CardNumber = card.CardNumber,
             
               

            };          

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeInformation(ChangeInformationModel model)
        {
            var user = this.usersService.GetAllUsers().FirstOrDefault(x => x.Id == GetCurrentUserId());
            var card = this.cardService
             .GetAllCards().FirstOrDefault(c => c.UserId == user.Id); //service should be witten again
            if (this.ModelState.IsValid)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Gender = model.Gender;
                user.Role = model.Role;
                user.City = model.City;    
                card.CardNumber = model.CardNumber;
             

                 this.usersService.Edit(user);
                this.cardService.Edit(card);
            } 
                
            return this.RedirectToAction("UserHomePage", "Users");
        }


        //===============for QR code generator=============================
        private static Byte[] BitmapToBytes(Bitmap img)
        {
       
            using (MemoryStream stream = new MemoryStream())
            {
                
        img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
            

        }
       // [HttpGet]
        public IActionResult MyCard()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MyCard(string qrText)
        {
            var user = this.usersService.GetAllUsers().FirstOrDefault(x => x.Id == GetCurrentUserId());

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText,
            QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            //The size of QR code image
            Bitmap qrCodeImage = qrCode.GetGraphic(10);
            return View(BitmapToBytes(qrCodeImage));
        }


        public IActionResult MyInvoices(MyInvoicesModel model)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            var order = this.orderService.GetAllOrders().FirstOrDefault(o => o.UserId == userId);
          
            MyInvoicesModel inputModel = new MyInvoicesModel()
            {

                FirstName = model.FirstName,
                LastName = model.LastName,
                SpecialOfferName = model.SpecialOfferName,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                UserId = user.Id
                
            };


            var myInvoices = new List<MyInvoices>();
            
            if (model != null)
            {
                bool UserId = !String.IsNullOrEmpty(inputModel.UserId);
                myInvoices = myInvoicesService.GetAllInvoices().ToList();
                if (UserId)
                    myInvoices = myInvoices.Where(u => u.UserId == inputModel.UserId).ToList();

            }

            if (myInvoices == null)
            {
                return View(inputModel);
            }
            else
            {
                var invoices = this.myInvoicesService.GetAllInvoices().FirstOrDefault(i => i.UserId == user.Id);

                return View(new MyInvoicesModel()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    SpecialOfferName = model.SpecialOfferName,
                    StartDate = model.StartDate,
                    UserId = model.UserId,
                    EndDate = model.EndDate,
                    MyInvoices = myInvoices

                });

            }
        }
       
        public IActionResult ViewInvoice(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("ViewInvoice", "Users");
            }
           int invoiceId = id;
             
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            var offer = this.specialOffersService.GetAllOffers().FirstOrDefault(s => s.SpecialOffersID == user.SpecialOfferId);
            var invoice = this.myInvoicesService.GetAllInvoices().FirstOrDefault(i => i.Id == invoiceId);
            
            var viewModel = new ViewInvoiceModel()
            {
               
                Id = invoice.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                OfferName = offer.OfferName,
                OfferPrice = offer.OfferPrice,
                TransportKind = invoice.TransportKind,
                TransportType = invoice.TransportType,
                TransportNumber = invoice.TransportNumber,
                StartDate = invoice.StartDate,
                UserId = invoice.UserId,
                EndDate = invoice.EndDate,
                StandartPrice = invoice.StandartPrice

             

        };
            return View(viewModel);
        }
        public IActionResult ChargeCard()
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            var order = this.orderService.GetAllOrders().FirstOrDefault(o => o.UserId == userId);
            var card = this.cardService.GetAllCards().FirstOrDefault(c => c.UserId == userId);
            var myInvoice = this.myInvoicesService.GetAllInvoices().FirstOrDefault(i => i.UserId == userId);

            //var viewModel = new ChargeCardModel()
            //{
            //    TransportKind = myInvoice.TransportKind,

            //    // Order = new Order()

            //};

            if (card.StartDate != DateTime.Today || card.EndDate.AddDays(-3) != DateTime.Today)
            {
                if(myInvoice.TransportKind == "All Kind")
                {
                    ViewData["InfoNotTextAll"] = "Last time you've been charged your card for All Kind Option now you can get 20% off if you select this option.";
                }
                else
                {
                    ViewData["InfoNotTextOne"] = "Last time you've been charged your card for One Kind Option now you can get 20% off if you select this option.";
                }
                if (order == null)//For All Kind Createing Order
                {
                    var Order = new Order
                    {
                        TransportKind = "All Kind",
                        UserId = GetCurrentUserId(),
                        StartDate = DateTime.Today,
                        EndDate = DateTime.Today,
                        StandartPrice = 50
                        
                    };
                   
                   
                    this.orderService.Add(Order);

                  
                };
               
            }
            //if (myInvoice != null && order != null)
            //    {
            //        if (order.TransportKind == "All Kind" && myInvoice.TransportKind == "All Kind")
            //        {
            //            order.StandartPrice = 50 - (50 * 0.2);
            //            this.orderService.Edit(order);
            //        }

            //    }

            ViewData["Error"] = "You've already Charged your card!";

            return View();
        }
        //[HttpGet]
        public IActionResult ChargeCardOneKind(string returnUrl = null)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
         

            var viewModel = new ChargeCardOneKindModel()
            {
                City = user.City,
              
              // Order = new Order()

            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChargeCardOneKind(ChargeCardOneKindModel model)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            var order = this.orderService.GetAllOrders().FirstOrDefault(o => o.UserId == userId);
            var myInvoice = this.myInvoicesService.GetAllInvoices().FirstOrDefault(i => i.UserId == userId);

            if (this.ModelState.IsValid)
            {
               

                order.TransportKind = model.TransportKind = "One Kind";
                order.UserId = GetCurrentUserId();
                order.TransportType = model.Order.TransportType;
                order.TransportNumber = model.TransportNumber;
                order.StartDate = DateTime.Today;
                order.EndDate = DateTime.Today;
                order.StandartPrice = 11;
                // model.Order.Duration = 1;

                if (myInvoice.TransportKind == "One Kind" && order.TransportKind == "One Kind")
                {
                    order.StandartPrice = 11 - 0.2;
                }

                this.orderService.Edit(order);

               
            }
 return this.RedirectToAction("ViewOrder", "Users");
            
        }
        public IActionResult ViewOrder(string returnUrl = null)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            var order = this.orderService.GetAllOrders().FirstOrDefault(o => o.UserId == userId);
            var offer = this.specialOffersService.GetAllOffers().FirstOrDefault(s => s.SpecialOffersID == user.SpecialOfferId);
            var myInvoice = this.myInvoicesService.GetAllInvoices().FirstOrDefault(i => i.UserId == userId);
           
             if (myInvoice != null && order != null)
            {
                if (order.TransportKind == "All Kind" && myInvoice.TransportKind == "All Kind")
                {
                    order.StandartPrice = 50 - (50 * 0.2);
                    this.orderService.Edit(order);
                }

            }
           
                var viewModel = new ViewOrderModel()
                {

                    TransportKind = order.TransportKind,
                    TransportType = order.TransportType,
                    TransportNumber = order.TransportNumber,
                    StartDate = order.StartDate,
                    EndDate = order.EndDate,
                    StandartPrice = order.StandartPrice,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    SpecialOfferID = user.SpecialOfferId,
                    OfferName = offer.OfferName,
                    OfferPrice = offer.OfferPrice,
                    Duration = order.Duration
                   

        };
            
            return View(viewModel);
           
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ViewOrder(ViewOrderModel model)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            var order = this.orderService.GetAllOrders().FirstOrDefault(o => o.UserId == userId);
            var myInvoice = this.myInvoicesService.GetAllInvoices().FirstOrDefault(i => i.UserId == userId);
            var offer = this.specialOffersService.GetAllOffers().FirstOrDefault(o => o.SpecialOffersID == user.SpecialOfferId);

               
            if (this.ModelState.IsValid)
            {
               
                order.Duration = model.Duration;

                this.orderService.Edit(order);

                if (user.SpecialOfferId == 0)
                {
                    order.StandartPrice = order.StandartPrice * order.Duration;
                    this.orderService.Edit(order);

                }
                else
                {
                    order.StandartPrice = offer.OfferPrice * order.Duration;
                    this.orderService.Edit(order);
                }
                return this.RedirectToAction("PaymentMethodPage", "Users");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult AddCard(string returnUrl = null)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            

            var viewModel = new AddCardModel()
            {
                
                Card = new Card()
                
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCard(AddCardModel model)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            //var myInvoice = this.myInvoicesService.GetAllInvoices().FirstOrDefault(i => i.UserId == userId);
           // var myInvoices = this.myInvoicesService.GetAllInvoices().FirstOrDefault(i => i.Id == userId);

            if (this.ModelState.IsValid)
            {
               
                model.Card.UserId = GetCurrentUserId();
                model.Card.CardNumber = model.Card.CardNumber;
                model.Card.StartDate = DateTime.Today;
                model.Card.EndDate = DateTime.Today.AddDays(6);
                model.Card.StandartPrice = 0.0;

                var MyInvoices = new MyInvoices
                {
                    UserId = GetCurrentUserId(),
                    TransportKind = "All Kind",
                    TransportType = "",
                    TransportNumber = "",
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(6),
                    StandartPrice = 0,
                    Duration = 0

                };
                var SpecialOffers = new SpecialOffers
                {
                    OfferName = "",
                    OfferPrice = 0
                };
                this.specialOffersService.Add(SpecialOffers);
                this.myInvoicesService.Add(MyInvoices);

                ViewData["Message"] = "Congratularions, You have 6 Days Free for All Kind Ttansports!";

                this.cardService.Add(model.Card);

                return this.RedirectToAction("UserHomePage", "Users");
            }

            return View(model);
        }

       // [HttpGet]
        public IActionResult PaymentMethodPage(string returnUrl = null)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            var order = this.orderService.GetAllOrders().FirstOrDefault(o => o.UserId == userId);
            var card = this.cardService.GetAllCards().FirstOrDefault(c => c.UserId == userId);
            var offer = this.specialOffersService.GetAllOffers().FirstOrDefault(s => s.SpecialOffersID == user.SpecialOfferId);

            var viewModel = new PaymentMethodPageModel()
            {
                
                FirstName = user.FirstName,
                LastName = user.LastName,
                SpecialOfferId = user.SpecialOfferId,
                Duration = order.Duration,
                OfferPrice = offer.OfferPrice,
                //StandartPrice = card.StandartPrice,
                StandartPrice = order.StandartPrice,


                MyInvoices = new MyInvoices()
                
               

            };
           

                return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PaymentMethodPage(PaymentMethodPageModel model)
        {
           string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            var card = this.cardService.GetAllCards().FirstOrDefault(c => c.UserId == userId);
            var order = this.orderService.GetAllOrders().FirstOrDefault(or => or.UserId == userId);
           
            var MyInvoices = new MyInvoices
            {
                UserId = order.UserId,
                TransportKind = order.TransportKind,
                TransportType = order.TransportType,
                TransportNumber = order.TransportNumber,
                StartDate = order.StartDate,
                EndDate = order.StartDate.AddMonths(order.Duration),
                StandartPrice = order.StandartPrice,
                Duration = order.Duration

            };
            this.myInvoicesService.Add(MyInvoices);
            if (this.ModelState.IsValid)
            {
               
                card.StartDate = DateTime.Today;
                card.EndDate = DateTime.Today.AddMonths(order.Duration);

                this.cardService.Edit(card);

                //Sending email notification
                //==============================================================
              

                    var message = new MimeMessage();

                    message.From.Add(new MailboxAddress("City Transport", "citytransportfinalproject@gmail.com"));

                    message.To.Add(new MailboxAddress("", user.Email));

                    message.Subject = "Transport Card Successfully Charged";



                    message.Body = new TextPart("plain")
                    {
                        Text = "Hi " + user.FirstName + " " + user.LastName + ", " +
                        "\n\n" +
                        "You've successfully Charged your Transport Card!" +
                        "\n" +
                        "For the Period:" + card.StartDate + " - " + card.EndDate +
                        "\n\n" +
                        "Regards," +
                        "\n" +
                        "City Transport"
                    };


                    using (var client = new MailKit.Net.Smtp.SmtpClient(new ProtocolLogger("smtp.log")))
                    {

                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                        client.Connect("smtp.gmail.com", 587, false);

                        client.Authenticate("citytransportfinalproject@gmail.com", "CityTransport1@");

                        client.Send(message);

                        client.Disconnect(true);
                    
                }

                
                this.orderService.Delete(order);
                
                return this.RedirectToAction("UserHomePage", "Users");
                }
            
           
            return View(model);
        }
    }
}
