using BibliotecaAPIWeb.Models;
using System;
using System.Text.Json.Serialization;

namespace BibliotecaAPIWeb.Models
{
    public class User
    {
        public int Id { get; init; }
        public string Name { get; set; }

        public virtual string UserType { get; set; }

        public virtual int MaxBooksAllowed { get; set; }

        public List<Sales>? Sales { get; set; } = new List<Sales>();

    } 
}

    public class UserDto
    {
        [JsonIgnore]
        public int Id { get; init; }
        public string Name { get; set; }

        public virtual string UserType { get; set; }

        public virtual int MaxBooksAllowed { get; set; }

        [JsonIgnore]
        public List<Sales>? Sales { get; set; } = new List<Sales>();
}

