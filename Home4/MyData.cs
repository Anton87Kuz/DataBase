using Home4.Tables;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home4
{


    public class MyData : IDisposable
    {
        static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Univercity;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        protected DataContext db = new DataContext(connectionString);

        public MyData()
        {

        }



        public void GetStudents()
        {
            Console.WriteLine("List of all student");
            foreach (Student item in db.GetTable<Student>())
            {
                Console.WriteLine($"{item.ID}, {item.Name}, {item.Surname}, {item.Year}");
            }

        }

        public void GetBooks()
        {
            Console.WriteLine("List of all books");
            foreach (Book item in db.GetTable<Book>())
            {
                Console.WriteLine($"{item.ID}, {item.Authors}, {item.Title}: {item.Year}, -- {item.Pages}; Quantity: {item.Quantity}");
               
            }
        }

        public void GetLessons()
        {
            Console.WriteLine("List of all lessons");
            foreach (Lesson item in db.GetTable<Lesson>())
            {
                Console.WriteLine($"{item.ID}.) {item.Name} ");
            }
        }


        public void GetTakenLessons()
        {
            var taken = from x in db.GetTable<TakenLessons>()
                        join y in db.GetTable<Lesson>() on x.LessonID equals y.ID
                        select y;

            Console.WriteLine("Those lessons are visited by students");
            if (taken.Count() != 0)
            {
                foreach (var item in taken)
                {
                    Console.WriteLine($" {item.ID}: {item.Name}");
                }
            }
            else { Console.WriteLine(" Students don't choose any lesson"); }

            var free = db.GetTable<Lesson>().Except(taken);

            Console.WriteLine("Those lessons students don't visit");
            if (free.Count() != 0)
            {
                foreach (var item in free)
                {
                    Console.WriteLine($" {item.ID}: {item.Name}");
                }
            }
            else { Console.WriteLine(" Students don't choose any lesson"); }
        }

        public bool AddBook()
        {
            bool result;
            Book book = new Book();
            Console.WriteLine("Enter book's authors:");
            book.Authors = Console.ReadLine();
            Console.WriteLine("Enter book's title:");
            book.Title = Console.ReadLine();
            Console.WriteLine("Enter book's year of printing:");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                if ((year > 1800) && (year < 2019))
                {
                    book.Year = year;
                    result = true;
                }
                else { Console.WriteLine("Year must be between 1800 and 2019"); result = false; }
            }
            else { Console.WriteLine("Year must be integer number"); result = false; }
            if (result)
            {
                Console.WriteLine("Enter number of pages in book:");
                if (int.TryParse(Console.ReadLine(), out int pages))
                {
                    if (pages > 0)
                    {
                        book.Pages = pages;
                        book.Quantity = 100;
                        db.GetTable<Book>().InsertOnSubmit(book);
                        db.SubmitChanges();
                        result = true;
                    }
                    else { Console.WriteLine("Number of pages must be positive"); result = false; }

                }
                else { Console.WriteLine("Year must be integer number"); result = false; }
            }
            else { return result; }
            return result;
        }

        public bool AddStudent()
        {
            Student stud = new Student();
            Console.WriteLine("Enter student name:");
            stud.Name = Console.ReadLine();
            Console.WriteLine("Enter student surname:");
            stud.Surname = Console.ReadLine();
            Console.WriteLine("Enter student year of birth:");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                if ((year > 1900) && (year < 2019))
                {
                    stud.Year = year;
                    db.GetTable<Student>().InsertOnSubmit(stud);
                    db.SubmitChanges();
                    return true;
                }
                else { Console.WriteLine("Year must be between 1900 and 2019"); return false; }
            }
            else { Console.WriteLine("Year must be integer number"); return false; }

        }

        public void AddLesson()
        {
            Lesson lesson = new Lesson();
            TakenLessons taken = new TakenLessons();
            Console.WriteLine("Enter lesson theme:");
            lesson.Name = Console.ReadLine();
            db.GetTable<Lesson>().InsertOnSubmit(lesson);
            db.SubmitChanges();
           // UpdateTakenLessons();
            
        }

       /* private void UpdateTakenLessons()
        {
            Table<Lesson> lessons = db.GetTable<Lesson>();
            
            
            foreach (var item in db.GetTable<Lesson>())
            {
                var query = from u in db.GetTable<TakenLessons>()
                            where u.LessonID == item.ID
                            select u;
                if (query.Count()==0)
                {
                    TakenLessons taken = new TakenLessons();
                    taken.LessonID = item.ID; 
                    db.GetTable<TakenLessons>().InsertOnSubmit(taken);
                    db.SubmitChanges();
                }
            }
        }*/

        
        public void SetBookToStudent()
        {
            GetStudents();

            GetBooks();

            Console.WriteLine("Enter ID of student");// get student id
            if (!int.TryParse(Console.ReadLine(), out int stID))
            {
                Console.WriteLine("Uncorrect value"); return;
            }
            if (db.GetTable<Student>().SingleOrDefault(x => x.ID == stID) == null) { Console.WriteLine("There is no such student"); return; }
            Console.WriteLine("Enter ID of book");
            if (!int.TryParse(Console.ReadLine(), out int bkID))
            {
                Console.WriteLine("Uncorrect value"); return;
            }
            if (db.GetTable<Book>().SingleOrDefault(x => x.ID == bkID) == null) { Console.WriteLine("There is no such book"); return; }
            TakenBooks taken = new TakenBooks();
            taken.BookID = bkID;
            taken.StudentID = stID;
            if (db.GetTable<TakenBooks>().Contains(taken))
            {
                Console.WriteLine("Such entry already exists");
            }
            else
            {
                Book book = db.GetTable<Book>().Single(x => x.ID == bkID);
                if (book.Quantity >= 1)
                {
                    book.Quantity -= 1;
                    db.GetTable<TakenBooks>().InsertOnSubmit(taken);
                    db.SubmitChanges();
                }
                else { Console.WriteLine("We don't have copy of this book"); }

            }


        }

        public void SetLessonToStudent()
        {
            
            GetStudents();
            
            GetLessons();
            Console.WriteLine("Enter ID of student");
            if (!int.TryParse(Console.ReadLine(), out int stID))
            {
                Console.WriteLine("Uncorrect value"); return;
            }
            if (db.GetTable<Student>().SingleOrDefault(x => x.ID == stID) == null) { Console.WriteLine("There is no such student"); return; }
            Console.WriteLine("Enter ID of lesson");
            if (!int.TryParse(Console.ReadLine(), out int lsID))
            {
                Console.WriteLine("Uncorrect value"); return;
            }
            if (db.GetTable<Lesson>().SingleOrDefault(x => x.ID == lsID) == null) { Console.WriteLine("There is no such lesson"); return; }
            TakenLessons taken = new TakenLessons();
            taken.LessonID = lsID;
            taken.StudentID = stID;
            if (db.GetTable<TakenLessons>().Contains(taken))
            {
                Console.WriteLine("Such entry already exists");
            }
            else
            {
                db.GetTable<TakenLessons>().InsertOnSubmit(taken);
                db.SubmitChanges();
            }




        }

        public void GetAllAboutStudent()
        {
            var takenLessonId = from x in db.GetTable<TakenLessons>() // create query from db
                                join y in db.GetTable<Student>() on x.StudentID equals y.ID
                                select new { y.ID, y.Name, y.Surname, y.Year, x.LessonID };

            var takenLessons = from x in db.GetTable<Lesson>()
                               join y in takenLessonId on x.ID equals y.LessonID
                               select new { y.ID, y.Name, y.Surname, y.Year, LessonName = x.Name };

            var takenBookId = from x in db.GetTable<TakenBooks>()
                              join y in db.GetTable<Student>() on x.StudentID equals y.ID
                              select new { y.ID, y.Name, y.Surname, y.Year, x.BookID };

            var takenBooks = from x in db.GetTable<Book>()
                             join y in takenBookId on x.ID equals y.BookID
                             select new { y.ID, y.Name, y.Surname, y.Year, BookTitle = x.Title};
            GetStudents();
            Console.WriteLine("Input student ID");
            if (int.TryParse(Console.ReadLine(), out int ID))
            {
                takenLessons = from x in takenLessons
                               where x.ID == ID
                               select x;
                takenBooks = from x in takenBooks
                             where x.ID == ID
                             select x;
                
                
                var student = db.GetTable<Student>().SingleOrDefault(x => x.ID == ID);
                if (student ==null) { Console.WriteLine("There is no such student"); return; }
                Console.WriteLine($"Student {ID}: {student.Name} {student.Surname} {student.Year} ");
                if (takenLessons.Count() != 0)
                {
                        Console.WriteLine($"Student take such lessons: ");
                        foreach (var item in takenLessons)
                        {

                            Console.WriteLine($" {item.LessonName}");
                        }
                }
                else { Console.WriteLine($"Student don't take any lesson"); }

                if (takenBooks.Count() != 0)
                {
                        Console.WriteLine($"Student take such books: ");
                        foreach (var item in takenBooks)
                        {

                            Console.WriteLine($" {item.BookTitle}");
                        }
                }
                else { Console.WriteLine($"Student don't take any book"); }
               


            }
            else { Console.WriteLine("Student's ID must be integer number"); }
            
            
            
        }
        



        public void Dispose()
        {
            db.Dispose();
        }
    }
}
