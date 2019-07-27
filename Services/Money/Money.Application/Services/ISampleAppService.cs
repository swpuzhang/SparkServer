using Commons.Domain.Models;
using Commons.Infrastruct;
using Sample.Application.ViewModels;
using Sample.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Application.Services
{
    public interface ISampleAppService
    {

        SampleVM GetById(Int64 id);

        void Update(SampleVM moneyViewModel);

        Task<HasBodyResponse<SampleResponse>> Login(SampleVM moneyViewModel);

    }
}
