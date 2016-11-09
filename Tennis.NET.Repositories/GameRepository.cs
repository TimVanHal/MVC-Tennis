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
    public class GameRepository
    {
        public List<Game> getGames()
        {
            List<Game> games = new List<Game>();
            using (var context = new TennisContext())
            {
                games = context.Games.Include(g => g.Player1).Include(g => g.Player2).ToList();
            }
            return games;
        }

        public Game getGameById(int? id)
        {
            Game game;
            using (var context = new TennisContext())
            {
                game = context.Games.Include(g => g.Player1).Include(g => g.Player2).SingleOrDefault(g => g.Id == id);
            }
            return game;
        }

        public void createGame(Game game)
        {
            using (var context = new TennisContext())
            {
                context.Games.Add(game);
                context.SaveChanges();
            }
        }

        public void modifyGame(Game game)
        {
            using (var context = new TennisContext())
            {
                context.Entry(game).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void deleteGame(int id)
        {
            Game game = getGameById(id);
            using (var context = new TennisContext())
            {
                context.Entry(game).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
