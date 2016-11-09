using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.NET.DataModel;
using Tennis.NET.Models;

namespace Tennis.NET.Repositories
{
    public class TeamPlayerRepository
    {
        public List<TeamPlayer> getTeamPlayers()
        {
            List<TeamPlayer> tp = new List<TeamPlayer>();
            using (var context = new TennisContext())
            {
                tp = context.TeamPlayers.Include(t => t.Division).Include(t => t.Player).Include(t => t.Team).ToList();
            }
            return tp;
        }

        public TeamPlayer getTeamPlayerById(int? playerId, int? divisionId, int? teamId)
        {
            TeamPlayer tp;
            using (var context = new TennisContext())
            {
                tp = context.TeamPlayers.Include(t => t.Division).Include(t => t.Player).Include(t => t.Team)
                    .SingleOrDefault(t => t.DivisionId == divisionId && t.PlayerId == playerId && t.TeamId == teamId);
            }
            return tp;
        }

        public void createTeamPlayer(TeamPlayer tp)
        {
            using (var context = new TennisContext())
            {
                context.TeamPlayers.Add(tp);
                context.SaveChanges();
            }
        }

        public void deleteTeamPlayer(int playerId, int divisionId, int teamId)
        {
            TeamPlayer tp = getTeamPlayerById(playerId, divisionId, teamId);
            using (var context = new TennisContext())
            {
                context.Entry(tp).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
