using InteractiveTyingGameBlazor.Chat;
using InteractiveTyingGameBlazor.Components;
using InteractiveTyingGameBlazor.Components.Account;
using InteractiveTyingGameBlazor.Data;
using InteractiveTyingGameBlazor.Data.Services;
using InteractiveTyingGameBlazor.GameManagement;
using InteractiveTyingGameBlazor.Hubs;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<TypingResultService>();
builder.Services.AddScoped<RegisteredVideosService>();
builder.Services.AddScoped<MessageService>();
builder.Services.AddSingleton<MatchmakingService>();
builder.Services.AddSingleton<PublicChatService>();
builder.Services.AddSingleton<CookieStorage>();
builder.Services.AddSingleton<ConnectionService>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

//var connectionString = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING") ?? throw new InvalidOperationException("Connection string 'AZURE_SQL_CONNECTIONSTRING' not found.");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));


//var connectionString = builder.Configuration.GetConnectionString("SQLITE_CONNECTION")
//    ?? throw new InvalidOperationException("Connection string 'SQLITE_CONNECTION' not found.");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlite("Data Source=C:\\home\\site\\wwwroot\\database.db"));

//var environment = webBuilder.Environment;
//var contentRootPath = environment.ContentRootPath;

//var databasePath = Path.Combine(contentRootPath, "Database", "database.db");
//var connectionString = $"Data Source={databasePath}";

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlite(connectionString));


var connectionString = builder.Configuration.GetConnectionString("SQLITE_CONNECTION")
    ?? throw new InvalidOperationException("Connection string 'SQLITE_CONNECTION' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=" + builder.Environment.ContentRootPath + "\\database.db"));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSignalR();
 
builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
          new[] { "application/octet-stream" });
});

//syncfusion licensing
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(builder.Configuration["SYNCFUSION_LICENSE_KEY"]);
var app = builder.Build();
var logger = ((IApplicationBuilder)app).ApplicationServices.GetService<ILogger<Program>>();
logger.LogError(builder.Environment.ContentRootPath);

try
{

	using (var scope = app.Services.CreateScope())
	{
		var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
		dbContext.Database.Migrate();
	}

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseResponseCompression();

    app.UseHttpsRedirection();

    app.UseStaticFiles();
    app.UseAntiforgery();

    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();
    app.MapAdditionalIdentityEndpoints();
    app.MapHub<ChatHub>("/chathub");
	using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var roles = new[] { "Admin","User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                IdentityRole roleRole = new(role);
                await roleManager.CreateAsync(roleRole);
            }
        }
    }
	using (var scope = app.Services.CreateScope())
	{
		var serviceProvider = scope.ServiceProvider;
		var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
		var adminRoleName = "Admin";
		var adminEmail = "admin@admin";
		var adminUser = await userManager.FindByEmailAsync(adminEmail);
		if (adminUser == null)
		{
			adminUser = new ApplicationUser
			{
				UserName = adminEmail,
				Email = adminEmail,
				EmailConfirmed = true, 
			};
			var adminPassword = "Heslo1_";
			var createUserResult = await userManager.CreateAsync(adminUser, adminPassword);
			if (createUserResult.Succeeded)
			{
				await userManager.AddToRoleAsync(adminUser, adminRoleName);
			}
		}
	}

	app.Run();
}
catch (Exception ex)
{
    logger.LogError(ex, "An unhandled exception has ocured");
    throw;
}