﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RazorWeb.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(0, 100)]
        public int DisplayOrder { get; set; }
    }
}
