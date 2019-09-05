using Commons.Domain.Models;
using Commons.Domain.Models.GameSample;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSample.MqCommands
{
    public class GameLogMqCommand
    {
        public GameLogMqCommand(GameSampleGameLog gameLog)
        {
            GameLog = gameLog;
        }

        public GameSampleGameLog GameLog { get; set; }

        public Dictionary<long, long> GetPlayers()
        {
            var gameOverAct =GameLog.GameActions.Where(x => x.ActionName == GameSampleGameLog.gameOver).First() as GameOverAct;
            Dictionary<long, long> allplayers = new Dictionary<long, long>();
            foreach (var one in gameOverAct.Players)
            { 
                allplayers.Add(one.Id, one.CoinsInc);
            }
            return allplayers;
        }
    }
}
