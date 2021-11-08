using System;

namespace DepthChart.Domain
{
    public class PlayerPosition
    {
        public PlayerPosition(League league, Team team, Player player, SupportingPosition supportingPosition, int? supportingPositionRanking)
        {
           
            League = league;
            Team = team;
            Player = player;
            SupportingPosition = supportingPosition;
            SupportingPositionRanking = supportingPositionRanking;
        }

        public League League { get; }

        public Team Team { get; }

        public Player Player { get; }

        public SupportingPosition SupportingPosition { get; }

        public int? SupportingPositionRanking { get; }
    }
}