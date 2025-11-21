using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;

namespace KetabKhaneh
{
    internal class SqliteDataAccess
    {
        public static List<Book> LoadBook()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Book>("select * from tblBook", new DynamicParameters());
                return output.ToList();
            }
        }
        public static int SaveBook(Book B1) 
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var ID = cnn.ExecuteScalar<int>("insert into tblBook (BookTitle,Bookmarker,Year) values (@Title,@Bookmarker,@Year);SELECT last_insert_rowid();", B1);
                B1.id = ID;
                return ID;
            }
       }
        private static string LoadConnectionString(string BookTitle="Default")
        {
            return ConfigurationManager.ConnectionStrings[BookTitle].ConnectionString;        
        }
        public static List<Place> LoadPlace()
        {
            using (IDbConnection cnm = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnm.Query<Place>("select * from tblPlace", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void savePlace(Place P1)
        {
            using (IDbConnection cnm=new SQLiteConnection(LoadConnectionString()))
            {
                cnm.Execute("insert into tblPlace ([column],[line],[BookId]) values (@Column,@Line,@BookId)", P1);
            }
        }
        public static string LoadConnectionString2(int column)
        {
            return ConfigurationManager.ConnectionStrings[column].ConnectionString;
        }
        public static List<Detail> SearchBooks(string keyword)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var sql = @"SELECT b.BookTitle, b.Bookmarker, b.Year, 
                           p.[Column] AS ShelfColumn, p.[Line] AS ShelfLine
                   FROM tblBook b
                    JOIN tblPlace p ON b.Id = p.BookId
                    WHERE b.BookTitle LIKE @kw OR b.Bookmarker LIKE @kw OR b.Year LIKE @kw";

                var output = cnn.Query(sql, new { kw = "%" + keyword + "%" });
                List<Detail> details = new List<Detail>();
   
                foreach (var row in output)
               {
                    Book book = new Book();

                    book.Title = row.BookTitle;
                    book.Bookmarker = row.Bookmarker;
                    book.Year = row.Year;


                    Place place = new Place();

                    place.Column = row.ShelfColumn;
                    place.Line = row.ShelfLine;

                    details.Add(new Detail { B1 = book, P1 = place });
                }
                cnn.Open();
                string query;
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    query = "SELECT * FROM Books";
                }
                else
                {
                    query = @"SELECT * FROM Books 
                      WHERE Title LIKE @keyword 
                         OR Year LIKE @keyword 
                         OR Bookmarker LIKE @keyword";
                }
                return details;
            }
        }
        public static int GetLastBookId()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                return cnn.ExecuteScalar<int>("SELECT last_insert_rowid()");
            }
        }

        public static void SavePlace(Place P1, int bookId)
        {
            using (IDbConnection cnm = new SQLiteConnection(LoadConnectionString()))
            {
                cnm.Execute("INSERT INTO tblPlace (Column, Line, BookId) VALUES (@Column, @Line, @BookId)",
                    new { P1.Column, P1.Line, BookId = bookId });
            }
        }
    }
}
