using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FutMobile.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "O campo Email é requerido.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Nome é requerido.")]
        [StringLength(100, ErrorMessage = "O campo Nome deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Login é requerido.")]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo Senha é requerido.")]
        [StringLength(100, ErrorMessage = "A senha deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Lembrar deste navegador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo Senha é requerido.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo Login é requerido.")]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Display(Name = "Lembrar-me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O campo Login é requerido.")]
        [StringLength(100, ErrorMessage = "O campo Login deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo Email é requerido.")]
        [EmailAddress(ErrorMessage = "O campo Email não contém um e-mail válido.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é requerido.")]
        [StringLength(100, ErrorMessage = "A senha deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "O campo Nome é requerido.")]
        [StringLength(100, ErrorMessage = "O campo Nome deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        [Display(Name = "Nome")]
        public string Name { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "O campo Email é requerido.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é requerido.")]
        [StringLength(100, ErrorMessage = "A Senha deve ter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não coincidem.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "O campo Email é requerido.")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }

    public class ContactViewModel
    {
        [Required(ErrorMessage = "O campo Nome é requerido.")]
        [StringLength(100, ErrorMessage = "O campo Nome deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Sobrenome é requerido.")]
        [StringLength(100, ErrorMessage = "O campo Nome deve ter no mínimo {2} caracteres.", MinimumLength = 3)]
        [Display(Name = "Sobrenome")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O campo Email é requerido.")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Mensagem é requerido.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Mensagem")]
        public string Mensagem { get; set; }

        [Required(ErrorMessage = "O campo DDD é requerido.")]
        [Display(Name = "Mensagem")]
        public int DDD { get; set; }

        [Required(ErrorMessage = "O campo Telefone é requerido.")]
        [Display(Name = "Mensagem")]
        public int Tel { get; set; }
    }
}
