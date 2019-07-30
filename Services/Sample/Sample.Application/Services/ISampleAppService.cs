using Sample.Application.ViewModels;
using Sample.Domain.Models;
using Commons.Domain.Models;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Application.Services
{
    public interface ISampleAppService
    {
        Task<HasBodyResponse<SampleResponse>> Login(SampleVM sampleVM);
        SampleVM GetById(Int64 id);
      

    }
}
