using System.Data.SqlClient;
using Zadanie4.Model;

namespace Zadanie4.Service;

public class AnimalsService
{
    private readonly SqlConnection _connection;

    public AnimalsService()
    {
        _connection = new SqlConnection("Data Source=db-mssql16;Initial Catalog=s24427;Integrated Security=True");
        _connection.Open();
    }

    public List<Animal> GetAnimals(string param)
    {
        if (!testParam(param))
        {
            return null;
        }
        List<Animal> animals = new List<Animal>();
        using SqlCommand command = new SqlCommand($"SELECT * FROM Animal order by {param}", _connection);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            animals.Add(new Animal(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(),
                reader[3].ToString(), reader[4].ToString()));
        }
        reader.Close();
        return animals;
    }

    public void PutAnimal(InsertAnimal param)
    {
        string sql = "INSERT INTO ANIMAL VALUES (@Name, @Description, @Category, @Area)";
        using SqlCommand command = new SqlCommand(sql, _connection);

        command.Parameters.AddWithValue("@Name", param.name);
        command.Parameters.AddWithValue("@Description", param.description);
        command.Parameters.AddWithValue("@Category", param.category);
        command.Parameters.AddWithValue("@Area", param.area);
        command.ExecuteNonQuery();
    }

    public bool PutAnimal(int id, InsertAnimal animal)
    {
        var animalToUpdate = GetAnimal(id);
        if (animalToUpdate.Equals(null))
        {
            return false;
        }
        
        string sql = "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @Id";
        using var command = new SqlCommand(sql, _connection);
        command.Parameters.AddWithValue("@Name", animal.name);
        command.Parameters.AddWithValue("@Description", animal.description);
        command.Parameters.AddWithValue("@Category", animal.category);
        command.Parameters.AddWithValue("@Area", animal.area);
        command.Parameters.AddWithValue("@Id", id);
        command.ExecuteNonQuery();
        return true;
    }

    public bool DeleteAnimal(int id)
    {
        var animalToDelete = GetAnimal(id);
        if (animalToDelete.Equals(null))
        {
            return false;
        }
        else
        {
            string sql = "DELETE FROM ANIMAL WHERE IdAnimal = @Id";
            using var command = new SqlCommand(sql, _connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
            return true;
        }
    }

    private object GetAnimal(int id)
    {
        {
            string sql = "SELECT * FROM Animal WHERE IdAnimal = @Id";
            using var command = new SqlCommand(sql, _connection);
            command.Parameters.AddWithValue("@Id", id);
            var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            var animal = new Animal
            (
                (int)reader["IdAnimal"],
                (string)reader["Name"],
                (string)reader["Description"],
                (string)reader["Category"],
                (string)reader["Area"]
            );
            reader.Close();
            return animal;
        }
    }

    private bool testParam(String str)
    {
        if (str == "idanimal" || str == "name" || str == "description" || str == "category" ||
            str == "area")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}