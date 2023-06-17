using System;
namespace PetzApi.Models;

public class Users
{
	public int Id { get; set; }
    public string FirebaseId { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string? Password { get; set; }
 }

