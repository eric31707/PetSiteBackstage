﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PetSite.Models.EFModels
{
    public partial class Payment
    {
        public Payment()
        {
            ProductOrders = new HashSet<ProductOrder>();
        }

        public int PaymentId { get; set; }
        public string PaymentMethod { get; set; }

        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
}