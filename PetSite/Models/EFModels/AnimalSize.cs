﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PetSite.Models.EFModels
{
    public partial class AnimalSize
    {
        public AnimalSize()
        {
            Adoptions = new HashSet<Adoption>();
        }

        public int AnimalSizeId { get; set; }
        public string AnimalSize1 { get; set; }

        public virtual ICollection<Adoption> Adoptions { get; set; }
    }
}