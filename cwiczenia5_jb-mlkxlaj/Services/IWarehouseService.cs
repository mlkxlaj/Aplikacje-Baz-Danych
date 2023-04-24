using Zadanie5.DTOs;

namespace Zadanie5.Services
{
    public interface IWarehouseService
    {
        public int AddProduct(ProductDTO product);


        public int AddProductProcedure(ProductDTO product);
    }
}
