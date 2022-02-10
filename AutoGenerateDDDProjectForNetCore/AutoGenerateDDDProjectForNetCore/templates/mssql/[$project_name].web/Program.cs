using blog.application;
using blog.application.BaiViet;
using blog.application.DanhMuc;
using blog.domain.entity.BaiViet;
using blog.domain.entity.Tag;
using blog.domain.responsitory;
using blog.domain.responsitory.instances;
using blog.infrastructure.persistence;
using blog.infrastructure.persistence.instances;

var builder = WebApplication.CreateBuilder(args);

//Config for IoCs
builder.Services.AddAutoMapper(typeof(ConfigAutoMapper));

builder.Services.AddScoped<IDbFactory, DbFactory>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IDanhMucRepository, DanhMucRepository>();
builder.Services.AddScoped<IBaiVietRepository, BaiVietRepository>();
builder.Services.AddScoped<IRepository<BAIVIET_DANHMUC>, RepositoryBase<BAIVIET_DANHMUC>>();
builder.Services.AddScoped<IRepository<BAIVIET_TAG>, RepositoryBase<BAIVIET_TAG>>();
builder.Services.AddScoped<IRepository<TAG>, RepositoryBase<TAG>>();

builder.Services.AddScoped<IBaiVietService, BaiVietService>();
builder.Services.AddScoped<IDanhMucService, DanhMucService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();