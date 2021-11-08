using System;

namespace DepthChart.Domain
{
    public class SupportingPosition
    {
        public SupportingPosition(Guid leagueId, string name)
        {
         

            Id = Guid.NewGuid();
            LeagueId = leagueId;
            Name = name;
        }

        public Guid Id { get; }

        public Guid LeagueId { get; }

        public string Name { get; }
    }
}