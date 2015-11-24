﻿using System;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Models
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}