using BibliotecaAPIWeb.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace BibliotecaAPIWeb.Data
{
    public class ExcelRepository
    {
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BibliotecaBaseDatos.xlsx");

        public ExcelRepository(string filePath)
        {
            _filePath = filePath;
        }

        public List<string> GetUsersHeaders()
        {
            var usersHeaders = new List<string>();

            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(1);
                int lastColumnUsed = worksheet.LastColumnUsed().ColumnNumber();

                for (int col = 1; col <= lastColumnUsed; col++)
                {
                    string header = worksheet.Cell(1, col).GetValue<string>();
                    usersHeaders.Add(header);
                }
            }
            return usersHeaders;
        }

        public List<string> GetBooksHeaders()
        {
            var booksheaders = new List<string>();

            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(3);
                int lastColumnUsed = worksheet.LastColumnUsed().ColumnNumber();

                for (int col = 1; col <= lastColumnUsed; col++)
                {
                    string header = worksheet.Cell(1, col).GetValue<string>();
                    booksheaders.Add(header);
                }
            }
            return booksheaders;
        }

        public List<User> GetDataUsers()
        {
            var usersDataList = new List<User>();

            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var lastRowUsed = worksheet.LastRowUsed().RowNumber();

                for (int row = 2; row <= lastRowUsed; row++)
                {
                    var userDataItem = new User
                    {
                        Id = worksheet.Cell(row, 1).GetValue<int>(),
                        Name = worksheet.Cell(row, 2).GetValue<string>(),
                        UserType = worksheet.Cell(row, 3).GetValue<string>(),
                        BooksLoaned = worksheet.Cell(row, 4).GetValue<List<Book>>(),
                        MaxBooksAllowed = worksheet.Cell(row, 5).GetValue<int>(),
                        CanAskALoan = worksheet.Cell(row, 6).GetValue<bool>(),
                        LoanDays = worksheet.Cell(row, 7).GetValue<int>()
                    };
                    usersDataList.Add(userDataItem);
                }
            }
            return usersDataList;
        }

        public User GetUserById(int id)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(1);

                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var idCell = row.Cell(1);

                    if (idCell.GetValue<int>() == id)
                    {
                        return new User
                        {
                            Id = idCell.GetValue<int>(),
                            Name = row.Cell(2).GetValue<string>(),
                            UserType = row.Cell(3).GetValue<string>(),
                            BooksLoaned = row.Cell(4).GetValue<List<Book>>(),
                            MaxBooksAllowed = row.Cell(5).GetValue<int>(),
                            CanAskALoan = row.Cell(6).GetValue<bool>(),
                            LoanDays = row.Cell(7).GetValue<int>()
                        };
                    }
                }
            }
            throw new FormatException("The ISBN is not valid.");
        }

        public User GetUserByType(string userType)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(1);

                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var typeCell = row.Cell(3);

                    if (typeCell.GetValue<string>() == userType)
                    {
                        return new User
                        {
                            UserType = typeCell.GetValue<string>(),
                            Id = row.Cell(1).GetValue<int>(),
                            Name = row.Cell(2).GetValue<string>(),
                            BooksLoaned = row.Cell(4).GetValue<List<Book>>(),
                            MaxBooksAllowed = row.Cell(5).GetValue<int>(),
                            CanAskALoan = row.Cell(6).GetValue<bool>(),
                            LoanDays = row.Cell(7).GetValue<int>()
                        };
                    }
                }
            }
            throw new FormatException("The type is not valid.");
        }


        public List<Book> GetDataBooks()
        {
            var booksDataList = new List<Book>();

            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException($"The Excel file was not found at {_filePath}");
            }

            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(3);
                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                for (int row = 2; row <= lastRowUsed; row++)
                {
                    var bookDataItem = new Book
                    {
                        Author = worksheet.Cell(row, 1).GetValue<string>(),
                        Title = worksheet.Cell(row, 2).GetValue<string>(),
                        ISBN = worksheet.Cell(row, 3).GetValue<int>(),
                        Available = worksheet.Cell(row, 4).GetValue<bool>(),
                        LoanDate = worksheet.Cell(row, 5).GetValue<DateTime>(),
                        ReturnDate = worksheet.Cell(row, 6).GetValue<DateTime>(),
                    };
                    booksDataList.Add(bookDataItem);
                }
            }
            return booksDataList;
        }

        public Book GetBookByISBN(int isbn)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(2);
                
                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var isbnCell = row.Cell(3);

                    if (isbnCell.GetValue<int>() == isbn)
                    {
                        return new Book
                        {
                            ISBN = isbnCell.GetValue<int>(),
                            Author = row.Cell(1).GetValue<string>(),
                            Title = row.Cell(2).GetValue<string>(),
                            Available = row.Cell(4).GetValue<bool>(),
                            LoanDate = row.Cell(5).GetValue<DateTime>(),
                            ReturnDate = row.Cell(6).GetValue<DateTime>(),
                        };
                    }
                }
            }
            throw new FormatException("The ISBN is not valid.");
        }

        public Book GetBookByTitle(string title)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(2);

                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    var titleCell = row.Cell(2);

                    if (titleCell.GetValue<string>() == title)
                    {
                        return new Book
                        {
                            Title = titleCell.GetValue<string>(),
                            Author = row.Cell(1).GetValue<string>(),
                            ISBN = row.Cell(3).GetValue<int>(),
                            Available = row.Cell(4).GetValue<bool>(),
                            LoanDate = row.Cell(5).GetValue<DateTime>(),
                            ReturnDate = row.Cell(6).GetValue<DateTime>(),
                        };
                    }
                }
            }
            throw new FormatException("The title is not valid.");
        }

        public void CreateUserData(User newUserData)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(1);
                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                lastRowUsed++;

                worksheet.Cell(lastRowUsed, 1).Value = newUserData.Id;
                worksheet.Cell(lastRowUsed, 2).Value = newUserData.Name;
                worksheet.Cell(lastRowUsed, 3).Value = newUserData.UserType;

                var booksLoanedToString = string.Join(", ", newUserData.BooksLoaned.Select(libro => libro.ISBN));
                worksheet.Cell(lastRowUsed, 4).Value = booksLoanedToString;

                worksheet.Cell(lastRowUsed, 5).Value = newUserData.MaxBooksAllowed;
                worksheet.Cell(lastRowUsed, 6).Value = newUserData.CanAskALoan;
                worksheet.Cell(lastRowUsed, 7).Value = newUserData.LoanDays;
                
                workbook.Save();
            }
        }

        public void CreateBookData(Book newBookData)
        {
            using (var workbook = new XLWorkbook(_filePath))
            {
                var worksheet = workbook.Worksheet(2);
                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                lastRowUsed++;

                worksheet.Cell(lastRowUsed, 1).Value = newBookData.Author;
                worksheet.Cell(lastRowUsed, 2).Value = newBookData.Title;
                worksheet.Cell(lastRowUsed, 3).Value = newBookData.ISBN;
                worksheet.Cell(lastRowUsed, 4).Value = newBookData.Available;

                workbook.Save();
            }
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
                    int currentId = worksheet.Cell(row, 3).GetValue<int>();

                    if (currentId == updatedUserData.Id)
                    {
                        worksheet.Cell(row, 2).Value = updatedUserData.Name;
                        worksheet.Cell(row, 3).Value = updatedUserData.UserType;
                        found = true;
                    }
                    workbook.Save();
                }
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
                    int currentId = worksheet.Cell(row, 1).GetValue<int>();

                    if (currentId == updatedBookData.ISBN)
                    {
                        worksheet.Cell(lastRowUsed, 1).Value = updatedBookData.Author;
                        worksheet.Cell(lastRowUsed, 2).Value = updatedBookData.Title;
                        worksheet.Cell(lastRowUsed, 4).Value = updatedBookData.Available;
                        worksheet.Cell(lastRowUsed, 5).Value = updatedBookData.LoanDate;
                        worksheet.Cell(lastRowUsed, 6).Value = updatedBookData.ReturnDate;
                        found = true;
                    }
                    workbook.Save();
                }
            }
        }
    }
}
