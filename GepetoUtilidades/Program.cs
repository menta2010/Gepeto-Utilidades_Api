using GepetoUtilidades.Service.Config;
using GepetoUtilidades.Service.Interfaces;
using GepetoUtilidades.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region injeção dependencia

builder.Services.AddScoped<IConsultaGptService, ConsultaGptService>();

builder.Services.AddHttpClient();

builder.Services.Configure<OpenAiApiSettings>(builder.Configuration.GetSection("OpenAI"));


#endregion
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
