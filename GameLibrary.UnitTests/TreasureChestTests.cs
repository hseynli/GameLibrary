using Xunit.Abstractions;

namespace GameLibrary.UnitTests;

public class TreasureChestTests : IDisposable
{
    private readonly Stack<TreasureChest> chests;
    private readonly ITestOutputHelper output;

    public TreasureChestTests(ITestOutputHelper output)
    {
        chests = new();
        this.output = output;
        output.WriteLine($"Initial chest count: {chests.Count}");
    }

    [Theory]
    [InlineData(true, true, true)]
    [InlineData(true, false, false)]
    [InlineData(false, true, true)]
    [InlineData(false, false, true)]
    public void CanOpen_WhenCalled_ReturnsExceptedOutcome(bool isLocked, bool hasKey, bool excpected)
    {
        // Arrange (Prepare)
        TreasureChest chest = new TreasureChest(isLocked);
        chests.Push(chest);
        output.WriteLine($"New chest count: {chests.Count}");

        // Act (Perform)
        bool actual = chest.CanOpen(hasKey);

        // Assert (Verify)
        Assert.Equal(excpected, actual);
        Assert.Single(chests);
    }

    // For use internal methods use this in the tested project:
    /*
     <ItemGroup>
        <InternalsVisibleTo Include="GameLibrary.UnitTests"/>
     </ItemGroup>
     */
    [Fact]
    public void Inverse_WhenCalled_ThenInverseTheBoolValue()
    {
        // Arrange (Prepare)
        TreasureChest chest = new TreasureChest(true);

        // Act (Perform)
        bool actual = chest.Inverse(true);
        output.WriteLine($"Actual value: {actual}");

        // Assert (Verify)
        Assert.False(actual);
    }

    // For ignore some unit test use skip property
    [Fact(Skip = "Unit test is not ready yet")]
    public void CanOpen_ChestIsLockedAndHasKey_ReturnsTrue()
    {
        // Arrange (Prepare)
        TreasureChest chest = new TreasureChest(true);
        chests.Push(chest);
        output.WriteLine($"New chest count: {chests.Count}");

        // Act (Perform)
        bool result = chest.CanOpen(true);

        // Assert (Verify)
        Assert.True(result);
        Assert.Single(chests);
    }

    [Fact]
    public void CanOpen_ChestIsLockedAndHasNoKey_ReturnsFalse()
    {
        // Arrange (Prepare)
        TreasureChest chest = new TreasureChest(true);
        chests.Push(chest);

        // Act (Perform)
        bool result = chest.CanOpen(false);

        // Assert (Verify)
        Assert.False(result);
        Assert.Single(chests);
    }

    public void Dispose()
    {
        if (chests.Count > 0)
        {
            chests.Pop();
            Assert.Empty(chests);
        }
    }
}