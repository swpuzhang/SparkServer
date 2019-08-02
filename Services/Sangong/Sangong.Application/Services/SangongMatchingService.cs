using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sangong.Application.ViewModels;
using Sangong.Domain;
using Sangong.Domain.Commands;
using Sangong.Domain.Models;
using Sangong.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Infrastruct;

namespace Sangong.Application.Services
{
    public class SangongMatchingService : ISangongMatchingService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        public SangongMatchingService(ISangongInfoRepository repository, IMapper mapper, IMediatorHandler bus)
        {
            _mapper = mapper;
            _bus = bus;
        }
        
        public async Task<BodyResponse<SangongMatchingResponseVM>> Playnow(long id)
        {
            BodyResponse<SangongMatchingResponseInfo> response = await _bus.SendCommand(new SangongPlaynowCommand(id));
            return response.MapResponse<SangongMatchingResponseVM>(_mapper);
        }
    }
}
