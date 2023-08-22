using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace mvcTraining4.Models
{
    public class EditCustomer
    {
        public int CUSTOMER_ID { get; set; }
        [Display(Name = "Name:")]
        [Required(ErrorMessage = "Please Enter name")]
        public string CUSTOMER_NAME { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Password:")]
        public string CUSTOMER_PASSWORD { get; set; }
        [Display(Name = "Confirm Password:")]
        [Compare("CUSTOMER_PASSWORD", ErrorMessage = " Passwords are dont match.")]
        public string confirmPassword { get; set; }
        [Required(ErrorMessage = "Please Enter Email Address")]
        public string CUSTOMER_EMAIL { get; set; }
    }
}