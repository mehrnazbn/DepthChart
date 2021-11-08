using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepthChart.Domain
{
    public class Team
    {


        public Guid Id { get; }
        public League Leagued { get; }
        public League League { get; }
        public string Name { get; }


        private List<Player> _players = new List<Player>();
        private List<PlayerPosition> _playerPositions = new List<PlayerPosition>();

        public Team(League league, string name)
        {
          
            Id = Guid.NewGuid();
            League = league;
            Name = name;
        }

        public IEnumerable<Player> Players => _players.AsReadOnly();

        public IEnumerable<PlayerPosition> PlayerPositions
        {
            get
            {
                var supportings = _playerPositions.GroupBy(pp => new { pp.SupportingPosition.Id, pp.SupportingPositionRanking });

                foreach (var supporting in supportings)
                {
                    var supportingPositionRankingGroups = supporting.GroupBy(pp => pp.SupportingPositionRanking);

                    foreach (var supportingPositionRankingGroup in supportingPositionRankingGroups)
                    {
                        foreach (var playerPosition in supportingPositionRankingGroup.Where(r => r.SupportingPositionRanking >= 0).Reverse())
                        {
                            yield return playerPosition;
                        }
                    }
                }
            }
        }

        public Player AddPlayer(string name)
        {
       
            var player = new Player(League, this, name);
            _players.Add(player);
            return player;
        }

        
        public IEnumerable<PlayerPosition> GetBackupPlayerPositions(Guid playerId, Guid supportingPositionId)
        {
            var playerPositions = PlayerPositions.ToList();
            var playerPoIndex = playerPositions.FindIndex(pp => pp.Player.Id == playerId && pp.SupportingPosition.Id == supportingPositionId);
            return playerPositions.Where(pp => pp.SupportingPosition.Id == supportingPositionId).Skip(playerPoIndex + 1);
        }

        public PlayerPosition UpdatePlayerPosition(Guid playerId, Guid supportingPositionId, int? supportingPositionRanking = null)
        {

           
            //with supporting position

            if (supportingPositionRanking != null)
            {
                _playerPositions.RemoveAll(pp => pp.Player.Id == playerId && pp.SupportingPosition.Id == supportingPositionId);

                // Update position
                var player = _players.Find(p => p.Id == playerId);
                var supportingPosition = League.SupportingPositions.FirstOrDefault(s => s.Id == supportingPositionId);
                var playerPosition = new PlayerPosition(League, this, player, supportingPosition, supportingPositionRanking);

                var exsitingPlayerPosition = _playerPositions.FirstOrDefault(pp => pp.SupportingPositionRanking == supportingPositionRanking);

                if (exsitingPlayerPosition == null)
                {
                    _playerPositions.Add(playerPosition);
                }
                // if exsiting position , move the existing player one level down 
                else
                {

                    _playerPositions.RemoveAll(pp => pp.Player.Id == exsitingPlayerPosition.Player.Id && pp.SupportingPosition.Id == exsitingPlayerPosition.SupportingPosition.Id);
  
                    var playerPositionExisting = new PlayerPosition(League, this, exsitingPlayerPosition.Player, supportingPosition, supportingPositionRanking + 1);
                    _playerPositions.Add(playerPositionExisting);

                    var playerPositionNew = new PlayerPosition(League, this, player, supportingPosition, supportingPositionRanking);
                    _playerPositions.Add(playerPositionNew);
                }

                return playerPosition;
            }

           // with no supporting position add to end of chart
            else
            {
                _playerPositions.RemoveAll(pp => pp.Player.Id == playerId);
                var player = _players.Find(p => p.Id == playerId);
                var supportingPosition = League.SupportingPositions.FirstOrDefault(s => s.Id == supportingPositionId);
                var lastPosition = _playerPositions.Max(p => p.SupportingPositionRanking);
                var playerPosition = new PlayerPosition(League, this, player, supportingPosition, lastPosition + 1);
                _playerPositions.Add(playerPosition);
                return playerPosition;

            }
        }


            public void removePlayerFromDepthChart(Guid playerId, Guid supportingPositionId, int? supportingPositionRanking = null)
            {

                _playerPositions.RemoveAll(pp => pp.Player.Id == playerId && pp.SupportingPosition.Id == supportingPositionId);
            }


        public List<string> getFullDepthChart()
        {
            List<string> results = new List<string>();
            foreach (var supportingPositionInfo in _playerPositions.GroupBy(p => p.SupportingPosition.Name))
            {
                results.Add($"{supportingPositionInfo.Key}: [{string.Join(",", supportingPositionInfo.Select(s => $"{s.Player.Name}"))}]");
            }


            return results;

        }




    }
}
