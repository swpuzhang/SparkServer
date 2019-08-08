﻿using Sangong.GameMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sangong.Domain.Logic
{
    public static class GameAccounter
    {
        public static GameOverEvent Caculate(List<GameSeat> seates, List<KeyValuePair<long, List<int>>> poolsSeats)
        {
            List<KeyValuePair<int, CardCombination>> seatRand = new List<KeyValuePair<int, CardCombination>>();
            foreach(var seat in seates)
            {
                seatRand.Add(new KeyValuePair<int, CardCombination>(seat.SeatNum, seat.Combination));
            }
            seatRand.Sort((x, y) =>
            {
                if (x.Value == null)
                {
                    return -1;
                }
                return x.Value.CompareTo(y.Value);
             });
            seates.Reverse();
            List<WinnerCoinsPool> poolWinners = new List<WinnerCoinsPool>();
            foreach (var onePool in poolsSeats)
            {
                int winseat = GetPoolWinner(seatRand, onePool.Value);
                poolWinners.Add(new WinnerCoinsPool(winseat, onePool.Key));
            }
            List<int> winners = GetWinners(seates, poolWinners);
            var playerCards = GetHandCards(seates);
            GameOverEvent gameOver = new GameOverEvent(winners, poolWinners, playerCards);
            return gameOver;
        }

        public static int GetPoolWinner(List<KeyValuePair<int, CardCombination>> seates, List<int> joinedSeats)
        {
            foreach(var oneSeat in seates)
            {
                if (joinedSeats.Contains(oneSeat.Key))
                {
                    return oneSeat.Key;
                }
            }
            return joinedSeats.First();
        }

        public static List<int> GetWinners(List<GameSeat> seates, List<WinnerCoinsPool> pollWinners)
        {
            List<int> winners = new List<int>();
            foreach (var oneSeat in seates)
            {
                long totalWin = pollWinners.Where(x => x.WinnerSeat == oneSeat.SeatNum).Sum(x => x.Coins);
                long profit = totalWin - oneSeat.TotalBetedCoins;
                oneSeat.Win(profit);
                if (profit > 0)
                {
                    winners.Add(oneSeat.SeatNum);
                }

            }
            return winners;
        }

        public static Dictionary<int, PlayerCards> GetHandCards(List<GameSeat> seates)
        {
            Dictionary<int, PlayerCards> allPlayerCards = new Dictionary<int, PlayerCards>();
            foreach (var seat in seates)
            {
                if (seat.IsCanContinue())
                {
                    var playerCards = new PlayerCards(seat.handCards, (int)seat.Combination.ComType, seat.Combination.Point);
                    allPlayerCards.Add(seat.SeatNum, playerCards);
                }
            }
            return allPlayerCards;
        }
    }
}
