using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RockyDemo.Models
{
    public class Category
    {
        /*the Key data annotations tells entity framework that the Id column is an identity column and primary key*/
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        //see https://docs.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-5.0#the-label-tag-helper
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Display Order for category must be greater than 0")]
        //MaxValue is the largest possivbile value for an int that the system can generate
        public int DisplayOrder { get; set; }
    }
}
