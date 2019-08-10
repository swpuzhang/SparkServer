var url = "mongodb://localhost:27017/SkyWatch";
var db = connect(url);
db.UserIdGenInfo.save({"_id":10000000000,"UserId":10000000000})
db.createCollection("AccountInfo");
db.AccountInfo.ensureIndex({PlatformAccount:1}, {unique: true});
db.createCollection("LevelConfig");
db.LevelConfig.ensureIndex({Level:1}, {unique: true});
db.LevelConfig.insert( {Level:1, NeedExp:10});
db.LevelConfig.insert( {Level:2, NeedExp:10});
db.CoinsRangeConfig.insert({CoinsBegin:5000,CoinsEnd:100000,Blind:500})
db.CoinsRangeConfig.insert({CoinsBegin:100000,CoinsEnd:9223372036854775807,Blind:2000})
db.RoomListConfig.insert({Blind:500, MinCoins:5000, MaxCoins:5000, TipsPersent:10,MinCarry:5000, MaxCarry:50000})
db.RoomListConfig.insert({Blind:2000, MinCoins:20000, MaxCoins:20000, TipsPersent:20,MinCarry:20000, MaxCarry:200000})