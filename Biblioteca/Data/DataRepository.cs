﻿using BibliotecaAPIWeb.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Transactions;

namespace BibliotecaAPIWeb.Data
{
    public class DataRepository
    {

        private readonly DapperContext _context;

        public DataRepository(DapperContext context) 
        {
            _context = context;
        }


        public void CreateUser(UserDto newUser)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = @"
                        INSERT INTO Users (Name, UserType, MaxBooksAllowed)
                        VALUES (@Name, @UserType, @MaxBooksAllowed)";

                    var rowsAffected = connection.Execute(sql, newUser);
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception("Ocurrió un error al insertar el usuario en la base de datos", sqlEx);
                }
            }
        }

        public void CreateBook(Book newBook)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = @"
                        INSERT INTO Books (ISBN, Author, Title, Available)
                        VALUES (@ISBN, @Author, @Title, @Available)";

                    var rowsAffected = connection.Execute(sql, newBook);
                }
                catch (SqlException sqlEx) 
                {
                    throw new Exception("Ocurrió un error al insertar el producto en la base de datos", sqlEx);
                }
            }
        }


        public IEnumerable<User> GetUsers()
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = @"
                        SELECT
                            u.ID AS id_user,
                            u.Name,
                            u.UserType,
                            u.MaxBooksAllowed,
                            s.ID as sales_id,
                            s.ISBN_book,
                            s.id_user,
                            b.ISBN,
                            b.Author,
                            b.Title,
                            b.Available
                        FROM Users u
                        LEFT JOIN Sales s ON u.ID = s.id_user
                        LEFT JOIN Books b ON s.ISBN_book = b.ISBN;";

                    var users = connection.Query<User, Sales, User>(sql,
                        (user, sales) =>
                        {
                            user.Sales = new List<Sales>();

                            if (sales != null && sales.Id != 0)
                            {
                                user.Sales.Add(sales);
                            }

                            return user;
                        },
                        splitOn: "sales_id, ISBN"
                        );
                    return users;
                }
                catch (SqlException sqlEx)
                {
                    return Enumerable.Empty<User>();
                }
            } 
        }

        public User GetUserById(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = @"
                        SELECT
                            u.ID,
                            u.Name,
                            u.UserType,
                            u.MaxBooksAllowed
                        FROM Users u
                        WHERE u.ID = @ID";

                    var user = connection.QueryFirst<User>(sql, new {ID = id});
                    return user;
                }
                catch (SqlException sqlEx)
                {
                    return null;
                    throw new Exception("Ocurrió un error al tomar el usuario de la base de datos", sqlEx);
                }
            }
        }

        public List<User> GetUserByType(string userType)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = @"
                        SELECT
                            u.ID,
                            u.Name,
                            u.UserType,
                            u.MaxBooksAllowed
                        FROM Users u
                        WHERE u.UserType = @UserType";

                    var users = connection.Query<User>(sql, new { UserType = userType }).ToList();
                    return users;
                }
                catch (SqlException sqlEx)
                { 
                    return null;
                    throw new Exception("Ocurrió un error al tomar el tipo de usuario de la base de datos", sqlEx);
                }
            }
        }


        public IEnumerable<Book> GetBooks()
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = @"
                        SELECT
                            b.ISBN,
                            b.Author,
                            b.Title,
                            b.Available
                        FROM Books b";

                    var books = connection.Query<Book>(sql);
                    return books;
                }
                catch (SqlException sqlEx) 
                {
                    return Enumerable.Empty<Book>();
                }
            }
        }

        public Book GetBookByISBN(string isbn)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = @"
                        SELECT
                            b.ISBN,
                            b.Author,
                            b.Title,
                            b.Available
                        FROM Books b
                        WHERE b.ISBN = @isbn";

                    var book = connection.QueryFirst<Book>(sql, new { ISBN = isbn });
                    return book;
                }
                catch (SqlException sqlEx)
                {
                    return null;
                    throw new Exception("Ocurrió un error al tomar el libro de la base de datos", sqlEx);
                }
            }
        }

        public Book GetBookByTitle(string title)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = @"
                        SELECT
                            b.ISBN,
                            b.Author,
                            b.Title,
                            b.Available
                        FROM Books b
                        WHERE b.Title = @Title";

                    var book = connection.QueryFirst<Book>(sql, new { Title = title });
                    return book;
                }
                catch (SqlException sqlEx)
                {
                    return null;
                    throw new Exception("Ocurrió un error al tomar el libro de la base de datos", sqlEx);
                }
            }
        }

        public string UpdateUserById(UserDto updatedUser)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = @"
                                UPDATE Users
                                SET Name = @Name, UserType = @UserType
                                WHERE ID = @ID";

                        var rowsAffected = connection.Execute(sql, updatedUser, transaction);

                        if (rowsAffected == 0) 
                        {
                            return "No se encontró el usuario con dicho ID";
                        }

                        transaction.Commit();
                        return $"Se realizaron {rowsAffected} actualizaciones";
                    } catch (SqlException sqlEx) {
                        {
                            transaction.Rollback();
                            throw new Exception("Ocurrió un error al insertar el usuario en la base de datos", sqlEx);
                        }
                    }
                }
            } 
        }

        public string UpdateBookByISBN(BookDto updatedBook)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = @"
                                UPDATE Books
                                SET Author = @Author, Title = @Title, Available = @Available
                                WHERE ISBN = @ISBN";

                        var rowsAffected = connection.Execute(sql, updatedBook, transaction);

                        if (rowsAffected == 0)
                        {
                            return "No se encontró el libro con dicho ISBN";
                        }

                        transaction.Commit();
                        return $"Se realizaron {rowsAffected} actualizaciones";
                    }
                    catch (SqlException sqlEx)
                    {
                        {
                            transaction.Rollback();
                            throw new Exception("Ocurrió un error al insertar el producto en la base de datos", sqlEx);
                        }
                    }
                }
            }
        }

        public string DeleteUser(int id)
        {
            using (var connection = _context.CreateConnection()) 
            {
                try
                {
                    var sql = @"DELETE FROM Users WHERE ID = @ID";
                    var rowsAffected = connection.Execute(sql, new {ID = id});

                    if (rowsAffected == 0)
                    {
                        return "No se encontró el usuario con dicho ID";
                    }
                    return "Eliminado correctamente";
                } catch (SqlException sqlEx)
                {
                    throw new Exception("Ocurrió un error al eliminar el usuario de la base de datos", sqlEx);
                }
            }
        }

        public string DeleteBook(string isbn)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = @"DELETE FROM Books WHERE ISBN = @ISBN";
                    var rowsAffected = connection.Execute(sql, new { ISBN = isbn });

                    if (rowsAffected == 0)
                    {
                        return "No se encontró el libro con dicho ISBN";
                    }
                    return "Eliminado correctamente";
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception("Ocurrió un error al eliminar el producto de la base de datos", sqlEx);
                }
            }
        }
    }
}