﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Application.UseCases.DTO
{
    public class CreateTableDto
    {
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
    }
}
