using PetzApi.Utils;
using PetzApi.Models;

namespace PetzApi.Repositories;


public class UsersRepository : BaseRepository, IUsersRepository
{
    public UsersRepository(IConfiguration configuration) : base(configuration) { }

    public List<Users> GetAllUsers()
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT [Id]
                                           ,[FirebaseId]
                                           ,[Fullname]
                                           ,[Email]
                                           ,[Username]
                                           ,[Password]
                               FROM[Petz].[dbo].[users]";

                var reader = cmd.ExecuteReader();
                var users = new List<Users>();

                while (reader.Read())
                {
                    var user = new Users()
                    {
                        Id = DbUtils.GetInt(reader, "Id"),
                        FirebaseId = DbUtils.GetString(reader, "FirebaseId"),
                        Fullname = DbUtils.GetString(reader, "Fullname"),
                        Email = DbUtils.GetString(reader, "Email"),
                        Username = DbUtils.GetString(reader, "Username"),
                        Password = DbUtils.GetString(reader, "Password"),
                    };

                    users.Add(user);
                }

                reader.Close();
                return users;
            }
        }
    }

    public Users GetById(int id)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT [Id]
                           ,[FirebaseId]
                           ,[Fullname]
                           ,[Email]
                           ,[Username]
                           ,[Password]
                    FROM[Petz].[dbo].[users]
                    WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                Users user = null;

                if (reader.Read())
                {
                    user = new Users()
                    {
                        Id = DbUtils.GetInt(reader, "Id"),
                        FirebaseId = DbUtils.GetString(reader, "FirebaseId"),
                        Fullname = DbUtils.GetString(reader, "Fullname"),
                        Email = DbUtils.GetString(reader, "Email"),
                        Username = DbUtils.GetString(reader, "Username"),
                        Password = DbUtils.GetString(reader, "Password"),
                    };

                }
                reader.Close();
                return user;
            }
        }
    }

    public void Add(Users user)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                      INSERT INTO [users] (FirebaseId, Fullname, Email, Username, Password)
                        OUTPUT INSERTED.ID
                        VALUES (@FirebaseId, @Fullname, @Email, @Username, @Password)";

                DbUtils.AddParameter(cmd, "@FirebaseId", user.FirebaseId);
                DbUtils.AddParameter(cmd, "@Fullname", user.Fullname);
                DbUtils.AddParameter(cmd, "@Email", user.Email);
                DbUtils.AddParameter(cmd, "@Username", user.Username);
                DbUtils.AddParameter(cmd, "@Password", user.Password);

                user.Id = (int)cmd.ExecuteScalar();
            }
        }
    }

    public void Update(Users user)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                      UPDATE users
                         SET FirebaseId = @FirebaseId,
                              Fullname = @Fullname,
                              Email = @Email,
                              Username = @Username,
                              Password = @Password
                        WHERE Id = @Id";

                DbUtils.AddParameter(cmd, "@Id", user.Id);
                DbUtils.AddParameter(cmd, "@FirebaseId", user.FirebaseId);
                DbUtils.AddParameter(cmd, "@Fullname", user.Fullname);
                DbUtils.AddParameter(cmd, "@Email", user.Email);
                DbUtils.AddParameter(cmd, "@Username", user.Username);
                DbUtils.AddParameter(cmd, "@Password", user.Password);

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
                cmd.CommandText = "DELETE FROM users WHERE Id = @Id";
                DbUtils.AddParameter(cmd, "@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    //public Users GetUsersWithPets(int id)
}


       

