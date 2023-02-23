using eSchool.Infrastructure;
using eSchool.Infrastructure.UnitOfWork.Implementation;
using eSchool.Infrastructure.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(connectionString));

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
}

var app = builder.Build();

{
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}