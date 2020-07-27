using RuletaOnline.Configuration.AppSettings;

namespace RuletaOnline.Configuration
{
    public interface IServerConfiguration
    {
        MongoDBConfig GetMongoDBConfig();
    }
}