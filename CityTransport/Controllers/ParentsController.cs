﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CityTransport.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using CityTransport.Services.Abstraction;
using CityTransport.Data.Models;
using CityTransport.Models.ParentModels;
using CityTransport.Services;
using System.IO;
using System.Drawing;
using QRCoder;

namespace CityTransport.Controllers
{
    public class ParentsController : Controller
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

        public ParentsController(
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

        public IActionResult ParentUserHomePage(string id)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            var card = this.cardService.GetAllCards().FirstOrDefault(c => c.UserId == userId);

            usersService.Edit(user);
            if (card == null)
            {
                return this.RedirectToAction("ParentAddCard", "Parents");
            }
            var viewModel = new ParentUserHomePageModel()
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
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ParentViewOffer(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("ViewOffer", "Parents");
            }
            int offerId = id;
            // var offers = specialOffersService.GetOffersById(offerId);

            //string userRole 
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            //Find Offers By OfferId
            var offer = this.specialOffersService.GetAllOffers().FirstOrDefault(o => o.Id == offerId);

            var viewModel = new ParentViewOfferModel()
            {
                User = this.usersService.GetAllUsers().FirstOrDefault(),
                //SpecialOffers = user.Role,
                OfferName = offer.OfferName,
                OfferDescription = offer.OfferDescription,
                OfferPrice = offer.OfferPrice,
                RoleID = offer.RoleID

            };
            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ParentViewOffer(ParentViewOfferModel model)
        {

            //  var offers = specialOffersService.GetOffersById(id);

            //string userRole 
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            //Find Offers By Role
            var offer = this.specialOffersService.GetAllOffers().FirstOrDefault(o => o.RoleID == user.Role);


            if (this.ModelState.IsValid)
            {

                user.SpecialOfferId = offer.SpecialOffersID;//saves the record about the first offer's id

                this.usersService.Edit(user);
            };
            return this.RedirectToAction("ParentUserHomePage", "Parents");

        }
        public IActionResult ParentSpecialOffers(ParentSpecialOffersModel model)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            ParentSpecialOffersModel inputModel = new ParentSpecialOffersModel()
            {

                OfferName = model.OfferName,
                OfferDescription = model.OfferDescription,
                OfferPrice = model.OfferPrice,
                RoleID = model.RoleID,
                //Getting the current user role
                Role = user.Role
            };


            var offers = new List<SpecialOffers>();
            //return View(viewModel);
            if (model != null)
            {
                bool Role = !String.IsNullOrEmpty(inputModel.Role);
                offers = specialOffersService.GetAllOffers().ToList();
                if (Role)
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

                return View(new ParentSpecialOffersModel()
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
        public IActionResult ParentChangeInformation(string returnUrl = null)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);

            var card = this.cardService
               .GetAllCards().FirstOrDefault(c => c.UserId == user.Id);//For Null Card Data there is an Exception for NullReferenceException   

            var viewModel = new ParentChangeInformationModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                Role = user.Role,
                City = user.City,
                CardNumber = card.CardNumber
                // CardNumber = user.CardNumber

            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ParentChangeInformation(ParentChangeInformationModel model)
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
                // user.CardNumber = model.CardNumber;
                user.City = model.City;

                card.CardNumber = model.CardNumber;
                // card.UserID = user.Id;

                this.usersService.Edit(user);
                this.cardService.Edit(card);
            }

            return this.RedirectToAction("ParentUserHomePage", "Parents");
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

        public IActionResult ParentMyCard()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ParentMyCard(string qrText)
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
        public IActionResult ParentMyInvoices(ParentMyInvoicesModel model)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            ParentMyInvoicesModel inputModel = new ParentMyInvoicesModel()
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

                return View(new ParentMyInvoicesModel()
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
        public IActionResult ParentViewInvoice(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("ParentViewInvoice", "Parents");
            }
            int invoiceId = id;

            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            // var offer = this.specialOffersService.GetAllOffers().FirstOrDefault(o => o.Id == user.SpecialOfferId);
            var invoice = this.myInvoicesService.GetAllInvoices().FirstOrDefault(i => i.Id == invoiceId);

            var viewModel = new ParentViewInvoiceModel()
            {

                Id = invoice.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                //OfferName = offer.OfferName,
                //OfferPrice = offer.OfferPrice,
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
       

       
        [HttpGet]
        public IActionResult ParentAddCard(string returnUrl = null)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);


            var viewModel = new ParentAddCardModel()
            {

                Card = new Card()

            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ParentAddCard(ParentAddCardModel model)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);

            if (this.ModelState.IsValid)
            {

                model.Card.UserId = GetCurrentUserId();
                model.Card.CardNumber = model.Card.CardNumber;
                model.Card.StartDate = DateTime.Today;
                model.Card.EndDate = DateTime.Today.AddDays(6);
                model.Card.StandartPrice = 0.0;

                cardService.Add(model.Card);

                return this.RedirectToAction("ParentUserHomePage", "Parents");
            }

            return View(model);
        }
        public IActionResult ParentChildrenCard(string returnUrl = null)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            var children = this.parentService.GetAllParents().FirstOrDefault(c => c.UserId == userId);
          

           
                var viewModel = new ParentChildrenCardModel()
                {

                    Parent = new Parent()

                };
          
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ParentChildrenCard(ParentChildrenCardModel model)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            // var parent = this.parentService.GetAllParents().FirstOrDefault(p => p.UserId == userId);
            ParentChildrenCardModel inputModel = new ParentChildrenCardModel()
            {

                FirstName = model.FirstName,
                LastName = model.LastName,
                UserId = user.Id

            };


            var children = new List<Parent>();
            if (model != null)
            {
                bool UserId = !String.IsNullOrEmpty(inputModel.UserId);
                children = parentService.GetAllParents().ToList();
                if (UserId)
                    children = children.Where(u => u.UserId == inputModel.UserId).ToList();

            }

            if (children.Count() < 2)
            {
                if (this.ModelState.IsValid)
                {
                    var Parent = new Parent()
                    {
                        UserId = GetCurrentUserId(),
                        ChildrenCardNumber = model.ChildrenCardNumber,
                        ChildrenFirstName = "Dani",
                        ChildrenLastName = "Dimov"
                    };

                    parentService.Add(Parent);
                    //TempData["Success"] = "You've successfully added your children!";

                    return this.RedirectToAction("ParentViewChildren", "Parents");
                }
            }
            ViewData["Error"] = "You've already added two children!";

            return View(model);
        }
        public IActionResult ParentViewChildren(string returnUrl = null)
        {
            string userId = GetCurrentUserId();
            var user = this.usersService.GetAllUsers().FirstOrDefault(u => u.Id == userId);
            var parent = this.parentService.GetAllParents().FirstOrDefault(p => p.UserId == userId);//Finding the record for current user
            var children = this.cardService.GetAllCards().FirstOrDefault(c => c.CardNumber == parent.ChildrenCardNumber);//Finding ChildrenCardNumber in Card Table
            var names = this.usersService.GetAllUsers().FirstOrDefault(n => n.Id == children.UserId);

            
                var viewModel = new ParentViewChildrenModel()
                {

                    ChildrenFirstName = parent.ChildrenFirstName = names.FirstName,
                    ChildrenLastName = parent.ChildrenLastName = names.LastName,
                    ChildrenId = parent.ChildrenId = children.UserId,
                    ChildrenCardNumber = parent.ChildrenCardNumber


                };
                parentService.Edit(parent);
            
            return View(viewModel);

           
        }

        public IActionResult ParentChargeCard()
        {

            return View();
        }
        public IActionResult ParentPaymentMethodPage()
        {
            return View();
        }
       


    }
}
