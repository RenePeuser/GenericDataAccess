using System;
using System.Linq;
using System.Threading.Tasks;
using Cli.DataAccess;
using Cli.Models;
using ConsoleTables;

namespace Cli
{
    internal class App
    {
        private readonly EntityFactory _entityFactory;
        private readonly Repository<Person> _personRepo;

        public App(Repository<Person> personRepo, EntityFactory entityFactory)
        {
            _personRepo = personRepo;
            _entityFactory = entityFactory;
        }

        public Task<int> RunAsync()
        {
            PrintAllPersonsFromDatabase();

            AddSomePersons(10);

            PrintAllPersonsFromDatabase();

            DeleteSomePersons(4);

            PrintAllPersonsFromDatabase();

            return Task.FromResult(0);
        }

        private void DeleteSomePersons(int i)
        {
            Console.WriteLine();
            Console.WriteLine($"Delete {i} persons from database");
            _personRepo.Delete(_personRepo.GetAll().Take(4).ToArray());
        }

        private void AddSomePersons(int i)
        {
            Console.WriteLine();
            Console.WriteLine($"Add {i} persons to database");
            var newPersons = _entityFactory.Create<Person>().Take(10).ToArray();
            _personRepo.Add(newPersons);
        }

        private void PrintAllPersonsFromDatabase()
        {
            Console.WriteLine();
            Console.WriteLine("Current state of persons in database:");

            var allPersons = _personRepo.GetAll();
            var personTableAfterAdd = ConsoleTable.From(allPersons);
            Console.WriteLine(personTableAfterAdd.ToString());
        }
    }
}