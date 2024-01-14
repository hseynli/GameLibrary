using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.UnitTests.Fakes
{
    public class FakePlayerStatisticsService : IPlayerStatisticsService
    {
        private readonly Dictionary<string, PlayerStatistics> statsDictionary = new();

        public PlayerStatistics GetPlayerStatistics(string playerName)
        {
            return statsDictionary[playerName];
        }

        public void UpdatePlayerStatistics(PlayerStatistics stats)
        {
            statsDictionary[stats.PlayerName] = stats;
        }
    }
}
