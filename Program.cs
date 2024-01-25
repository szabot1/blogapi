using BlogApi.Data;
using BlogApi.Email;
using BlogApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService, MailService>();

builder.Services.AddDbContext<BlogDbContext>(options =>
{
    options.UseMySql(
        "server=localhost;database=fz_blog;user=fz_blog;password=asd123",
        MySqlServerVersion.LatestSupportedServerVersion
    );
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope())
{
    using var ctx = serviceScope.ServiceProvider.GetService<BlogDbContext>();
    ctx!.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
