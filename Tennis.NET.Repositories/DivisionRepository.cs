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
    public class DivisionRepository
    {
        public List<Division> getDivisions()
        {
            List<Division> divisions = new List<Division>();
            using (var context = new TennisContext())
            {
                divisions = context.Divisions.ToList();
            }
            return divisions;
        }

        public Division getDivisionById(int? id)
        {
            Division div;
            using (var context = new TennisContext())
            {
                div = context.Divisions.Include(d => d.TeamPlayers.Select(tp => tp.Player))
                    .Include(d => d.TeamPlayers.Select(tp => tp.Team))
                    .SingleOrDefault(d => d.Id == id);
            }
            return div;
        }

        public void createDivision(Division div)
        {
            using (var context = new TennisContext())
            {
                context.Divisions.Add(div);
                context.SaveChanges();
            }
        }

        public void modifyDivision(Division div)
        {
            using (var context = new TennisContext())
            {
                context.Entry(div).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void deleteDivision(int id)
        {
            using (var context = new TennisContext())
            {
                Division div = context.Divisions.Include(d => d.TeamPlayers.Select(tp => tp.Player))
                    .Include(d => d.TeamPlayers.Select(tp => tp.Team))
                    .SingleOrDefault(d => d.Id == id);
                foreach (TeamPlayer tp in div.TeamPlayers)
                {
                    context.Entry(tp).State = EntityState.Deleted;
                }
                context.Entry(div).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
