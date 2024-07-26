using System.Globalization;
using Infrastructure.ServiceExt;
using WebAPI;

// Выставляем культуру по умолчанию
var cultureInfo = new CultureInfo("kk-KZ");
CultureInfo.CurrentCulture = cultureInfo;
CultureInfo.CurrentUICulture = cultureInfo;

// Сборщик
var builder = WebApplication.CreateBuilder(args);

// Регистрируем сервисы сайта
builder.Services.RegisterSiteServices(builder.Configuration);

// Настраиваем CORS
/*builder.Services
    .AddCors(options =>
        options.AddPolicy("siteCors",
            x => x.AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins(
                    "https://localhost:4200",
                    "http://localhost:4200",
                    "http://10.10.21.32:8080"
                )));*/

// Добавление контроллеров и JSON сериализации
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");
app.Run();