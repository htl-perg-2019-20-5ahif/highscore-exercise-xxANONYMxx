using HighscoreApi;
using HighscoreLogic;
using System;
using System.Collections.Generic;
using Xunit;

namespace HighscoreTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var logic = new BusinessLogic();

            Assert.Equal(null, logic.AddPlayerLogic(null, null));
        }

        [Fact]
        public void Test2()
        {
            var logic = new BusinessLogic();

            Assert.Equal(null, logic.AddPlayerLogic(null, new Player { PlayerId = 42, PName = "ANS", Score = 2345 }));
        }

        [Fact]
        public void Test3()
        {
            var logic = new BusinessLogic();

            Assert.Equal(null, logic.AddPlayerLogic(new List<Player>(), null));
        }

        [Fact]
        public void Test4()
        {
            var logic = new BusinessLogic();
            Player player = new Player { PlayerId = 42, PName = "ANS", Score = 2345 };
            Player result = new Player { PlayerId = 0, PName = "♠☼┌", Score = 000 };
            Assert.Equal(result.PName, logic.AddPlayerLogic(new List<Player>(), player).PName);
        }


        [Fact]
        public void Test5()
        {
            var logic = new BusinessLogic();
            List<Player> players = new List<Player>();
            
            Player player = new Player { PlayerId = 22, PName = "ANS", Score = 2345 };
            Player player1 = new Player { PlayerId = 52, PName = "ANS", Score = 6435 };
            Player player2 = new Player { PlayerId = 62, PName = "ANS", Score = 2555 };
            Player player3 = new Player { PlayerId = 15, PName = "ANS", Score = 123 };
            Player player4 = new Player { PlayerId = 12, PName = "ANS", Score = 5345 };

            players.Add(player);
            players.Add(player1);
            players.Add(player2);
            players.Add(player3);
            players.Add(player4);

            Player badPlayer = new Player { PlayerId = 05, PName = "BAD", Score = 5 };


            // Player result = new Player { PlayerId = 0, PName = "♠☼┌", Score = 000 };
            // players.Add(player);
            Assert.Equal(null, logic.AddPlayerLogic(players, badPlayer));
        }
    }
}
