using System;
using System.Threading.Tasks;
using DepthChart.Dto;

namespace DepthChart.Services
{
    public interface IDepthChartService
    {
       TeamDto AddLeague(string leagueName);

        SupportingPositionDto AddSupportingPosition(Guid leagueId, string supportingPositionName);

       
        PlayerDto AddPlayer(Guid leagueId, Guid teamId, string playerName);

       
    }
}
