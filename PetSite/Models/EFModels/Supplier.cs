﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PetSite.Models.EFModels
{
    public partial class Supplier
    {
        public Supplier()
        {
            Purchases = new HashSet<Purchase>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Mobile { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}