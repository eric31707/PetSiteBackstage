﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PetSite.Models.EFModels
{
    public partial class Room
    {
        public Room()
        {
            RoomCartItems = new HashSet<RoomCartItem>();
            RoomImages = new HashSet<RoomImage>();
            RoomOrderItems = new HashSet<RoomOrderItem>();
        }

        public int RoomId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RoomCartItem> RoomCartItems { get; set; }
        public virtual ICollection<RoomImage> RoomImages { get; set; }
        public virtual ICollection<RoomOrderItem> RoomOrderItems { get; set; }
    }
}