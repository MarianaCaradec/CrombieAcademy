using BibliotecaAPIWeb.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Diagnostics;

namespace BibliotecaAPIWeb.Data
{
    public class ExcelRepository
    {
        private readonly string _filePath = "C:\\Users\\maric\\source\\repos\\CrombieAcademy\\Biblioteca\\bin\\Debug\\net8.0\\Data\\BibliotecaBaseDatos.xlsx";

        public ExcelRepository() { }


        public void CreateUserData(UserDto newUserData)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(1);
                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                lastRowUsed++;

                worksheet.Cell(lastRowUsed, 1).SetValue(newUserData.Id);
                worksheet.Cell(lastRowUsed, 2).SetValue(newUserData.Name);
                worksheet.Cell(lastRowUsed, 3).SetValue(newUserData.UserType);

                var booksLoanedToString = newUserData.BooksLoaned.ToString().Split(", ").Select(isbn => int.TryParse(isbn, out var parsedIsbn) ? new Book { ISBN = parsedIsbn } : null).Where(book => book != null).Select(book => book.ISBN.ToString()).ToList();
                worksheet.Cell(lastRowUsed, 4).SetValue(string.Join(", ", booksLoanedToString));

                worksheet.Cell(lastRowUsed, 5).SetValue(newUserData.MaxBooksAllowed);
                worksheet.Cell(lastRowUsed, 6).SetValue(newUserData.CanAskALoan);
                worksheet.Cell(lastRowUsed, 7).SetValue(newUserData.LoanDays);

