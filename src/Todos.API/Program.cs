using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Todos.Application.Interfaces;
using Todos.Application.Repositories;
using Todos.Application.TodoItems.Queries;
using Todos.Orchestrator.TodoItems.Commands;
using Todos.Orchestrator.TodoItems.Models;
using Todos.Orchestrator.TodoItems.Queries;
using Todos.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ITodoDbContext, TodoDbContext>(o => o.UseInMemoryDatabase("TodoDbContext"));
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddMediatR(typeof(GetTodoItemsQuery).Assembly, typeof(GetTodoItemsQueryHandler).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCors",
        policy =>
        {
            policy.WithOrigins("https://localhost:44398")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, "x-custom-header")
                  .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("DefaultCors");

app.MapGet("/todoItems", async (IMediator mediator) =>
{
    return await mediator.Send(new GetTodoItemsQuery())
        is List<TodoItemViewModel> todoItems
        ? Results.Ok(todoItems)
        : Results.NoContent();
});

app.MapGet("/todoItem", async (IMediator mediator, int id) =>
{
    return await mediator.Send(new GetTodoItemQuery { Id = id })
        is TodoItemViewModel todoItem
        ? Results.Ok(todoItem)
        : Results.NoContent();
});

app.MapPost("/todoItem", async (IMediator mediator, CreateTodoItemCommand command) =>
{
    return await mediator.Send(command)
        is int id
        ? Results.Ok(id)
        : Results.BadRequest();
});

app.MapPost("/todoItem/status", async (IMediator mediator, UpdateTodoItemStatusCommand command) =>
{
    return await mediator.Send(command)
        is bool result
        ? Results.Ok(result)
        : Results.BadRequest();
});

app.Run();

public partial class Program { }