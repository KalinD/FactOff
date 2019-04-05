﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FactOff.Models;
using FactOff.Models.ViewModels;
using FactOff.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using FactOff.Attributes;

namespace FactOff.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// The service lets the AccountController communicate with the UsersService
        /// which communicates with the database.
        /// </summary>
        private readonly IUsersService service;

        /// <summary>
        /// Initializes a new instance of the AccountController class.
        /// It's being called by the StartUp class.
        /// </summary>
        /// <param name="service">The required service for the class.</param>
        public AccountController(IUsersService service)
        {
            this.service = service;
        }

        /// <summary>
        /// The action redirects to the Profile page in the Account folder.
        /// </summary>
        /// <returns>Rendered view to the response.</returns>
        [FactOffAuthorize]
        public IActionResult Profile()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [FactOffAuthorize]
        public IActionResult EditProfile()
        {
            var user = service.GetUserById(new Guid(HttpContext.Session.GetString("logeduser")));
            var model = new AccountViewModel
            {
                Name = user.Name,
                Email = user.Email
            };
            return View(model);
        }

        [FactOffAuthorize]
        [HttpPost]
        public IActionResult EditProfile(AccountViewModel request)
        {
            if (ModelState.IsValid)
            {
                byte[] image;
                using (var ms = new MemoryStream())
                {
                    request.ImageUploaded.CopyTo(ms);
                    image = ms.ToArray();
                }
                service.EditUser(new Guid(HttpContext.Session.GetString("logeduser")), request.Email, image, request.ImageUploaded.ContentType, request.Name, request.Password);
                return RedirectToAction("Index", "Home");
            }
            request.Password = null;
            return View(request);
        }

        [FactOffAuthorize]
        public FileStreamResult GetUserImage()
        {
            var user = service.GetUserById(new Guid(HttpContext.Session.GetString("logeduser")));
            Stream stream = new MemoryStream(user.Image);
            return new FileStreamResult(stream, user.ImageContentType);
        }

        /// <summary>
        /// The action redirects to the SavedPosted page in the Account folder.
        /// </summary>
        /// <returns>Rendered view to the response.</returns>
        [FactOffAuthorize]
        public IActionResult SavedPosted()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        /// <summary>
        /// The action redirects to the SignIn page in the Account folder.
        /// </summary>
        /// <returns>Rendered view to the response.</returns>
        public IActionResult SignIn()
        {
            if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString("logeduser")))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        /// <summary>
        /// Gets the reqired fields to sign in.
        /// </summary>
        /// <param name="requestModel">Model of the required fields to sign in.</param>
        /// <returns>Rendered view to the response.</returns>
        [HttpPost]
        public IActionResult SignIn(SignInViewModel requestModel)
        {
            if (ModelState.IsValid)
            {
                var userId = service.SignIn(requestModel.Email, requestModel.Password);
                if (userId != null)
                {
                    HttpContext.Session.SetString("logeduser", userId);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }
            return View();
        }

        /// <summary>
        /// The action redirects to the Index page in the Home folder.
        /// </summary>
        /// <returns>Rendered view to the response.</returns>
        [FactOffAuthorize]
        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("logeduser");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// The action redirects to the Registration page in the Account folder.
        /// </summary>
        /// <returns>Rendered view to the response.</returns>
        public IActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Gets the required fields to register.
        /// </summary>
        /// <param name="requestModel">Model of the required fields to register.</param>
        /// <returns>Rendered view to the response.</returns>
        [HttpPost]
        public IActionResult Registration(RegisterViewModel requestModel)
        {
            if (service.UserExists(requestModel.Email))
            {
                ModelState.AddModelError("", "There is already an account with this email.");
            }
            else
            {
                service.CreateUser(requestModel.Email, requestModel.Name, requestModel.Password);
                return RedirectToAction("SignIn", "Account");
            }
            return View();
        }
    }
}
