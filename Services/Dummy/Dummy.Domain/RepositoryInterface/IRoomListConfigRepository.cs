using Dummy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Domain.RepositoryInterface
{
    public interface IRoomListConfigRepository
    {
        List<RoomListConfig> LoadConfig();
    }
}
