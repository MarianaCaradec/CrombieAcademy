using System.Text.Json.Serialization;

namespace BibliotecaAPIWeb.Models
{
    public class Book
    {
        public string Author { get; set; }

        public string Title { get; set; }

        public string ISBN { get; init; }

        public bool Available { get; set; }

    }

    public class BookDto
    {
        public string Author { get; set; }

        public string Title { get; set; }

        [JsonIgnore]
        public string ISBN { get; init; }

        public bool Available { get; set; }

    }
}
