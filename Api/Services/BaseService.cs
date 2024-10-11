using System.Linq.Expressions;
using Api.Models.Interfaces.Database;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public abstract class BaseService
{
    public T? FindById<T>(DbSet<T> values, int id) where T : class, IBaseIdentifierEntity
    {
        T? item = values.AsEnumerable<T>().Where<T>((T currentItem) =>
        {
            bool validId = currentItem.Id == id;

            return validId;
        })
        .FirstOrDefault<T>();

        return item;
    }

    public T? FindByColumn<T, U>(DbSet<T> values, Expression<Func<T, U>> column, U value) where T : class
    {
        T? item = values.AsEnumerable<T>().Where<T>((T currentItem) =>
        {
            bool validColumn = column.Compile().Invoke(currentItem)?.Equals(value) ?? false;

            return validColumn;
        })
        .FirstOrDefault<T>();

        return item;
    }
}