using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using PaymentJournal.Web.Repositories;

var builder = WebApplication.CreateBuilder(args);

//pull connection string from config file
string connectionString = builder.Configuration.GetConnectionString("LiteDb");

builder.Services.AddTransient<PaymentRepo>(s => new PaymentRepo(connectionString));
builder.Services.AddTransient<BudgetRepo>(s => new BudgetRepo(connectionString));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();