﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PetSite.Models.EFModels
{
    public partial class AnimailType
    {
        public AnimailType()
        {
            Adoptions = new HashSet<Adoption>();
        }

        public int AnimailTypeId { get; set; }
        public string AnimailType1 { get; set; }

        public virtual ICollection<Adoption> Adoptions { get; set; }
    }
}