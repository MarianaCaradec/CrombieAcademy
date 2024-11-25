using System.Text.Json.Serialization;

namespace BibliotecaAPIWeb.Models
{
    public class Book
    {
        public string Author { get; set; }

        public string Title { get; set; }

        [JsonIgnore]
        public int ISBN { get; init; }

        public bool Available { get; set; }

        [JsonIgnore]
        public DateTime LoanDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public DateTime ReturnDate { get; set; }

    }

    public class BookDto
    {
        public string Author { get; set; }

        public string Title { get; set; }

        public int ISBN { get; init; }

        public bool Available { get; set; }

        public DateTime LoanDate { get; set; } = DateTime.Now;

        public DateTime ReturnDate { get; set; }
    }
}
