﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PetSite.Models.EFModels
{
    public partial class Purchase
    {
        public Purchase()
        {
            PurchaseItems = new HashSet<PurchaseItem>();
        }

        public int PurchaseId { get; set; }
        public int SupplierId { get; set; }
        public bool Status { get; set; }
        public decimal Total { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}