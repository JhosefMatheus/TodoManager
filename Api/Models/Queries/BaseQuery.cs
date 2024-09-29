using System.Text.Json;
using Api.Models.Exceptions.HttpExceptions;
using Api.Models.Shared;

namespace Api.Models.Queries;

public abstract class BaseQuery
{
    public static T FromJson<T>(string base64Json) where T : BaseQuery
    {
        byte[] data = Convert.FromBase64String(base64Json);
        string json = System.Text.Encoding.UTF8.GetString(data);

        T query = JsonSerializer.Deserialize<T>(json)
        ?? throw new BadHttpException(
            "Erro inesperado no sistema ao converter o json.",
            AlertVariant.Error
        );

        return query;
    }

    public string ToBase64()
    {
        using MemoryStream ms = new MemoryStream();

        JsonSerializer.Serialize(ms, this);

        return Convert.ToBase64String(ms.ToArray());
    }
}