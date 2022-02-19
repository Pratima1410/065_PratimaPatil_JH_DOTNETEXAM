using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationExam.Models
{
    public class product
    {
        [Required]
        [DisplayName("Product Id")]
        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [Required]
        [DisplayName("Rate")]
        public double Rate { get; set; }
        [Required]
        [StringLength(200)]
        [DisplayName("Description")]
        public string Description { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
    }
}