using Xunit;

namespace RazorMvc.Tests
{
    public class StartupTests
    {
        [Fact]
        public void CheckIfHerokuConnectionStringCanBeConvertedToAspNetString()
        {
            // Assume
            var herokuConnectionString = "postgres://ogznvyzypeszdt:6145f42c1b3f4bc2c50f9f7d3a2ae7c818954198903a3f8fabc074f1ce25a9f5@ec2-54-247-158-179.eu-west-1.compute.amazonaws.com:5435/db9cc8f3mbd3rm";

            // Act
            var aspNetConnectionString = Startup.GetHerokuConnectionString(herokuConnectionString);

            // Assume
            Assert.Equal("User ID=ogznvyzypeszdt;Password=6145f42c1b3f4bc2c50f9f7d3a2ae7c818954198903a3f8fabc074f1ce25a9f5;Host=ec2-54-247-158-179.eu-west-1.compute.amazonaws.com;Port=5435;Database=db9cc8f3mbd3rm;Pooling=true;SSL Mode=Require;Trust Server Certificate=True;", aspNetConnectionString);
        }
    }
}
