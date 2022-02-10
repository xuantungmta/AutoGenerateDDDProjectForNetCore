using System.Data;

namespace [$project_name].domain.responsitory
{
    public interface IDbFactory
    {
        IDbConnection Init();
    }
}