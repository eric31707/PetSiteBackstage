﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PetSite.Models.EFModels
{
    public partial class Product
    {
        public Product()
        {
            ProductImages = new HashSet<ProductImage>();
            ProductOrderItems = new HashSet<ProductOrderItem>();
            PurchaseItems = new HashSet<PurchaseItem>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public int CategoryId { get; set; }
        public bool Status { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int FlavorId { get; set; }
        public int Quantity { get; set; }
        public int SpeciesId { get; set; }

        public virtual ChildCategory Category { get; set; }
        public virtual Color Color { get; set; }
        public virtual Flavor Flavor { get; set; }
        public virtual Size Size { get; set; }
        public virtual Species Species { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductOrderItem> ProductOrderItems { get; set; }
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}