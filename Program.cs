using DSALabsApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register all services used by the lab endpoints
builder.Services.AddSingleton<ArrayOperations>(_ => new ArrayOperations(15));
builder.Services.AddSingleton<FactorialService>();
builder.Services.AddSingleton<MultiplicationService>();
builder.Services.AddSingleton<TimingService>();
builder.Services.AddSingleton<BigOAnalysisService>();
builder.Services.AddSingleton<StudentManager>();
builder.Services.AddSingleton<SinglyLinkedList>();
builder.Services.AddSingleton<DoublyLinkedList>();
builder.Services.AddSingleton<CircularLinkedList>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();