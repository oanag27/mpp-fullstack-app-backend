using Microsoft.OpenApi.Models;
using System.Net.WebSockets;
using System.Net;
using System.Text;
using mmp_prj;
using Microsoft.EntityFrameworkCore;
using System;
using mmp_prj.Models;
using mmp_prj.Repository;
using mmp_prj.Service;
using mmp_prj.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add DbContext and Repository
builder.Services.AddDbContext<MppContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ISubtaskRepository, SubtaskRepository>();
builder.Services.AddScoped<ISubtaskService, SubtaskService>();


// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManagement");
    });
}
app.UseRouting();
app.UseCors();
app.UseHttpsRedirection();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapHub<MyHub>("/myhub"); // Adjust the endpoint and hub type as needed
//});
//app.UseWebSockets();
//app.Map("/ws", async c =>
//{
//    if (!c.WebSockets.IsWebSocketRequest)
//        c.Response.StatusCode = (int)HttpStatusCode.BadRequest;
//    else
//    {
//        using var webSocket = await c.WebSockets.AcceptWebSocketAsync();
//        while (true)
//        {
//            await webSocket.SendAsync(
//                Encoding.ASCII.GetBytes($".NET Rocks -> {DateTime.Now}"),
//                WebSocketMessageType.Text,
//                true, CancellationToken.None);
//            await Task.Delay(1000);
//        }
//    }
//});
//await app.RunAsync();
//app.UseWebSockets();

//app.UseMiddleware<TaskWebSocketMiddleware>();
//app.UseWebSockets();
//app.Use(async (context, next) =>
//{
//    if (context.Request.Path == "/ws")
//    {
//        if (context.WebSockets.IsWebSocketRequest)
//        {
//            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
//            await Echo(webSocket);
//        }
//        else
//        {
//            context.Response.StatusCode = StatusCodes.Status400BadRequest;
//        }
//    }
//    else
//    {
//        await next(context);
//    }

//});
//app.Run(async (context) =>
//{
//    using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
//    var socketFinishedTcs = new TaskCompletionSource<object>();

//    BackgroundSocketProcessor.AddSocket(webSocket, socketFinishedTcs);

//    await socketFinishedTcs.Task;
//});
//var webSocketOptions = new WebSocketOptions
//{
//    KeepAliveInterval = TimeSpan.FromMinutes(2)
//};

//webSocketOptions.AllowedOrigins.Add("https://localhost:7149");
////webSocketOptions.AllowedOrigins.Add("https://www.client.com");

//app.UseWebSockets(webSocketOptions);
//static async System.Threading.Tasks.Task Echo(WebSocket webSocket)
//{
//    var buffer = new byte[1024 * 4];
//    var receiveResult = await webSocket.ReceiveAsync(
//        new ArraySegment<byte>(buffer), CancellationToken.None);

//    while (!receiveResult.CloseStatus.HasValue)
//    {
//        await webSocket.SendAsync(
//            new ArraySegment<byte>(buffer, 0, receiveResult.Count),
//            receiveResult.MessageType,
//            receiveResult.EndOfMessage,
//            CancellationToken.None);

//        receiveResult = await webSocket.ReceiveAsync(
//            new ArraySegment<byte>(buffer), CancellationToken.None);
//    }

//    await webSocket.CloseAsync(
//        receiveResult.CloseStatus.Value,
//        receiveResult.CloseStatusDescription,
//        CancellationToken.None);
//}

app.UseAuthorization();

app.MapControllers();

app.Run();
