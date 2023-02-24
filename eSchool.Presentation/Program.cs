using eSchool.Application.Services.Implementations;
using eSchool.Application.Services.Interfaces;
using eSchool.Infrastructure;
using eSchool.Infrastructure.UnitOfWork.Implementation;
using eSchool.Infrastructure.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Onion.Application.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(connectionString));

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IGradeService, GradeService>();
    builder.Services.AddScoped<ISubjectService, SubjectService>();
}

var app = builder.Build();

{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}