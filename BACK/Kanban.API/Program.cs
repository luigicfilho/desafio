using Kanban.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureKanbanApp();

var app = builder.Build();

app.RegisterKanbanApp();

app.Run();