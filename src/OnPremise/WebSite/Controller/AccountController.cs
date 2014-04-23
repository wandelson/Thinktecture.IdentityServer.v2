﻿/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license.txt
 */

using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Thinktecture.IdentityServer.Protocols;
using Thinktecture.IdentityServer.Repositories;
using Thinktecture.IdentityServer.Web.ViewModels;

namespace Thinktecture.IdentityServer.Web.Controllers
{
    public class AccountController : AccountControllerBase
    {
        public AccountController()
            : base()
        { }

        public AccountController(IUserRepository userRepository, IConfigurationRepository configurationRepository)
            : base(userRepository, configurationRepository)
        { }

        // shows the signin screen
        public ActionResult SignIn(string returnUrl, bool mobile = false)
        {
            // you can call AuthenticationHelper.GetRelyingPartyDetailsFromReturnUrl to get more information about the requested relying party

            var vm = new SignInModel()
            {
                ReturnUrl = returnUrl,
                ShowClientCertificateLink = ConfigurationRepository.Global.EnableClientCertificateAuthentication
            };

            if (mobile) vm.IsSigninRequest = true;
            return View(vm);
        }

        // handles the signin
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult SignIn(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                if (UserRepository.ValidateUser(model.UserName, model.Password))
                {
                    // establishes a principal, set the session cookie and redirects
                    // you can also pass additional claims to signin, which will be embedded in the session token

                    return SignIn(
                        model.UserName,
                        AuthenticationMethods.Password,
                        model.ReturnUrl,
                        model.EnableSSO,
                        ConfigurationRepository.Global.SsoCookieLifetime);
                }
                ModelState.AddModelError("", Resources.AccountController.IncorrectCredentialsNoAuthorization);
            }

            model.ShowClientCertificateLink = ConfigurationRepository.Global.EnableClientCertificateAuthentication;
            return View(model);
        }

        // handles client certificate based signin
        public ActionResult CertificateSignIn(string returnUrl)
        {
            if (!ConfigurationRepository.Global.EnableClientCertificateAuthentication)
            {
                return new HttpNotFoundResult();
            }

            var clientCert = HttpContext.Request.ClientCertificate;

            if (clientCert != null && clientCert.IsPresent && clientCert.IsValid)
            {
                string userName;
                if (UserRepository.ValidateUser(new X509Certificate2(clientCert.Certificate), out userName))
                {
                    // establishes a principal, set the session cookie and redirects
                    // you can also pass additional claims to signin, which will be embedded in the session token

                    return SignIn(
                        userName,
                        AuthenticationMethods.X509,
                        returnUrl,
                        false,
                        ConfigurationRepository.Global.SsoCookieLifetime);
                }
            }

            return View("Error");
        }

    }
}