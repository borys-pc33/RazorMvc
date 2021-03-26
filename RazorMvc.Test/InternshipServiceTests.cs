using RazorMvc.Services;
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
            Assert.Equal(3, intershipService.GetClass().Members.Count);
        }

        [Fact]
        public void WhenAddMemberItShouldBeThere()
        {
            // Assume
            var intershipService = new InternshipService();

            // Act
            intershipService.AddMember("Marko");

            // Assert
            Assert.Equal(4, intershipService.GetClass().Members.Count);
            Assert.Contains("Marko", intershipService.GetClass().Members);
        }
    }
}
