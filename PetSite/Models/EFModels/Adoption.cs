﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PetSite.Models.EFModels
{
    public partial class Adoption
    {
        public int AdoptionId { get; set; }
        public int MemberId { get; set; }
        public string PostType { get; set; }
        public string AdoptName { get; set; }
        public int AnimalGenderId { get; set; }
        public int AnimalTypeId { get; set; }
        public string AnimalColor { get; set; }
        public string AreaAddress { get; set; }
        public string AdoptTitle { get; set; }
        public string AdoptDescription { get; set; }
        public int AnimalSizeId { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public int? ContainNumber { get; set; }
        public bool? OpenAdopt { get; set; }
        public bool? Ligation { get; set; }
        public bool? Deworming { get; set; }
        public bool? Vaccination { get; set; }
        public bool? Triple { get; set; }
        public bool? AnimalChip { get; set; }
        public bool? Fiv { get; set; }
        public bool? FeLv { get; set; }

        public virtual Gender AnimalGender { get; set; }
        public virtual AnimalSize AnimalSize { get; set; }
        public virtual AnimailType AnimalType { get; set; }
        public virtual Member Member { get; set; }
    }
}