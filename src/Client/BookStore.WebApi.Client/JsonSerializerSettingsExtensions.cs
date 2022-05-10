using Newtonsoft.Json;

namespace BookStore.WebApi.Client;

public static class JsonSerializerSettingsExtensions
{
    public static JsonSerializerSettings ApplyCommonNewtonsoftJsonSettings(
        this JsonSerializerSettings settings)
    {
        settings.TypeNameHandling = (TypeNameHandling)1;
        settings.PreserveReferencesHandling = (PreserveReferencesHandling)1;
        settings.ReferenceLoopHandling = (ReferenceLoopHandling)2;
        return settings;
    }
}
