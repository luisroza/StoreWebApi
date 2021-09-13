using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopifyChallenge.WebAPI.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "Field {0} is in an invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "Field {0} is in an invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field {0} is mandatory")]
        [StringLength(100, ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }
    }

    public class UserTokenViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }

    public class LoginResponseViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenViewModel UserToken { get; set; }
    }

    public class ClaimViewModel
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
