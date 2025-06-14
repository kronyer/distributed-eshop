using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ProductDbContext>(connectionName: "catalogdb");

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductAIService>();
builder.Services.AddMassTransitWithAssemblies(Assembly.GetExecutingAssembly());

//builder.AddOllamaSharpChatClient("ollama-llama3-2");//pm depreacted
builder.AddOllamaApiClient("ollama-llama3-2").AddChatClient();

//builder.AddOllamaSharpEmbeddingGenerator("ollama-all-minilm"); deprecated
builder.AddOllamaApiClient("ollama-all-minilm").AddEmbeddingGenerator();

builder.Services.AddInMemoryVectorStoreRecordCollection<int, ProductVector>("products");


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigration();

}

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapProductEndpoints();

app.UseHttpsRedirection();

app.Run();

