using RuletaOnline.Configuration.AppSettings;

namespace RuletaOnline.Configuration
{
    public class ServerConfiguration : IServerConfiguration
    {
        private readonly MongoDBConfig mongoDBConfig;
        public ServerConfiguration(AppSettingsConfig appSetting)
        {
            this.mongoDBConfig = appSetting.MongoDB;
        }

        public MongoDBConfig GetMongoDBConfig()
        {
            return mongoDBConfig;
        }
    }
}