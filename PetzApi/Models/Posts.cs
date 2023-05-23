﻿using System;
namespace PetzApi.Models
{
	public class Posts
	{
		public int Id { get; set; }
		public string? Post { get; set; }
		public DateTime? Date { get; set; }
		public string ImageUrl { get; set; }
		public int UserId { get; set; }
		public Users User { get; set; }
	}
}
