using AutoMapper;
using blog.application.BaiViet;
using blog.application.DanhMuc;
using blog.domain.entity.BaiViet;
using blog.domain.entity.DanhMuc;

namespace blog.application
{
    public class ConfigAutoMapper : Profile
    {
        public ConfigAutoMapper()
        {
            CreateMap<BAIVIET, BaiVietDTO>();
            CreateMap<DANHMUC, DanhMucDTO>();
        }
    }
}