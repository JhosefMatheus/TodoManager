using Api.Models.Interfaces.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public abstract class BaseService
{
    public T? FindById<T>(DbSet<T> values, int id) where T : class, IBaseIdentifierEntity
    {
        T? item = values.AsEnumerable<T>().Where<T>((T item) =>
        {
            bool validId = item.Id == id;

            return validId;
        })
        .FirstOrDefault<T>();

        return item;
    }
}