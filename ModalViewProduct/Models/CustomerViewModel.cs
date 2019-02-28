using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ModalViewProduct.Models
{
    public class CustomerViewModel
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("Customer Name")]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}