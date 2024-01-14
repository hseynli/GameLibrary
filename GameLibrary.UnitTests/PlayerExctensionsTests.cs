using FluentAssertions;

namespace GameLibrary.UnitTests
{
    public class PlayerExctensionsTests
    {
        [Fact]
        public void ToDo_WhenCalled_MapsProperties()
        {
            // Arrange
            Player player = new Player("Alice", 1, DateTime.Now);
            InventoryItem item = new InventoryItem(101, "Sword", "A sharp blade.");
            player.AddItemToInventory(item);

            // Act
            PlayerDto dto = player.ToDto();

            // Assert

            //Assert.Equivalent(player, dto);
            dto.Should().BeEquivalentTo(player, options => 
                                            options.Excluding(s => s.InventoryItems)
                                                   .Excluding(s => s.ExperiencePoints));
        }

        [Fact]
        public void ToDo_WhenCalled_MapsProperties_Excluding()
        {
            // Arrange
            Player player = new Player("Alice", 1, DateTime.Now);
            InventoryItem item = new InventoryItem(101, "Sword", "A sharp blade.");
            player.AddItemToInventory(item);

            // Act
            PlayerDto dto = player.ToDto();

            // Assert

            //Assert.Equivalent(player, dto);
            dto.Should().BeEquivalentTo(player, options => options.ExcludingMissingMembers());
        }
    }
}
