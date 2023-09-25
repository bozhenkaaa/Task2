using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Task2.Data;
using Task2.Repositories;
using Task2.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookStoreDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(serviceProvider => 
{
    var dbContext = serviceProvider.GetService<BookStoreDbContext>();
    var mapper = serviceProvider.GetService<IMapper>();
    return new UnitOfWork(dbContext, mapper);
});
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();