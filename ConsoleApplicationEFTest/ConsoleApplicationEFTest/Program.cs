using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplicationEFTest.Model;

namespace ConsoleApplicationEFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            InitData();
            Console.WriteLine("Result with deleted items:");
            foreach (var name in DisplayWithDeleted())
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("Result without deleted items:");
            foreach (var name in DisplayWithoutDeleted())
            {
                Console.WriteLine(name);
            }
            Console.ReadKey();
        }

        public static List<string> DisplayWithDeleted()
        {
            using (var db = new AppContext())
            {
                db.SoftDeletableFilter.Disable();
                return db.Entities.Select(e => e.Name).ToList();
            }
        }

        public static List<string> DisplayWithoutDeleted()
        {
            using (var db = new AppContext())
            {
                db.SoftDeletableFilter.Enable();
                return db.Entities.Select(e => e.Name).ToList();
            }
        }

        public static void InitData()
        {
            using (var db = new AppContext())
            {
                if (!db.Entities.Any())
                {
                    var entityList = new List<TestEntity>
                    {
                        new TestEntity
                        {
                            Id = Guid.NewGuid(),
                            Name = "DeletedItem1",
                            Deleted = true
                        },
                        new TestEntity
                        {
                            Id = Guid.NewGuid(),
                            Name = "Item1",
                            Deleted = false
                        },
                        new TestEntity
                        {
                            Id = Guid.NewGuid(),
                            Name = "DeletedItem2",
                            Deleted = true
                        },
                        new TestEntity
                        {
                            Id = Guid.NewGuid(),
                            Name = "Item2",
                            Deleted = false
                        }
                    };
                    db.Entities.AddRange(entityList);
                    db.SaveChanges();
                }
            }
        }
    }
}
