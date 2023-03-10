using eSchool.Application.Repositories.Implementations;
using eSchool.Application.Repositories.Interfaces;
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

    builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();

    builder.Services.AddScoped<IGradeRepository, GradeRepository>();

    builder.Services.AddScoped<IGradeSubjectRepository, GradeSubjectRepository>();

    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    builder.Services.AddScoped<IGradeService, GradeService>();

    builder.Services.AddScoped<ISubjectService, SubjectService>();

    builder.Services.AddCors();
}

var app = builder.Build();

{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}