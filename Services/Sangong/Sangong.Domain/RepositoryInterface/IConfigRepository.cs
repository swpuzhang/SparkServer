﻿using Sangong.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sangong.Domain.RepositoryInterface
{
    public interface IConfigRepository
    {
        List<CoinsRangeConfig> LoadCoinsRangeConfig();
    }
}