using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IConfiguration _config;

        public OwnerRepository(IConfiguration config)
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

        public List<Owner> getAllOwners()
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, [Name], Email, [Address], NeighborhoodId, Phone FROM [Owner]";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Owner> owners = new List<Owner>();
                        while (reader.Read())
                        {
                            Owner owner = new Owner()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            };
                            owners.Add(owner);
                        }
                        return owners;
                    }
                }
            }
        }

        public Owner getOwnerById(int Id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, [Name], Email, [Address], NeighborhoodId, Phone FROM[Owner] WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            Owner owner = new Owner()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Dogs = getDogsByOwnerId(Id)
                            };

                            return owner;
                        }
                        else return null;
                    }
                }
            }
        }

        public List<Dog> getDogsByOwnerId(int id)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = @"
                                            SELECT Id, Name, OwnerId, Breed
                                            FROM Dog
                                            WHERE OwnerId = @id";
                    command.Parameters.AddWithValue("@id", id);
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Dog> dogs = new List<Dog>();
                        while(reader.Read())
                        {
                            Dog dog = new Dog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed"))
                            };
                            dogs.Add(dog);
                        }
                        return dogs;
                    }
                }
            }
        }

    }
}
