using RevitApiMastery.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<RevitApiMastery.Services.CurriculumService>();
builder.Services.AddSingleton<RevitApiMastery.Services.SimulatorService>();
builder.Services.AddSingleton<RevitApiMastery.Services.SupportService>();
builder.Services.AddSingleton<RevitApiMastery.Services.EmailService>();
builder.Services.AddScoped<RevitApiMastery.Services.ProgressService>();
builder.Services.AddScoped<RevitApiMastery.Services.AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
