﻿using System;

namespace Cataolg.API.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {

        public ProductNotFoundException(Guid id) : base("Product", id)
        {
            
        }
    }
}
