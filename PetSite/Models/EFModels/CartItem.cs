﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PetSite.Models.EFModels
{
    public partial class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Cart Cart { get; set; }
    }
}