                workbook.Save();
            }
        }

        public void CreateBookData(BookDto newBookData)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(2);
                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                lastRowUsed++;

                worksheet.Cell(lastRowUsed, 1).SetValue(newBookData.Author);
                worksheet.Cell(lastRowUsed, 2).SetValue(newBookData.Title);
                worksheet.Cell(lastRowUsed, 3).SetValue(newBookData.ISBN);
                worksheet.Cell(lastRowUsed, 4).SetValue(newBookData.Available);

                workbook.Save();
            }
        }


        public List<UserDto> GetDataUsers()
        {
            var usersDataList = new List<UserDto>();

            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var lastRowUsed = worksheet.LastRowUsed().RowNumber();

                for (int row = 2; row <= lastRowUsed; row++)
                {
                    var booksLoanedList = worksheet.Cell(row, 4).GetValue<string>();
                    var booksLoanedFiltrated = booksLoanedList.Split(", ").Select(isbn => int.TryParse(isbn, out var parsedIsbn) ? new BookDto { ISBN = parsedIsbn } : null).Where(book => book != null).ToList();

                    var userData = new UserDto
                    {
                        Id = worksheet.Cell(row, 1).GetValue<int>(),
                        Name = worksheet.Cell(row, 2).GetValue<string>(),
                        UserType = worksheet.Cell(row, 3).GetValue<string>(),
                        BooksLoaned = booksLoanedFiltrated,
                        MaxBooksAllowed = worksheet.Cell(row, 5).GetValue<int>(),
                        CanAskALoan = worksheet.Cell(row, 6).GetValue<bool>(),
                        LoanDays = worksheet.Cell(row, 7).GetValue<int>()
                    };
                    usersDataList.Add(userData);
                }
            }
            return usersDataList;
        }

        public UserDto GetUserById(int id)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(1);

                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var idCell = row.Cell(1).GetValue<int>();

                    var booksLoanedListToString = row.Cell(4).GetValue<string>();
                    var booksLoanedFiltrated = booksLoanedListToString.Split(", ").Select(isbn => int.TryParse(isbn, out var parsedIsbn) ? new BookDto { ISBN = parsedIsbn } : null).Where(book => book != null).ToList();

                    if (idCell == id)
                    {
                        return new UserDto
                        {
                            Id = idCell,
                            Name = row.Cell(2).GetValue<string>(),
                            UserType = row.Cell(3).GetValue<string>(),
                            BooksLoaned = booksLoanedFiltrated,
                            MaxBooksAllowed = row.Cell(5).GetValue<int>(),
                            CanAskALoan = row.Cell(6).GetValue<bool>(),
                            LoanDays = row.Cell(7).GetValue<int>()
                        };
                    }
                }
            }
            throw new FormatException("The ISBN is not valid.");
        }

        public List<UserDto> GetUserByType(string userType)
        {
            var usersByType = new List<UserDto>();

            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(1);

                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var typeCell = row.Cell(3).GetValue<string>();

                    var booksLoanedListToString = row.Cell(4).GetValue<string>();
                    var booksLoanedFiltrated = booksLoanedListToString.Split(", ").Select(isbn => int.TryParse(isbn, out var parsedIsbn) ? new BookDto { ISBN = int.Parse(isbn) } : null).Where(book => book != null).ToList();

                    if (typeCell == userType)
                    {
                        var userByType = new UserDto
                        {
                            UserType = typeCell,
                            Id = row.Cell(1).GetValue<int>(),
                            Name = row.Cell(2).GetValue<string>(),
                            BooksLoaned = booksLoanedFiltrated,
                            MaxBooksAllowed = row.Cell(5).GetValue<int>(),
                            CanAskALoan = row.Cell(6).GetValue<bool>(),
                            LoanDays = row.Cell(7).GetValue<int>()
                        };
                        usersByType.Add(userByType);
                    }
                }
                return usersByType;
            }
            throw new FormatException("The type is not valid.");
        }


        public List<BookDto> GetDataBooks()
        {
            var booksDataList = new List<BookDto>();

            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(2);
                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                for (int row = 2; row <= lastRowUsed; row++)
                {
                    var bookData = new BookDto
                    {
                        Author = worksheet.Cell(row, 1).GetValue<string>(),
                        Title = worksheet.Cell(row, 2).GetValue<string>(),
                        ISBN = worksheet.Cell(row, 3).GetValue<int>(),
                        Available = worksheet.Cell(row, 4).GetValue<bool>(),
                    };
                    booksDataList.Add(bookData);
                }
            }
            return booksDataList;
        }

        public BookDto GetBookByISBN(int isbn)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(2);

                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var isbnCell = row.Cell(3).GetValue<int>();

                    if (isbnCell == isbn)
                    {
                        return new BookDto
                        {
                            ISBN = isbnCell,
                            Author = row.Cell(1).GetValue<string>(),
                            Title = row.Cell(2).GetValue<string>(),
                            Available = row.Cell(4).GetValue<bool>(),
                        };
                    }
                }
            }
            throw new FormatException("The ISBN is not valid.");
        }

        public BookDto GetBookByTitle(string title)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(2);

                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var titleCell = row.Cell(2);

                    if (titleCell.GetValue<string>() == title)
                    {
                        return new BookDto
                        {
                            Title = titleCell.GetValue<string>(),
                            Author = row.Cell(1).GetValue<string>(),
                            ISBN = row.Cell(3).GetValue<int>(),
                            Available = row.Cell(4).GetValue<bool>(),
                        };
                    }
                }
            }
            throw new FormatException("The title is not valid.");
        }

        public void UpdateUserDataById(User updatedUserData)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(1);
                int lastRowUsed = worksheet.LastRowUsed().RowNumber();
                bool found = false;

                for (int row = 2; row <= lastRowUsed; row++)
                {
                    int currentId = worksheet.Cell(row, 1).GetValue<int>();

                    if (currentId == updatedUserData.Id)
                    {
                        worksheet.Cell(row, 2).SetValue(updatedUserData.Name);
                        worksheet.Cell(row, 3).SetValue(updatedUserData.UserType);
                        found = true;
                        break;
                    }
                }
                workbook.Save();
            }
        }

        public void UpdateBookDataByISBN(Book updatedBookData)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(2);
                var lastRowUsed = worksheet.LastRowUsed().RowNumber();
                bool found = false;

                for (int row = 2; row <= lastRowUsed; row++)
                {
                    int currentISBN = worksheet.Cell(row, 3).GetValue<int>();
                    if (currentISBN == updatedBookData.ISBN)
                    {
                        worksheet.Cell(row, 1).SetValue(updatedBookData.Author);
                        worksheet.Cell(row, 2).SetValue(updatedBookData.Title);
                        worksheet.Cell(row, 4).SetValue(updatedBookData.Available);
                        found = true;
                        break;
                    }
                }

                    workbook.Save();

            }
        }

        public void DeleteUser(int id)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RowsUsed().Skip(1);

                foreach (var row in rows)
                {
                    var rowId = row.Cell(1).GetValue<int>().ToString();
                    var rowIdToInt = int.TryParse(rowId, out int Id);

                    if (rowIdToInt && Id == id)
                    {
                        row.Delete();
                        break;
                    }
                }
                workbook.Save();
            }
        }

        public void DeleteBook(int isbn)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(2);
                var rows = worksheet.RowsUsed().Skip(1);

                foreach (var row in rows)
                {
                    var rowISBN = row.Cell(3).GetValue<int>().ToString();
                    var rowISBNToString = int.TryParse(rowISBN, out int ISBN);

                    if (rowISBNToString && ISBN == isbn)
                    {
                        row.Delete();
                        break;
                    }
                }
                workbook.Save();
            }
        }
    }
}