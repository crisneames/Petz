using PetzApi.Utils;
using PetzApi.Models;
using System.Drawing;

namespace PetzApi.Repositories;


public class PetsRepository : BaseRepository, IPetsRepository
{
    public PetsRepository(IConfiguration configuration) : base(configuration) { }

    public List<Pets> GetAllPets()
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT Id
                                           ,Name
                                           ,KindOfPet
                                           ,ImageUrl
                                           ,UserId
                                      FROM  pets";

                var reader = cmd.ExecuteReader();
                var pets = new List<Pets>();

                while (reader.Read())
                {
                    var pet = new Pets()
                    {
                        Id = DbUtils.GetInt(reader, "Id"),
                        Name = DbUtils.GetString(reader, "Name"),
                        KindOfPet = DbUtils.GetString(reader, "KindOfPet"),
                        ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                        UserId = DbUtils.GetInt(reader, "UserId"),

                    };

                    pets.Add(pet);
                }

                reader.Close();
                return pets;
            }
        }
    }

    public Pets GetById(int id)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT Id
                           ,Name
                           ,KindOfPet
                           ,ImageUrl
                           ,UserId
                      FROM pets
                      WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                Pets pet = null;

                if (reader.Read())
                {
                    pet = new Pets()
                    {
                        Id = DbUtils.GetInt(reader, "Id"),
                        Name = DbUtils.GetString(reader, "Name"),
                        KindOfPet = DbUtils.GetString(reader, "KindOfPet"),
                        ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                        UserId = DbUtils.GetInt(reader, "UserId"),
                    };

                }
                reader.Close();
                return pet;
            }
        }
    }

    public void Add(Pets pet)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                     INSERT INTO [pets] (Name, KindOfPet, ImageUrl, UserId)
                        OUTPUT INSERTED.ID
                        VALUES (@Name, @KindOfPet, @ImageUrl, @UserId)";

                DbUtils.AddParameter(cmd, "@Name", pet.Name);
                DbUtils.AddParameter(cmd, "@KindOfPet", pet.KindOfPet);
                DbUtils.AddParameter(cmd, "@ImageUrl", pet.ImageUrl);
                DbUtils.AddParameter(cmd, "@UserId", pet.UserId);


                pet.Id = (int)cmd.ExecuteScalar();
            }
        }
    }

    public void Update(Pets pet)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                     UPDATE pets
                         SET Name = @Name,
                              KindOfPet = @KindOfPet,
                              ImageUrl = ImageUrl,
                              UserId = @UserId
                              
                        WHERE Id = @Id";

                DbUtils.AddParameter(cmd, "@Id", pet.Id);
                DbUtils.AddParameter(cmd, "@Name", pet.Name);
                DbUtils.AddParameter(cmd, "@KindOfPet", pet.KindOfPet);
                DbUtils.AddParameter(cmd, "@ImageUrl", pet.ImageUrl);
                DbUtils.AddParameter(cmd, "@UserId", pet.UserId);


                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int id)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM pets WHERE Id = @Id";
                DbUtils.AddParameter(cmd, "@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }

}




