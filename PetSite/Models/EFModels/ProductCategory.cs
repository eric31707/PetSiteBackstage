﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PetSite.Models.EFModels
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            ParentCategories = new HashSet<ParentCategory>();
        }

        public int ProductCategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ParentCategory> ParentCategories { get; set; }
    }
}