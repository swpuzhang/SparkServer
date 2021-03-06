﻿using Commons.Domain.Models;
using Commons.Domain.Models.Sangong;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sangong.MqCommands
{
    public class GameLogMqCommand
    {
        public GameLogMqCommand(SangongGameLog gameLog)
        {
            GameLog = gameLog;
        }

        public SangongGameLog GameLog { get; set; }

        public Dictionary<long, long> GetPlayers()
        {
            var gameOverAct =GameLog.GameActions.Where(x => x.ActionName == SangongGameLog.gameOver).First() as GameOverAct;
            Dictionary<long, long> allplayers = new Dictionary<long, long>();
            foreach (var one in gameOverAct.Players)
            {
                allplayers.Add(one.Id, one.CoinsInc);
            }
            return allplayers;
        }
    }
}
