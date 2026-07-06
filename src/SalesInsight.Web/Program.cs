using SalesInsight.Application;
using SalesInsight.Infrastructure;
using SalesInsight.Web.ApiClients;
using SalesInsight.Web.Components;
using SalesInsight.Web.ExceptionHandling;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>("database");
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddHttpClient<DashboardApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? throw new InvalidOperationException("API base URL is not configured."));
});
builder.Services.AddHttpClient<CustomerApiClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ApiBaseUrl"]
        ?? throw new InvalidOperationException("API base URL is not configured."));
});

var app = builder.Build();
app.MapHealthChecks("/health");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();
