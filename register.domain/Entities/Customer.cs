using register.domain.Enum;
using System;

namespace register.domain.Entities
{
    public class Customer : Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime Birthdate { get; private set; }
        public GenderType Gender { get; private set; }

        public Customer(Guid id, string firstName, string lastName, DateTime birthdate, GenderType gender)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Birthdate = birthdate;
            Gender = gender;
        }

        protected Customer() { }
    }
}
