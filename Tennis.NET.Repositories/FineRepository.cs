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
    public class FineRepository
    {
        public List<Fine> getFines()
        {
            List<Fine> fines = new List<Fine>();
            using (var context = new TennisContext())
            {
                fines = context.Fines.Include(f => f.Player).ToList();
            }
            return fines;
        }

        public Fine getFineById(int? id)
        {
            Fine fine;
            using (var context = new TennisContext())
            {
                fine = context.Fines.Include(f => f.Player).SingleOrDefault(f => f.Id == id);
            }
            return fine;
        }

        public void createFine(Fine fine)
        {
            using (var context = new TennisContext())
            {
                context.Fines.Add(fine);
                context.SaveChanges();
            }
        }

        public void modifyFine(Fine fine)
        {
            using (var context = new TennisContext())
            {
                context.Entry(fine).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void deleteFine(int id)
        {
            Fine fine = getFineById(id);
            using (var context = new TennisContext())
            {
                context.Entry(fine).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
