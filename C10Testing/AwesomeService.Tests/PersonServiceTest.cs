using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AwesomeService.Tests
{
    public class PersonService_Should
    {
        [Fact]
        public void Return_Count_Of_letters()
        {
            var personRepositoryMock = new Mock<IPersonRepository>();
            personRepositoryMock.Setup(p => p.GetNames()).Returns(new string[] { "Fred", "Wilma" });

            PersonService personService = new PersonService(personRepositoryMock.Object);

            var actualResult = personService.CountLetters();
            var expectedResult = 9;

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
