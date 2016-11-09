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
    public class TeamRepository
    {
        public List<Team> getTeams()
        {
            List<Team> teams = new List<Team>();
            using (var context = new TennisContext())
            {
                teams = context.Teams.ToList();
            }
            return teams;
        }

        public Team getTeamById(int? id)
        {
            Team team;
            using (var context = new TennisContext())
            {
                team = context.Teams.Include(t => t.TeamPlayers.Select(tp => tp.Division))
                    .Include(t => t.TeamPlayers.Select(tp => tp.Player))
                    .SingleOrDefault(t => t.Id == id);
            }
            return team;
        }

        public void createTeam(Team team)
        {
            using (var context = new TennisContext())
            {
                context.Teams.Add(team);
                context.SaveChanges();
            }
        }

        public void modifyTeam(Team team)
        {
            using (var context = new TennisContext())
            {
                context.Entry(team).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void deleteTeam(int id)
        {
            using (var context = new TennisContext())
            {
                Team team = context.Teams.Include(t => t.TeamPlayers.Select(tp => tp.Division))
                    .Include(t => t.TeamPlayers.Select(tp => tp.Player))
                    .SingleOrDefault(t => t.Id == id);
                foreach (TeamPlayer tp in team.TeamPlayers)
                {
                    context.Entry(tp).State = EntityState.Deleted;
                }
                context.Entry(team).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
