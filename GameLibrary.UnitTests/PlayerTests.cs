using FluentAssertions;

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

        [Fact]
        public void AddItemToInventory_WithNewItem_AddsItemToInventory()
        {
            // Arrange (Prepare)
            Player player = new Player("Alice", 1, DateTime.Now);
            InventoryItem item = new InventoryItem(101, "Sword", "A sharp blade.");

            // Act (Perform)
            player.AddItemToInventory(item);

            // Assert (Verify)
            player.InventoryItems.Should().HaveCount(1);
            player.InventoryItems.Should().NotBeEmpty();
            player.InventoryItems.Should().Contain(item);
            player.InventoryItems.Should().ContainSingle(item => item.Id == 101 && item.Name == "Sword");
        }

        [Fact]
        public void Greet_NullOrEmptyGreeting_ThrowsArgumentException()
        {
            // Arrange (Prepare)
            Player player = new Player("Alice", 1, DateTime.Now);

            // Act (Perform)
            Action act = () => player.Greet(null);

            // Assert (Verify)
            //Assert.Throws<ArgumentException>(act);
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void IncreaseLevel_WhenCalled_RaisesLevelUpEvent()
        {
            // Arrange (Prepare)
            Player player = new Player("Alice", 1, DateTime.Now);
            using var monitor = player.Monitor();

            // Act (Perform)
            player.IncreaseLevel();

            // Assert (Verify)
            monitor.Should().Raise(nameof(player.LevelUp));
        }
    }
}