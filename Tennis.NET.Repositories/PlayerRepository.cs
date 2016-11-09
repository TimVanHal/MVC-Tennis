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
    public class PlayerRepository
    {
        public List<Player> getPlayers()
        {
            List<Player> players = new List<Player>();
            using (var context = new TennisContext())
            {
                players = context.Players.ToList();
            }
            return players;
        }

        public Player getPlayerById(int? id)
        {
            Player player;
            using (var context = new TennisContext())
            {
                player = context.Players.Include(p => p.Roles)
                    .Include(p => p.Fines)
                    .Include(p => p.TeamPlayers.Select(tp => tp.Division))
                    .Include(p => p.TeamPlayers.Select(tp => tp.Team))
                    .SingleOrDefault(p => p.Id == id);
            }
            return player;
        }

        public void createPlayer(Player player)
        {
            using (var context = new TennisContext())
            {
                context.Players.Add(player);
                context.SaveChanges();
            }
        }

        public void modifyPlayer(Player player)
        {
            using (var context = new TennisContext())
            {
                removeAllRoles(player.Id);

                Player p = new Player
                {
                    BirthDay = player.BirthDay,
                    City = player.City,
                    FederationNr = player.FederationNr,
                    FirstName = player.FirstName,
                    Gender = player.Gender,
                    HouseNr = player.HouseNr,
                    Id = player.Id,
                    LastName = player.LastName,
                    MailBox = player.MailBox,
                    PhoneNr = player.PhoneNr,
                    PlayerNr = player.PlayerNr,
                    Street = player.Street,
                    YearOfJoin = player.YearOfJoin,
                    Zipcode = player.Zipcode,
                    Roles = new List<Role>()
                };
                context.Players.Attach(p);

                foreach (Role role in player.Roles)
                {
                    Role r = new Role { Id = role.Id };
                    context.Roles.Attach(r);

                    p.Roles.Add(r);
                }
                context.Entry(p).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private void removeAllRoles(int id)
        {
            using (var context = new TennisContext())
            {
                Player p = context.Players.Include(pl => pl.Roles).SingleOrDefault(pl => pl.Id == id);
                p.Roles.Clear();
                context.SaveChanges();
            }
        }

        public void deletePlayer(int id)
        {
            using (var context = new TennisContext())
            {
                Player player = context.Players //.Include(p => p.Roles)
                    .Include(p => p.Fines)
                    .Include(p => p.TeamPlayers.Select(tp => tp.Division))
                    .Include(p => p.TeamPlayers.Select(tp => tp.Team))
                    .SingleOrDefault(p => p.Id == id);
                List<Game> playerGames = context.Games.Where(g => g.Player1Id == id || g.Player2Id == id).ToList();
                foreach (Game g in playerGames)
                {
                    context.Entry(g).State = EntityState.Deleted;
                }
                foreach (Fine f in player.Fines)
                {
                    context.Entry(f).State = EntityState.Deleted;
                }
                foreach (TeamPlayer tp in player.TeamPlayers)
                {
                    context.Entry(tp).State = EntityState.Deleted;
                }
                context.Entry(player).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
