using FluentAssertions;
using register.domain.Entities;
using register.domain.Enum;
using System;
using Xunit;

namespace registerTests.domainTests
{
    public class CustomerEntityTests
    {

        [Fact(DisplayName = "Validate object to Costumer entity")]
        [Trait("Entity", "Customer")]
        public void should_create_Costumer()
        {
            //Arrange
            var costumerExpected = new
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                BirthDate = Convert.ToDateTime("2000-04-21"),
                Gender = GenderType.Male
            };

            //Act
            var costumer = new Customer(costumerExpected.Id, costumerExpected.FirstName, costumerExpected.LastName, costumerExpected.BirthDate, costumerExpected.Gender);
            
            //Assert
            costumerExpected.Should().Equals(costumer);
            costumer.Id.Should().Be(costumerExpected.Id);
            costumer.FirstName.Should().Be(costumerExpected.FirstName);
            costumer.LastName.Should().Be(costumerExpected.LastName);
            costumer.Birthdate.Should().Be(costumerExpected.BirthDate);
        }
    }
}
