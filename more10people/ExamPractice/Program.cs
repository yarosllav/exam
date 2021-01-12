using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPractice
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Latittude { get; set; }
        public int Longitude { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public int CityId { get; set; }
    }

    public class FluentContext : DbContext
    {
        public FluentContext() : base("exam")
        { }
        public DbSet<Person> Persons { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasKey(x => x.Id);
            modelBuilder.Entity<Person>().Property(x => x.Name).HasMaxLength(128);
            modelBuilder.Entity<City>().HasKey(x => x.Id);
            modelBuilder.Entity<Person>().Property(x => x.Address.Street).IsRequired();
        }
    }
    class Program
    {
        public class AddressRepository
        {
            public IEnumerable<string> GetStreets(int cityId)
            {
                using (var _context = new FluentContext())
                {
                    return _context.Persons.Where(x => x.Address.CityId == cityId).Select(x => x.Address.Street).Where(x => x.Count() > 10).ToList();
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hi");
        }
    }
}
