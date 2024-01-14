using FluentAssertions;
using GameLibrary.UnitTests.Fakes;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.UnitTests
{
    public class GameWorldTests
    {
        // STUBS VS MOCKS
        // We use stubs in the Arrange phase of the test.

        [Fact]
        public void GetPlayerReport_PlayerExists_ReturnsExpectedReport()
        {
            // Arrange
            Player player = new Player("Alice", 10, new DateTime(2020, 1, 1));


            // This is a stub, not a mock. It's a fake implementation of the IPlayerStatisticsService interface.
            FakePlayerStatisticsService playerStatisticsService = new FakePlayerStatisticsService();
            var stats = new PlayerStatistics
            {
                PlayerName = player.Name,
                GamesPlayed = 10,
                TotalScore = 1000
            };
            playerStatisticsService.UpdatePlayerStatistics(stats);

            var expected = new PlayerReportDto(
                               player.Name,
                               player.Level,
                               player.JoinDate,
                               stats.GamesPlayed,
                               stats.TotalScore,
                               stats.TotalScore / stats.GamesPlayed);

            GameWorld gameWorld = new GameWorld(playerStatisticsService);

            // Act
            PlayerReportDto actual = gameWorld.GetPlayerReport(player);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetPlayerReport_PlayerExists_ReturnsExpectedReport_WithStubs()
        {
            // Arrange
            Player player = new Player("Alice", 10, new DateTime(2020, 1, 1));

            var stats = new PlayerStatistics
            {
                PlayerName = player.Name,
                GamesPlayed = 10,
                TotalScore = 1000
            };

            // Fake object
            var statisticsServiceStub = Substitute.For<IPlayerStatisticsService>();
            statisticsServiceStub.GetPlayerStatistics(player.Name).Returns(stats);

            var expected = new PlayerReportDto(
                               player.Name,
                               player.Level,
                               player.JoinDate,
                               stats.GamesPlayed,
                               stats.TotalScore,
                               stats.TotalScore / stats.GamesPlayed);

            GameWorld gameWorld = new GameWorld(statisticsServiceStub);

            // Act
            PlayerReportDto actual = gameWorld.GetPlayerReport(player);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetPlayerGameWinMethod_ValidPlayerAndScore_UpdatePlayerStatistics_WithMocks()
        {
            // Arrange
            Player player = new Player("Alice", 10, new DateTime(2020, 1, 1));     

            var stats = new PlayerStatistics
            {
                PlayerName = player.Name,
                GamesPlayed = 10,
                TotalScore = 1000
            };

            // Fake object
            var statisticsServiceMock = Substitute.For<IPlayerStatisticsService>();
            statisticsServiceMock.GetPlayerStatistics(player.Name).Returns(stats);

            GameWorld gameWorld = new GameWorld(statisticsServiceMock);

            // Act
            gameWorld.RecordPlayerGameWin(player, 20);

            // Assert
            statisticsServiceMock.Received().UpdatePlayerStatistics(Arg.Is<PlayerStatistics>(
                                                                    stats => stats.PlayerName == player.Name &&
                                                                    stats.GamesPlayed == 11
                                                                    && stats.TotalScore == 1020));
        }

        [Fact]
        public void GetPlayerGameWinMethod_ValidPlayerAndScore_UpdatePlayerStatistics_MockLibrary()
        {
            // Arrange
            Player player = new Player("Alice", 10, new DateTime(2020, 1, 1));

            var stats = new PlayerStatistics
            {
                PlayerName = player.Name,
                GamesPlayed = 10,
                TotalScore = 1000
            };

            var statisticsServiceStub = new Mock<IPlayerStatisticsService>();
            statisticsServiceStub.Setup(x => x.GetPlayerStatistics(player.Name)).Returns(stats);

            var expected = new PlayerReportDto(
                              player.Name,
                              player.Level,
                              player.JoinDate,
                              stats.GamesPlayed,
                              stats.TotalScore,
                              stats.TotalScore / stats.GamesPlayed);

            GameWorld stub = new GameWorld(statisticsServiceStub.Object);

            // Act
            var actual = stub.GetPlayerReport(player);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}