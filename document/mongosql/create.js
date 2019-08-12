var url = "mongodb://localhost:27017/SkyWatch";
var db = connect(url);
db.UserIdGenInfo.save({"_id":NumberLong(10000000000),"UserId":NumberLong(10000000000)})
db.createCollection("AccountInfo");
db.AccountInfo.ensureIndex({PlatformAccount:1}, {unique: true});
db.createCollection("LevelConfig");
db.LevelConfig.ensureIndex({Level:1}, {unique: true});
db.LevelConfig.insert( {Level:1, NeedExp:10});
db.LevelConfig.insert( {Level:2, NeedExp:10});
db.CoinsRangeConfig.insert({CoinsBegin:NumberLong(5000),CoinsEnd:NumberLong(100000),Blind:NumberLong(500)})
db.CoinsRangeConfig.insert({CoinsBegin:NumberLong(100000),CoinsEnd:NumberLong(-1),Blind:NumberLong(2000)})
db.RoomListConfig.insert({Blind:NumberLong(500), MinCoins:NumberLong(5000), MaxCoins:NumberLong(5000), TipsPersent:NumberLong(10),MinCarry:NumberLong(5000), MaxCarry:NumberLong(50000)})
db.RoomListConfig.insert({Blind:NumberLong(2000), MinCoins:NumberLong(20000), MaxCoins:NumberLong(20000), TipsPersent:NumberLong(20),MinCarry:NumberLong(20000), MaxCarry:NumberLong(200000)})