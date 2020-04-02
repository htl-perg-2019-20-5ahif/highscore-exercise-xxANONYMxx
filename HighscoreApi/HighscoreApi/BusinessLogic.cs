using HighscoreApi.Data;
using HighscoreLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HighscoreApi
{
    public class BusinessLogic
    {
        public Player AddPlayerLogic(List<Player> highscore, Player player)
        {
            if(player == null|| highscore == null)
            {
                return null;
            }


            Player old;
            var count = highscore.Count;
            if (count < 5)
            {
                //highscore.Add(player);
                Player p = new Player();
                p.PName = "♠☼┌";
                return p;
            }
            else
            {
                // Highscore Liste ist sortiert
                if (highscore.Last().Score < player.Score)
                {
                    return highscore.Last();
                    // Zack bumm brack
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
