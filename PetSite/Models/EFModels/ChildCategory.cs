﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PetSite.Models.EFModels
{
    public partial class ChildCategory
    {
        public ChildCategory()
        {
            Products = new HashSet<Product>();
        }

        public int ChildCategoryId { get; set; }
        public string Name { get; set; }
        public int ParentCategoryId { get; set; }

        public virtual ParentCategory ParentCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}