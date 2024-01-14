using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.UnitTests
{
    public class PlayerTests
    {
        [Fact]
        public void IncreaseLevel_WhenCalled_HasExcpectedLevel()
        {
            // Arrange (Prepare)
            Player player = new Player("Alice", 1, DateTime.Now);

            // Act (Perform)
            player.IncreaseLevel();

            // Assert (Verify)
            Assert.Equal(2, player.Level);

            player.Level.Should().Be(2);
            player.Level.Should().BeGreaterThan(1);
            player.Level.Should().BeGreaterThanOrEqualTo(2);
            player.Level.Should().BePositive();
            player.Level.Should().NotBe(1);

            //Assert.InRange(player.Level, 1, 100);
            player.Level.Should().BeInRange(2, 100);
        }

        [Fact]
        public void Greet_ValidGreeting_ReturnsGreetingWithPlayersName()
        {
            // Arrange (Prepare)
            Player player = new Player("Alice", 1, DateTime.Now);

            // Act (Perform)
            string actual = player.Greet("Hello");

            // Assert (Verify)
            //Assert.Equal("Hello, Alice!", actual);
            //Assert.Contains("Alice", actual);
            //Assert.EndsWith("Alice!", actual);
            //Assert.NotNull(actual);
            //Assert.NotEmpty(actual);

            actual.Should().Be("Hello, Alice!");
            actual.Should().Contain("Alice");
            actual.Should().EndWith("Alice!");
            actual.Should().NotBeNullOrEmpty();
            actual.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void Constructor_OnNewInstance_SetsJoinDate()
        {
            // Arrange (Prepare)
            DateTime currentDate = DateTime.Now;

            // Act (Perform)
            Player player = new Player("Alice", 1, currentDate);

            // Assert (Verify)
            Assert.Equal(currentDate, player.JoinDate);

            player.JoinDate.Should().Be(currentDate);
        }
    }
}
