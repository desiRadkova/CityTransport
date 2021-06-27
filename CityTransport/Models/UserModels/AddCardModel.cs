﻿using CityTransport.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTransport.Models.UserModels
{
    public class AddCardModel
    {
        public User User { get; set; }
      
        public Card Card { get; set; }
        public int CardNumber { get; set; }
       

    }
}
