﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class DogDto
    {
        public string Name { get; set; } = string.Empty;
        public string BreedDog { get; set; }
        public int WeightDog { get; set; }
    }
}
