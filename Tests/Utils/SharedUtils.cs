using Api.Models.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Tests.Utils;

public class SharedUtils
{
    public static T GetPropertyValue<T>(object src, string label)
    {
        bool propertyExists = CheckPropertyExists(src, label);

        if (!propertyExists)
        {
            throw new KeyNotFoundException($"Não foi possível encontrar a chave {label} no objeto.");
        }

        T property = (T)src.GetType().GetProperty(label)!.GetValue(src)!;

        return property;
    }

    public static bool CheckPropertyExists(object src, string label)
    {
        bool propertyExists = src.GetType().GetProperty(label) != null;

        return propertyExists;
    }

    public static AlertVariant GetAlertVariantFromString(string variant)
    {
        switch (variant)
        {
            case "success":
                return AlertVariant.Success;
            case "info":
                return AlertVariant.Info;
            case "warning":
                return AlertVariant.Warning;
            case "error":
                return AlertVariant.Error;
            case "default":
                return AlertVariant.Default;
            default:
                return AlertVariant.Default;
        }
    }

    public static object ActionResultToObject(ActionResult actionResult)
    {
        if (actionResult is not OkObjectResult)
        {
            throw new InvalidCastException("Esperasse que a actionResult seja uma instância de OkObjectResult.");
        }

        OkObjectResult okObjectResult = (OkObjectResult)actionResult;

        if (okObjectResult.Value == null)
        {
            throw new NullReferenceException("Esperasse que a propriedade Value de okObjectResult não seja nula.");
        }

        object actionResultAsObject = okObjectResult.Value;

        return actionResultAsObject;
    }
}