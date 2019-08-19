using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Domain.RepositoryInterface
{
    public interface IConfigRepository<T>
    {
        T LoadConfig();
    }
    public interface  IConfigListRepository<T>
    {
        List<T> LoadConfig();
    }

}
