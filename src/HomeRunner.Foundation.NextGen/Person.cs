
using System;
using System.Collections.Generic;

namespace HomeRunner.Foundation.NextGen
{
    public class Person
    {
        public Person(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public int AgeInDays(int years) => years * 365;

        public string FullName { get { return $"{Name} {FullName}"; } }
    }

    public class Organisation
    {
        public Dictionary<Guid, Person> Persons { get; private set; } = new Dictionary<Guid, Person>();

        public Person Add(Guid id, Person person)
        {
            this.Persons.Add(person?.Id ?? id, person ?? new Person(id));

            return this.Persons[id];
        }

    }
}
