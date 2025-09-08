var builder = WebApplication.CreateBuilder(args);
builder.Services.AddJWTTokenServices(builder.Configuration);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSeriLogConfig(builder);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddAuthorizationCustomRoles();
builder.Services.AddCustomSwaggerParameter();
builder.Services.AddHttpClientPolly();
builder.Services.AddApiVersioningConfig();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();  // needed for minimal & controllers
builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration.GetSection(Constant.ApplicationInsightsConnectionstring).Value;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
