using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sangong.Domain.Logic
{
    public class CoinsPool
    {
        class BetInfo : IComparable<BetInfo>
        {

            public int SeatNum { get; private set; }
            public long BetCoins { get; private set; }

            public BetInfo(int seatNum, long betChips)
            {
                this.SeatNum = seatNum;
                this.BetCoins = betChips;
            }

            public int CompareTo(BetInfo other)
            {
                int ret = BetCoins.CompareTo(other.BetCoins);
                if (ret == 0)
                {
                    return SeatNum.CompareTo(other.SeatNum);
                }
                return ret;
            }
        }

        public void AddBet(int seatNum, long coins)
        {
            if (!_allBetRecords.TryAdd(seatNum, coins))
            {
                _allBetRecords[seatNum] += coins;
            }
           
        }

        private List<KeyValuePair<long, List<int>>> CaculatePools()
        {
            List<KeyValuePair<long, List<int>>> pools = new List<KeyValuePair<long, List<int>>>();
            SortedSet<BetInfo> sortedBet = new SortedSet<BetInfo>();
            foreach (var record in _allBetRecords)
            {
                sortedBet.Add(new BetInfo(record.Key, record.Value));
            }
            foreach (var one in sortedBet)
            {
                long leftCoins = one.BetCoins;
                foreach(var onePool in pools)
                {
                    leftCoins -= onePool.Key;
                    onePool.Value.Add(one.SeatNum);
                }
                if (leftCoins > 0)
                {
                    pools.Add(new KeyValuePair<long, List<int>>(leftCoins, new List<int>() { one.SeatNum }));
                }
            }

            List<KeyValuePair<long, List<int>>> totalPools = new List<KeyValuePair<long, List<int>>>(); 
            foreach (var onePool in pools)
            {
                long total = onePool.Key * onePool.Value.Count;
                List<int> seats = onePool.Value;
                totalPools.Add(new KeyValuePair<long, List<int>>(total, seats));

            }
            return totalPools;
        }

        public void CaculateFirstPools()
        {
            _firstPools = CaculatePools();
        }

        public void CaculateSecondPools()
        {
            _secondPools = CaculatePools();
        }

        public List<long> GetFirstPoolsResult()
        {
           
            if (_firstPools == null)
            {
                CaculateFirstPools();
            }
            
            return _firstPools.Select(x => x.Key).ToList();
        }

      
        public List<KeyValuePair<long, List<int>>>  GetSecondPools()
        {
            if (_secondPools == null)
            {
                CaculateSecondPools();
            }
            return _secondPools;
        }

        public void Clean()
        {
            _allBetRecords.Clear();
            _firstPools = null;
            _secondPools = null;
        }

        private Dictionary<int, long> _allBetRecords = new Dictionary<int, long>();
        private List<KeyValuePair<long, List<int>>> _firstPools = null;
        private List<KeyValuePair<long, List<int>>> _secondPools = null;

    }
}
