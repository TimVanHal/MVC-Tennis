using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.NET.DataModel;
using Tennis.NET.Models;

namespace Tennis.NET.Repositories
{
    public class RoleRepository
    {
        public List<Role> getRoles()
        {
            List<Role> roles = new List<Role>();
            using (var context = new TennisContext())
            {
                roles = context.Roles.ToList();
            }
            return roles;
        }

        public List<Role> getRolesFiltered(int playerId)
        {
            List<Role> filteredList = new List<Role>();
            using (var context = new TennisContext())
            {
                Player p = context.Players.Find(playerId);
                foreach (Role r in context.Roles)
                {
                    if (r.Players.Contains(p))
                    {
                        filteredList.Add(r);
                    }
                }
            }
            return filteredList;
        }

        public Role getRoleById(int? id)
        {
            Role role;
            using (var context = new TennisContext())
            {
                role = context.Roles.Include(r => r.Players).SingleOrDefault(r => r.Id == id);
            }
            return role;
        }

        public void createRole(Role role)
        {
            using (var context = new TennisContext())
            {
                context.Roles.Add(role);
                context.SaveChanges();
            }
        }

        public void modifyRole(Role role)
        {
            using (var context = new TennisContext())
            {
                removeAllPlayers(role.Id);

                Role r = new Role { Id = role.Id, Name = role.Name, Players = new List<Player>() };
                context.Roles.Attach(r);

                foreach (Player player in role.Players)
                {
                    Player p = new Player { Id = player.Id };
                    context.Players.Attach(p);
                    
                    r.Players.Add(p);
                }
                context.Entry(r).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private void removeAllPlayers(int id)
        {
            using (var context = new TennisContext())
            {
                Role r = context.Roles.Include(rl => rl.Players).SingleOrDefault(rl => rl.Id == id);
                r.Players.Clear();
                context.SaveChanges();
            }
        }

        public void deleteRole(int id)
        {
            using (var context = new TennisContext())
            {
                Role role = context.Roles.Find(id);
                context.Entry(role).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void removePlayer(int roleId, int playerId)
        {
            Role role = getRoleById(roleId);
            using (var context = new TennisContext())
            {
                Player player = context.Players.Find(playerId);
                role.Players.Remove(player);
                player.Roles.Remove(role);
                context.SaveChanges();
            }
        }
    }
}
