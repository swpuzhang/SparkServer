using System.Threading.Tasks;
using ServerLog.Domain.Models;
using ServerLog.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.IntegrationBus.MqCommands.Sangong;
using Commons.Message.MqEvents;

namespace ServerLog.Application.Services
{
    public class ServerLogService : IServerLogService
    {
        private readonly IGameLogInfoRepository _repository;
        private readonly IMoneyLogInfoRepository _moneyRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        public ServerLogService(IGameLogInfoRepository repository, IMapper mapper, IMediatorHandler bus, 
            IMoneyLogInfoRepository moneyRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _bus = bus;
            _moneyRepository = moneyRepository;
        }

        public Task WriteGameLog(GameLogMqCommand command)
        {
            return _repository.InsertLog(new GameLogInfo(command.GameLog.GameId,
                command.GameLog.Blind, command.GameLog.RoomId,
                command.GameLog.RoomType, command.GameLog.GameTime,
                command.GameLog.GameActions));
        }

        public Task WriteMoneyLog(MoneyChangedMqEvent command)
        {
            return _moneyRepository.InsertLog(_mapper.Map<MoneyLogInfo>(command));
        }
    }
}
