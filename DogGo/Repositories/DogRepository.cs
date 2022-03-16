using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class DogRepository //TODO: implement IDogRepository
    {
        private readonly IConfiguration _config;

        public DogRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Dog> GetAllDogs()
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, Name, OwnerId, Breed, Notes, ImageUrl
                        FROM Dog";

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Dog> dogs = new List<Dog>();
                        while (reader.Read())
                        {
                            Dog dog = new Dog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                            };
                            if (!reader.IsDBNull(reader.GetOrdinal("Notes"))) dog.Notes = reader.GetString(reader.GetOrdinal("Notes"));
                            if (!reader.IsDBNull(reader.GetOrdinal("ImageUrl"))) dog.Notes = reader.GetString(reader.GetOrdinal("ImageUrl"));
                            dogs.Add(dog);
                        }
                        return dogs;
                    }
                }
            }
        }

    }
}
