/*
 * Copyright (c) Dominick Baier.  All rights reserved.
 * see license.txt
 */

using System;
using System.ComponentModel.DataAnnotations;

namespace Thinktecture.IdentityServer.Web.ViewModels
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Digite o endereço de email.")]
        [Display(Name = "UserName", ResourceType = typeof(Resources.SignInModel))]
        public string UserName { get; set; }
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Digite a senha da sua conta da Nobre.")]
        [Display(Name = "Password", ResourceType = typeof(Resources.SignInModel))]
        public string Password { get; set; }

        [Display(Name = "EnableSSO", ResourceType = typeof(Resources.SignInModel))]
        public bool EnableSSO { get; set; }

        private bool? isSigninRequest;

        public bool IsSigninRequest
        {
            get
            {
                if (isSigninRequest == null)
                {
                    isSigninRequest = !String.IsNullOrWhiteSpace(ReturnUrl);
                }
                return isSigninRequest.Value;
            }
            set
            {
                isSigninRequest = value;
            }
        }

        public string ReturnUrl { get; set; }

        public bool ShowClientCertificateLink { get; set; }
    }
}