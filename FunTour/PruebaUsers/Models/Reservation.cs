﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebaUsers.Models
{
    public partial class Reservation
    {
        [Key]
        public int Id_Reservation { get; set; }

        public virtual TravelPackage TravelPackage { get; set; }
        public virtual UserDetails Client { get; set; }
        public virtual ReservedRoom ReservedRoom { get; set; }
        public virtual ReservedSeat ReservedSeat { get; set; }
        public virtual BusReservedSeat BusReservedSeat { get; set; }
        public virtual ReservedTicket ReservedTicket { get; set; }
    }
}