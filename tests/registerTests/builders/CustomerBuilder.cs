using register.app.ViewModels;
using register.domain.Enum;
using System;

namespace registerTests.builders
{
    public class CustomerBuilder
    {
        public Guid _id;
        public string _firstName;
        public string _lastName;
        public DateTime _birthDate;
        public GenderType _gender;

        public CustomerViewModel BuildCustomer()
        {
            return new CustomerBuilder()
                        .FirstName("John")
                        .LastName("Doe")
                        .Birthdate(DateTime.Parse("2000-04-21"))
                        .Gender(GenderType.Male)
                        .Build();
        }

        public CustomerViewModel Build()
        {
            return new CustomerViewModel()
            {
                Id = _id,
                FirstName = _firstName,
                LastName = _lastName,
                Birthdate = _birthDate,
                Gender = _gender
            };
        }

        public CustomerBuilder Id(Guid id)
        {
            _id = id;
            return this;
        }

        public CustomerBuilder FirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public CustomerBuilder LastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public CustomerBuilder Birthdate(DateTime birthdate)
        {
            _birthDate = birthdate;
            return this;
        }

        public CustomerBuilder Gender(GenderType gender)
        {
            _gender = gender;
            return this;
        }


    }
}
