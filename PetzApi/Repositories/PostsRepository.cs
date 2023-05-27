﻿using System;
using PetzApi.Repositories;
using PetzApi.Utils;
using PetzApi.Models;
using Microsoft.Extensions.Hosting;

namespace PetzApi.Repositories;

public class PostsRepository : BaseRepository, IPostsRepository
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

                 

              
                        var post = new Posts()
                        {
                            Id = postId,
                            Post = DbUtils.GetString(reader, "Post"),
                            Date = DbUtils.GetDateTime(reader, "Date"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            UserId = DbUtils.GetInt(reader, "UserId"),

                            User = new Users()
                            {
                                Id = DbUtils.GetInt(reader, "UserId"),
                                FullName = DbUtils.GetString(reader, "FullName"),
                                Email = DbUtils.GetString(reader, "Email"),
                                Username = DbUtils.GetString(reader, "Username")
                            }
                        };

                        posts.Add(post);
                   



                };


                reader.Close();
                return posts;
            }

        }

    }

    public List<Posts> GetAll()
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
                                          ,u.Username
                                     FROM posts as p
                                     JOIN users as u
                                     ON p.UserId = u.Id";

                var reader = cmd.ExecuteReader();
                var posts = new List<Posts>();

                while (reader.Read())
                {
                    var post = new Posts()
                    {
                        Id = DbUtils.GetInt(reader, "Id"),
                        Post = DbUtils.GetString(reader, "Post"),
                        Date = DbUtils.GetDateTime(reader, "Date"),
                        ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                        UserId = DbUtils.GetInt(reader, "UserId"),

                        User = new Users()
                        {

                            Username = DbUtils.GetString(reader, "Username")
                        }
                    };
                    posts.Add(post);
                }
                reader.Close();
                return posts;
            }
        }
    }

    public Posts GetById(int id)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                                SELECT [Id]
                                       ,[Post]
                                       ,[Date]
                                       ,[ImageUrl]
                                       ,[UserId]
                                FROM [Petz].[dbo].[posts]
                                WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", id);
                var reader = cmd.ExecuteReader();
                Posts post = null;

                if (reader.Read())
                {
                    post = new Posts()
                    {
                        Id = DbUtils.GetInt(reader, "Id"),
                        Post = DbUtils.GetString(reader, "Post"),
                        Date = DbUtils.GetDateTime(reader, "Date"),
                        ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                        UserId = DbUtils.GetInt(reader, "UserId")
                    };
                }
                reader.Close();
                return post;
            }
        }
    }

    public void Add(Posts post)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                      INSERT INTO [posts] (Post, Date, ImageUrl, UserId)
                        OUTPUT INSERTED.ID
                        VALUES (@Post, @Date, @ImageUrl, @UserId)";

                DbUtils.AddParameter(cmd, "@Post", post.Post);
                DbUtils.AddParameter(cmd, "@Date", post.Date);
                DbUtils.AddParameter(cmd, "@ImageUrl", post.ImageUrl);
                DbUtils.AddParameter(cmd, "@UserId", post.UserId);


                post.Id = (int)cmd.ExecuteScalar();
            }
        }
    }

    public void Update(Posts post)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                      UPDATE posts
                         SET Post = @Post,
                              Date = @Date,
                              ImageUrl = @ImageUrl,
                              UserId = @UserId
                      WHERE Id = @Id";

                DbUtils.AddParameter(cmd, "@Id", post.Id);
                DbUtils.AddParameter(cmd, "@Post", post.Post);
                DbUtils.AddParameter(cmd, "@Date", post.Date);
                DbUtils.AddParameter(cmd, "@ImageUrl", post.ImageUrl);
                DbUtils.AddParameter(cmd, "@UserId", post.UserId);


                cmd.ExecuteNonQuery();
            }
        }
    }

    public void DeletePost(int id)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Posts WHERE Id = @Id";
                DbUtils.AddParameter(cmd, "@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public List<Posts> GetAllWithPets()


    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @" Select posts.Id as PostId
,posts.Post
,posts.[Date]
,posts.ImageUrl 
,posts.UserId
,pets.name as PetName
from posts
join petposts on posts.Id = petPosts.PostId
join pets on pets.Id = petPosts.PetId

";
                var reader = cmd.ExecuteReader();
                var posts = new List<Posts>();

                Posts? post = null;

                while (reader.Read())
                {
                    if (post == null || post.Id != DbUtils.GetInt(reader, "PostId"))
                    {
                        if (post != null)
                        {
                            posts.Add(post);
                        }

                        post = new Posts()
                        {
                            Id = DbUtils.GetInt(reader, "PostId"),
                            Post = DbUtils.GetString(reader, "Post"),
                            Date = DbUtils.GetDateTime(reader, "Date"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            UserId = DbUtils.GetInt(reader, "UserId"),
                            Pet = new List<Pets>()
                        };
                    }

                    post.Pet.Add(new Pets()
                    {

                        Name = DbUtils.GetString(reader, "PetName"),

                    });
                }

                if (post != null)
                {
                    posts.Add(post);
                }

                conn.Close();
                return posts;
            }
        }
    }
}