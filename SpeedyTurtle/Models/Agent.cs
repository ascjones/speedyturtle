﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedyTurtle.Models
{
    public class User
    {
        public int Id { get; set; }
        
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }

        public UserType Type { get; set; }
    }

    public enum UserType
    {
        Agent, Seeker
    }
}