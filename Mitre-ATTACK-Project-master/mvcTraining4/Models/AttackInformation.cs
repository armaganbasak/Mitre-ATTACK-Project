//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mvcTraining4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class AttackInformation
    {
        public int ATTACK_ID { get; set; }
        [Display(Name = "Attack Name:")]
        public string ATTACK_NAME { get; set; }
        [Display(Name = "Attack Details:")]
        public string ATTACK_DETAIL { get; set; }
        public int CUSTOMER_ID { get; set; }
    }
}
