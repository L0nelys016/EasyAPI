using EasyAPI.Models;

namespace EasyAPI.Repositories;

public class RepositoryBase
{
    protected readonly PostgresContext Сontext;

    protected RepositoryBase(PostgresContext postgresContext) => Сontext = postgresContext;
}
