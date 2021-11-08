using System;

namespace DepthChart.Domain
{
    public class Player
    {


        public Player(League league, Team team, string name)
        {
           

            Id = Guid.NewGuid();
            League = league;
            Team = team;
            Name = name;
        }

        public Guid Id { get; }

        public League League { get; }

        public Team Team { get; }

        public string Name { get; }
    }
}