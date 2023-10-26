using System;

namespace AspNetRestApiContainer.Application.Parameters.Commands
{
    public class ProductCreate
    {
        public string Number { get; set; }

        public decimal Price { get; set; }
    }
}
