using System;

namespace AspNetRestApiContainer.Application.Views
{
    public class ProductView
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public decimal Price { get; set; }
    }
}
