using System.Data;
using System.Data.SqlClient;
using Zadanie5.DTOs;

namespace Zadanie5.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IConfiguration _configuration;

        public WarehouseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int AddProduct(ProductDTO product)
        {
            var connectionString = _configuration.GetConnectionString("Database");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.Transaction = transaction;
                        command.CommandText = "SELECT COUNT(*) FROM Product WHERE IdProduct = @IdProduct";
                        command.Parameters.AddWithValue("@IdProduct", product.IdProduct);
                        var productCount = (int)command.ExecuteScalar();

                        command.CommandText = "SELECT COUNT(*) FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
                        command.Parameters.AddWithValue("@IdWarehouse", product.IdWarehouse);
                        var warehouseCount = (int)command.ExecuteScalar();

                        if (productCount == 0 || warehouseCount == 0)
                        {
                            transaction.Rollback();
                            return -1;
                        }
                        
                        command.CommandText =
                            "SELECT COUNT(*) FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt <= @CreatedAt";
                        command.Parameters.AddWithValue("@Amount", product.Amount);
                        command.Parameters.AddWithValue("@CreatedAt", product.CreatedAt);
                        var orderCount = (int)command.ExecuteScalar();

                        if (orderCount == 0)
                        {
                            transaction.Rollback();
                            return -2;
                        }
                        
                        command.CommandText ="SELECT COUNT(*) FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt <= @CreatedAt AND FulfilledAt IS NOT NULL";
                        var orderFulfilledCount = (int)command.ExecuteScalar();
                        Console.WriteLine(orderFulfilledCount);
                        if (orderFulfilledCount > 0)
                        {
                            transaction.Rollback();
                            return -3;
                        }
                        
                        command.CommandText =
                            "UPDATE [Order] SET FulfilledAt = @FulfilledAt WHERE IdOrder = (SELECT TOP 1 IdOrder FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt <= @CreatedAt)";
                        command.Parameters.AddWithValue("@FulfilledAt", DateTime.Now);
                        command.ExecuteNonQuery();
                        
                        command.CommandText =
                            "INSERT INTO Product_Warehouse (IdProduct, IdWarehouse, IdOrder, Amount, Price, CreatedAt) VALUES (@IdProduct, @IdWarehouse, (SELECT TOP 1 IdOrder FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt <= @CreatedAt), @Amount, @Price, @CreatedAt)";
                        command.Parameters.AddWithValue("@Price", 10);
                        command.ExecuteNonQuery();
                        
                        transaction.Commit();
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return 1;
        }

        public int AddProductProcedure(ProductDTO product)
        {
            var connectionString = _configuration.GetConnectionString("Database");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand("AddProductToWarehouse", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdProduct", product.IdProduct);
                        command.Parameters.AddWithValue("@IdWarehouse", product.IdWarehouse);
                        command.Parameters.AddWithValue("@Amount", product.Amount);
                        command.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);

                        var idParam = command.Parameters.Add("@IdProductWarehouse", SqlDbType.Int);
                        idParam.Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();
                        
                        var id = (int)idParam.Value;
                        
                        if (id == 0)
                        {
                            return 2;
                        }
                        return 1;
                    }
                }
        }
    }
}

