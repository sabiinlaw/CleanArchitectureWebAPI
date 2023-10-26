namespace AspNetRestApiContainer.Application.Parameters.Queries
{
    public class GetProductQuery : QueryParameter
    {
        public string Number { get; set; }
        public decimal Price { get; set; }
    }
}