        using System;
using System.Text.Json.Serialization;

namespace BibliotecaAPIWeb.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; init; }

        public string Name { get; set; }

        private string _userType;

        public virtual string UserType
        {
            get => _userType;
            set
            {
                if (value == "Estudiante")
                {
                    MaxBooksAllowed = 3;
                    LoanDays = 7;
                } else if (value == "Profesor")
                {
                    MaxBooksAllowed = 5;
                    LoanDays = 14;
                }

                if(value != "Estudiante" && value != "Profesor")
                {
                    throw new InvalidOperationException("TipoUsuario no válido.");
                }
                _userType = value;
            }
        }

        [JsonIgnore]
        public List<BookDto> BooksLoaned { get; set; } = new List<BookDto>();

        [JsonIgnore]
        public virtual int MaxBooksAllowed { get; set; }

        [JsonIgnore]
        public bool CanAskALoan { get; set; }

        [JsonIgnore]
        public virtual int LoanDays { get; set; }

    }

    public class UserDto
    {
        public int Id { get; init; }
        public string Name { get; set; }

        private string _userType;

        public virtual string UserType
        {
            get => _userType;
            set
            {
                if (value == "Estudiante")
                {
                    MaxBooksAllowed = 3;
                    LoanDays = 7;
                }
                else if (value == "Profesor")
                {
                    MaxBooksAllowed = 5;
                    LoanDays = 14;
                }

                if (value != "Estudiante" && value != "Profesor")
                {
                    throw new InvalidOperationException("TipoUsuario no válido.");
                }
                _userType = value;
            }
        }

        public List<BookDto> BooksLoaned { get; set; }

        public virtual int MaxBooksAllowed { get; set; }

        public bool CanAskALoan { get; set; }

        public virtual int LoanDays { get; set; }

    }
}
