using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "�̸���")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "��Ī")]
        public string NickName { get; set; }
    }
}
