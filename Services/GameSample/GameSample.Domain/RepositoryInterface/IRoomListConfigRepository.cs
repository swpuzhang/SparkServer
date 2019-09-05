using GameSample.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameSample.Domain.RepositoryInterface
{
    public interface IRoomListConfigRepository
    {
        List<RoomListConfig> LoadConfig();
    }
}
