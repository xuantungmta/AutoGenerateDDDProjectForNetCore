using blog.domain.entity.DanhMuc;

namespace blog.domain.responsitory.instances
{
    public interface IDanhMucRepository : IRepository<DANHMUC>
    {
        Task<IEnumerable<DANHMUC>> GetAllByTypeAsyn(long type);
    }
}