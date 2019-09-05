using Commons.Domain.Models;
using Commons.Domain.Models.Dummy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dummy.MqCommands
{
    public class GameLogMqCommand
    {
        public GameLogMqCommand(DummyGameLog gameLog)
        {
            GameLog = gameLog;
        }

        public DummyGameLog GameLog { get; set; }

        public Dictionary<long, long> GetPlayers()
        {
            var gameOverAct =GameLog.GameActions.Where(x => x.ActionName == DummyGameLog.gameOver).First() as GameOverAct;
            Dictionary<long, long> allplayers = new Dictionary<long, long>();
            foreach (var one in gameOverAct.Players)
            { 
                allplayers.Add(one.Id, one.CoinsInc);
            }
            return allplayers;
        }
    }
}
