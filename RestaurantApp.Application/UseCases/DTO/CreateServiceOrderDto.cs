﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class CreateServiceOrderDto
    {
        public int MenuItem { get; set; }
        public int MenuQuantity { get; set; }
        public int TableNumber { get; set; }

    }
}
