using System.Linq.Expressions;

namespace [$project_name].utils.specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> SpecExpression { get; }

        bool IsSatisfiedBy(T obj);
    }
}