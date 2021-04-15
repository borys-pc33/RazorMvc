using RazorMvc.Services;
using System.Linq;
using Xunit;

namespace RazorMvc.Tests
{
    public class InternshipServiceTests
    {
        [Fact]
        public void InitiallyContainsThreeMembers()
        {
            // Assume
            var intershipService = new InternshipService();

            // Act

            // Assert
            Assert.Equal(3, intershipService.GetMembers().Count);
        }

        [Fact]
        public void WhenAddMemberItShouldBeThere()
        {
            // Assume
            var intershipService = new InternshipService();

            // Act
            intershipService.AddMember("Marko");

            // Assert
            Assert.Equal(4, intershipService.GetMembers().Count);
            Assert.Contains("Marko", intershipService.GetMembers().Select(member => member.Name));
        }
    }
}
