using TeksBebasAPI.Models;
using TeksBebasAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(classifier => TextClassifier.GetInstance);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseSwagger();
app.UseSwaggerUI();


app.MapPost("/sentiment", async (TextSchema textInput, TextClassifier classifier) => {
    var result = await Task.Run(() => classifier.GetSentiment(textInput.Text));

    return Results.Ok(result);
})
    .WithName("Measure Sentiment")
    .WithOpenApi();

app.MapPost("/emotion", async (TextSchema textInput, TextClassifier classifier) => {
    var result = await Task.Run(() => classifier.GetEmotion(textInput.Text));

    return Results.Ok(result);
})
    .WithName("Emotion Classification")
    .WithOpenApi();


app.Run();