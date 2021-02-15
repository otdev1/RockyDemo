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

        public string Name { get; set; }

        [DisplayName("Display Order")] 
        //see https://docs.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-5.0#the-label-tag-helper
        public int DisplayOrder { get; set; }
    }
}
