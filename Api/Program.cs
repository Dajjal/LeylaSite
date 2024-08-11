using System.Globalization;
using System.Text.Json.Serialization;
using Infrastructure.ServiceExt;

var builder = WebApplication.CreateBuilder(args);

// Устанавливаем культуру по умолчанию
SetDefaultCulture("kk-KZ");

// Регистрируем сервисы сайта
builder.Services.RegisterLeylaSiteServices(builder.Configuration);

// Регистрируем контроллеры с конфигурацией JSON
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        // Игнорировать циклические ссылки при сериализации
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Настройка Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Настройка файлов по умолчанию и статики
app.UseDefaultFiles();
app.UseStaticFiles();

// Конфигурация конвейера обработки HTTP запросов
if (app.Environment.IsDevelopment())
{
    // Включаем Swagger в режиме разработки
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");

// Запуск приложения
app.Run();
return;

// Метод для установки культуры по умолчанию
static void SetDefaultCulture(string culture)
{
    var cultureInfo = new CultureInfo(culture);
    CultureInfo.CurrentCulture = cultureInfo;
    CultureInfo.CurrentUICulture = cultureInfo;
}