using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Reward.Application.ViewModels;
using Reward.Domain;
using Reward.Domain.Commands;
using Reward.Domain.Models;
using Reward.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Infrastruct;
using Reward.Domain.Events;

namespace Reward.Application.Services
{
    public class RewardService : IRewardService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        public RewardService(IMapper mapper, IMediatorHandler bus)
        {
            _mapper = mapper;
            _bus = bus;
        }


        public async Task<BodyResponse<RegisterRewardVM>> QueryRegisterReward(long id)
        {
            return await _bus.SendCommand(new QueryRegisterRewardCommand(id));
        }

        public async Task<BodyResponse<RewardInfoVM>> GetRegisterReward(long id)
        {
            return await _bus.SendCommand(new GetRegisterRewardCommand(id));
        }

        public async Task<BodyResponse<LoginRewardVM>> QueryLoginReward(long id)
        {
            return await _bus.SendCommand(new QueryLoginRewardCommand(id));
        }

        public async Task<BodyResponse<RewardInfoVM>> GetLoginReward(long id)
        {
            return await _bus.SendCommand(new GetLoginRewardCommand(id));
        }

        public async Task<BodyResponse<BankruptcyInfoVM>> QueryBankruptcy(long id)
        {
            return await _bus.SendCommand(new QueryBankruptcyCommand(id));
        }

        public async Task<BodyResponse<RewardInfoVM>> GetBankruptcy(long id)
        {
            return await _bus.SendCommand(new GetBankruptcyRewardCommand(id));
        }

        public async Task InvitedFriendReward(long id, string platform, int type)
        {
            await _bus.RaiseEvent(new InvitedFriendEvent(id, platform, type));
        }

        public async Task InvitedFriendRegistered(string platform, int type)
        {
            await _bus.RaiseEvent(new InvitedFriendRegisterdEvent( platform, type));
        }
    }
}
