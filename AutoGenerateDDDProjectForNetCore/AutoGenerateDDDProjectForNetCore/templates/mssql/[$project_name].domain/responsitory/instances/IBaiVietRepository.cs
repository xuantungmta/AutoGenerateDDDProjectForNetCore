using blog.domain.entity.BaiViet;

namespace blog.domain.responsitory.instances
{
    public interface IBaiVietRepository : IRepository<BAIVIET>
    {
        Task<IEnumerable<BAIVIET>> GetAllByCategoryAsyn(long categoryId);
        IEnumerable<BAIVIET> GetAllByCategory(long categoryId);
        Task<IEnumerable<BAIVIET>> GetByCategoryPagingAsyn(long categoryId, string keyword, int page, int pageSize);
        Task<long> CountByCategoryAsync(long categoryId, string keyword);
    }
}