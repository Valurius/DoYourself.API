using DoYourself.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext(builder.Configuration)
    .AddRegisterSecurity()
    .AddControllers().Services
    .AddControllersWithViews().Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection()
.UseAuthentication()
.UseAuthorization()
.UseFileServer()
.UseDefaultFiles()
.UseStaticFiles();

app.MapControllers();
app.UseCors(options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.Run();
