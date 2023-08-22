using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcTraining4.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter email")]
        public string email{ get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter password")]
        public string password{ get; set; }
    }
}