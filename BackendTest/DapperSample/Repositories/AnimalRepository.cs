using Dapper;
using STGenetics.Core.Entities;
using STGenetics.Core.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;

namespace STGenetics.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly string _connectionString;
        public AnimalRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("sqlserver");
        }

        public async Task<IEnumerable<Animal>> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = "SELECT AnimalId, Name, Breed, BirthDate, Sex, Price, Status " +
                      "FROM Animal";

            var animals = await connection.QueryAsync<Animal>(sql);
            return animals;
        }

        public async Task<Animal> GetById(int animalId)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = $"SELECT AnimalId, Name, Breed, BirthDate, Sex, Price, Status " +
                      $"FROM Animal " +
                      $"WHERE AnimalId = @animalId";

            var animals = await connection.QueryFirstOrDefaultAsync<Animal>(sql, new { AnimalId = animalId });
            return animals;
        }

        public async Task<Animal> GetByFilter(string filter, string value)
        {
            using var connection = new SqlConnection(_connectionString);

            filter = filter.ToLower();
            if (filter.Contains("id"))
                filter = "AnimalId";
            else if (filter.Contains("name"))
                filter = "Name";
            else if (filter.Contains("sex"))
                filter = "Sex";
            else if (filter.Contains("status"))
                filter = "Status";
            else
                filter = string.Empty;

            var sql = $"SELECT AnimalId, Name, Breed, BirthDate, Sex, Price, Status FROM Animal WHERE {filter} = @{filter.ToLower()}";

            Animal animals = null;
            if (!string.IsNullOrEmpty(filter))
            {
                if (filter.Equals("AnimalId"))
                    animals = await connection.QueryFirstOrDefaultAsync<Animal>(sql, new { AnimalId = value });
                else if (filter.Equals("Name"))
                    animals = await connection.QueryFirstOrDefaultAsync<Animal>(sql, new { Name = value });
                else if (filter.Equals("Sex"))
                    animals = await connection.QueryFirstOrDefaultAsync<Animal>(sql, new { Sex = value });
                else if (filter.Equals("Status"))
                    animals = await connection.QueryFirstOrDefaultAsync<Animal>(sql, new { Status = value });
            }
            return animals;
        }

        public async Task<Animal> GetByOlderThanTwoYearsAndFemale()
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = $"SELECT AnimalId, Name, Breed, BirthDate, Sex, Price, Status" +
                      $"FROM Animal" +
                      $"WHERE DATEDIFF(year, BirthDate, GetDate()) >= 2" +
                      $"AND Sex = 'F'" +
                      $"ORDER BY Name";

            var animals = await connection.QueryFirstOrDefaultAsync<Animal>(sql);
            return animals;
        }

        public async Task<AnimalSexByQuantity> AnimalSexByQuantity()
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = $"SELECT Sex, COUNT(1) AS Quantity FROM Animal GROUP BY Sex";

            var animals = await connection.QueryFirstOrDefaultAsync<AnimalSexByQuantity>(sql);
            return animals;
        }

        public async Task<int> Create(Animal animal)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = @"INSERT INTO Animal (AnimalId, Name, Breed, BirthDate, Sex, Price, Status) 
                        VALUES (@AnimalId, @Name, @Breed, @BirthDate, @Sex, @Price, @Status)";

            var rows = await connection.ExecuteAsync(sql, animal);
            return rows;
        }

        public async Task<int> Update(Animal animal)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = @"UPDATE Animal 
                        SET Name = @Name, 
                            Breed = @Breed,
                            BirthDate = @BirthDate, 
                            Sex = @Sex, 
                            Price = @Price, 
                            Status = @Status
                        WHERE AnimalId = @AnimalId";

            var rows = await connection.ExecuteAsync(sql, animal);
            return rows;
        }

        public async Task<int> Delete(int animalId)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = @"DELETE FROM Animal 
                        WHERE AnimalId=@animalId";

            var rows = await connection.ExecuteAsync(sql, new { AnimalId = animalId });
            return rows;
        }
    }
}
