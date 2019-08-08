﻿using Commons.Extenssions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Sangong.Domain.Logic
{
    public enum GameStatus
    {
        Idle = 0,
        ready,
        playing,
        FirstRound,
        SecondRound,
        GameOver,
    }

    public class GameStatusLogic
    {
        public GameStatusLogic()
        {
            _timer.Elapsed += TimeOut;
            _timer.AutoReset = false;
        }

        public GameStatus _status { get; private set; } = GameStatus.Idle;
        private System.Timers.Timer _timer = new System.Timers.Timer();
        private Action _a;
        public void  WaitForNexStatus(Action a, GameStatus nexStatus, double ms)
        {
            _timer.Stop();
            if (ms == 0)
            {
                a();
                return;
            }
            _timer.Interval = ms;
            _timer.Start();
            //_timer = new Timer(TimeOut, null, ts, TimeSpan.MinValue .FromSeconds(-1));
            _status = nexStatus;
            _a = a;
           
        }

        public bool IsGameCanStart()
        {
            return _status == GameStatus.Idle || _status == GameStatus.ready;
        }

        public bool IsFirstRound()
        {
            return _status == GameStatus.FirstRound;
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        private void TimeOut(object o, ElapsedEventArgs e)
        {
            OneThreadSynchronizationContext.Instance.Post(x => _a() , null);
        }

    }
}
