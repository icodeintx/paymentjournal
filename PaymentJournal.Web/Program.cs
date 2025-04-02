using MudBlazor.Services;
using PaymentJournal.Web;
using PaymentJournal.Web.Repositories;

var builder = WebApplication.CreateBuilder(args);

//setup logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

string connectionString = "";


#if DEBUG
    connectionString = ".\\Data\\paymentjournallitedb.db";
#else 
    if (builder.Configuration["IsDocker"]?.ToLower() == "true")
    {
       connectionString = builder.Configuration.GetConnectionString("LiteDb") ?? "";
    }
    else
    {
        connectionString = Environment.GetEnvironmentVariable("PaymentJournalDbPath") + "\\liteDb.db";
    }
#endif


builder.Services.AddTransient<PaymentRepo>(s => new PaymentRepo(connectionString));
builder.Services.AddTransient<BudgetRepo>(s => new BudgetRepo(connectionString));
builder.Services.AddTransient<CacheRepo>(s => new CacheRepo(connectionString));

builder.Services.AddScoped<IHighlightJS, HighlightJS>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        Console.WriteLine($"Serving static file: {ctx.File.Name}");
    }
});

app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();