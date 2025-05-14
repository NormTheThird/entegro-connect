var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();
builder.ConfigureLogging();
builder.ConfigureSwagger();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(_ => { _.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None); });
app.UseCors("CORSPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();