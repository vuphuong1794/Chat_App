using ChatApplication;
using ChatApplication.Hub;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IDictionary<string, UserRoomConnection>>(opt: IServiceProvider =>
new Dictionary<string, UserRoomConnection>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();


app.UseEndpoints(endpoint =>
{
    endpoint.MapHub<ChatHub>(pattern: "/chat");
})
app.MapRazorPages();


app.Run();
