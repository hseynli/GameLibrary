using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public interface IPlayerStatisticsService
    {
        PlayerStatistics GetPlayerStatistics(string playerName);
        void UpdatePlayerStatistics(PlayerStatistics stats);
    }
}
