namespace catalog.Api.Exeptios
{
    public class ProductNotFoundExeption : Exception
    {
        public ProductNotFoundExeption() : base("product not found!")
        {

        }
    }
}
