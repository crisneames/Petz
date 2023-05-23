using System;
using PetzApi.Repositories;
using PetzApi.Utils;
using PetzApi.Models;
namespace PetzApi.Repositories;

public class PostsRepository : BaseRepository
{
    public PostsRepository(IConfiguration configuration) : base(configuration) { }

    public List<Posts> GetPostsByUser(int Id)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                                    SELECT p.Id
                                          ,p.Post
                                          ,p.Date
                                          ,p.ImageUrl
                                          ,p.UserId
                                          ,u.FullName
                                          ,u.Email
                                          ,u.Username
                                     FROM posts as p
                                     JOIN users as u
                                     ON p.UserId = u.Id
                                     WHERE p.UserId = @id";

                DbUtils.AddParameter(cmd, "@id", Id);

                var reader = cmd.ExecuteReader();
                var posts = new List<Posts>();



                while (reader.Read())
                {
                    var postId = DbUtils.GetInt(reader, "Id");

                var existingPost = posts.FirstOrDefault(p => p.Id == postId);

                    if (existingPost == null)
                    {
                        existingPost = new Posts()
                        {
                            Id = postId,
                            Post = DbUtils.GetString(reader, "Post"),
                            Date = DbUtils.GetDateTime(reader, "Date"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            UserId = DbUtils.GetInt(reader, "UserId"),

                            User = new Users()
                            {
                                FullName = DbUtils.GetString(reader, "FullName"),
                                Email = DbUtils.GetString(reader, "Email"),
                                Username = DbUtils.GetString(reader, "Username")
                            }
                        };

                        posts.Add(existingPost);
                    }

                    
                   
                     };


                reader.Close();
                return posts;
            }

        }

    }

}