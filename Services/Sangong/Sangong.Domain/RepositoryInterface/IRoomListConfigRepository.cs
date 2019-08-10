using Sangong.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sangong.Domain.RepositoryInterface
{
    public interface IRoomListConfigRepository
    {
        List<RoomListConfig> LoadConfig();
    }
}
