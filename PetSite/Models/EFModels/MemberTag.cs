﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PetSite.Models.EFModels
{
    public partial class MemberTag
    {
        public int MemberTagId { get; set; }
        public int MemberId { get; set; }
        public int TagId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Tag Tag { get; set; }
    }
}