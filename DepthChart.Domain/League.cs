using System;
using System.Collections.Generic;

namespace DepthChart.Domain
{
    public class League
    {
        private List<Team> _teams = new List<Team>();

        private List<SupportingPosition> _supportingPositions = new List<SupportingPosition>();

        public League(string name)
        {
          
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }

        public IEnumerable<Team> Teams => _teams.AsReadOnly();

        public IEnumerable<SupportingPosition> SupportingPositions => _supportingPositions.AsReadOnly();

        public Team GetTeam(Guid id)
        {
            return _teams.Find(t => t.Id == id);
        }

        public Team AddTeam(string name)
        {
  

            var team = new Team(this, name);
            _teams.Add(team);
            return team;
        }

        public SupportingPosition AddSupportingPosition(string name)
        {
       
            var supportingPosition = new SupportingPosition(Id, name);
            _supportingPositions.Add(supportingPosition);
            return supportingPosition;
        }
    }
}