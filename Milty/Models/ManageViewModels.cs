﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Milty.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
        [Display(Name = "Имя")]
        public string Firstname { get; set; }
        [Display(Name = "Фамилия")]
        public string Lastname { get; set; }
        [Display(Name = "Уровень доступа")]
        public IList<string> AccessLevel { get; set; }
    }

    public class ListViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
        [Display(Name = "Имя")]
        public string Firstname { get; set; }
        [Display(Name = "Фамилия")]
        public string Lastname { get; set; }
        [Display(Name = "Уровень доступа")]
        public IList<string> AccessLevel { get; set; }

        public ListViewModel(string Id, string Email, string Firstname, string Lastname, IList<string> AccessLevel)
        {
            this.Id = Id;
            this.Email = Email;
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.AccessLevel = AccessLevel;
        }
    }

    public class EditUserModel
    {
        public string Id { get; set; }
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
        [Display(Name = "Имя")]
        public string Firstname { get; set; }
        [Display(Name = "Фамилия")]
        public string Lastname { get; set; }
        [Display(Name = "Уровень доступа")]
        public IList<string> AccessLevel { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public EditUserModel() {
        }

        public EditUserModel(string Id, string Email, string Firstname, string Lastname, IList<string> AccessLevel)
        {
            this.Id = Id;
            this.Email = Email;
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.AccessLevel = AccessLevel;
        }
    }

    public class NewListItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }

        public string GetSelected()
        {
            if (Selected)
            {
                return "selected=selected";
            }
            else
            {
                return "";
            }
        }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение нового пароля")]
        [Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение нового пароля")]
        [Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Номер телефона")]
        public string Number { get; set; }
    }

    public class UpdateProfileViewModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Код")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}