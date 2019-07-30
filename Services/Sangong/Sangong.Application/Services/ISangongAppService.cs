using Sangong.Application.ViewModels;
using Sangong.Domain.Models;
using Commons.Domain.Models;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sangong.Application.Services
{
    public interface ISangongAppService
    {
        Task<HasBodyResponse<SangongResponse>> Login(SangongVM sangongVM);
        SangongVM GetById(Int64 id);
      

    }
}
