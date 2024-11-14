using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using TuberTreats.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

List<Customer> customers = new List<Customer>()
{
    //5 customers
    new Customer()
    {
        Id = 1,
        Name = "Tom",
        Address = "343 Guilty Dr"
    },
    new Customer()
    {
        Id = 2,
        Name = "Tim",
        Address = "101 Study Ave"
    },
    new Customer()
    {
        Id = 3,
        Name = "Tammy",
        Address = "321 Blastoff Ct"
    },
    new Customer()
    {
        Id = 4,
        Name = "Teresa",
        Address = "777 Lucky Dr"
    },
    new Customer()
    {
        Id = 5,
        Name = "Tevin",
        Address = "404 Lost Way"
    },
};

List<TuberDriver> tuberDrivers = new List<TuberDriver>()
{
    //3 drivers
    new TuberDriver()
    {
        Id = 1,
        Name = "Johnny Quickfast",
        TuberDeliveries = 5,
    },
    new TuberDriver()
    {
        Id = 2,
        Name = "Fred Frederickson",
        TuberDeliveries = 8,
    },
    new TuberDriver()
    {
        Id = 3,
        Name = "George Tippins",
        TuberDeliveries = 12,
    },
};

List<Topping> toppings = new List<Topping>()
{
    //5 toppings
    new Topping()
    {
        Id = 1,
        Name = "Bacon Bits"
    },
    new Topping()
    {
        Id = 2,
        Name = "Sour Cream"
    },
    new Topping()
    {
        Id = 3,
        Name = "Cheese"
    },
    new Topping()
    {
        Id = 4,
        Name = "Butter"
    },
    new Topping()
    {
        Id = 5,
        Name = "Salt"
    },
};

List<TuberOrder> tuberOrders = new List<TuberOrder>()
{
    //3 orders, some have toppings
    new TuberOrder()
    {
        Id = 1,
        OrderPlacedOnDate = new DateTime(2024, 11, 13, 10, 30, 0),
        CustomerId = 1,
        TuberDriverId = 1,
        DeliveredOnDate = new DateTime(2024, 11, 13, 11, 1, 0),
        Toppings = new List<Topping>
        {
            new Topping { Id = 1, Name = "Bacon Bits"},
            new Topping { Id = 2, Name = "Sour Cream"},
            new Topping { Id = 3, Name = "Cheese"},
            new Topping { Id = 4, Name = "Butter"},
            new Topping { Id = 5, Name = "Salt"},
        }
    },
    new TuberOrder()
    {
        Id = 2,
        OrderPlacedOnDate = new DateTime(2024, 11, 13, 13, 30, 0),
        CustomerId = 1,
        TuberDriverId = 1,
        DeliveredOnDate = new DateTime(2024, 11, 13, 13, 59, 0),
        Toppings = new List<Topping>
        {
            new Topping { Id = 4, Name = "Butter"},
            new Topping { Id = 5, Name = "Salt"},
        }
    },
    new TuberOrder()
    {
        Id = 3,
        OrderPlacedOnDate = new DateTime(2024, 11, 13, 16, 20, 0),
        CustomerId = 1,
        TuberDriverId = 1,
        Toppings = new List<Topping>
        {
            new Topping { Id = 2, Name = "Sour Cream"},
            new Topping { Id = 3, Name = "Cheese"},
        }
    },
};

app.MapGet("/api/orders", () => 
{
    return Results.Ok(tuberOrders);
});
app.MapGet("/api/orders/{id}", (int id) => 
{
    TuberOrder tuberOrder = tuberOrders.FirstOrDefault(to => to.Id == id);
    return Results.Ok(tuberOrder);
});
app.MapPost("/api/orders", (TuberOrder tuberOrder) => 
{
    tuberOrder.OrderPlacedOnDate = DateTime.Now;
    tuberOrders.Add(tuberOrder);

    return Results.Created($"/api/orders/{tuberOrder.Id}", tuberOrder);
});
app.MapPut("/api/orders/{id}", (int id, TuberOrder tuberOrder) => 
{
    TuberOrder tuberToChange = tuberOrders.FirstOrDefault(to => to.Id == id);
    tuberToChange.CustomerId = 2;

    return Results.Ok(tuberToChange);

});
app.MapPost("/api/orders/{id}/complete", (int id, TuberOrder tuberOrder) => 
{
    TuberOrder tuberToChange = tuberOrders.FirstOrDefault(to => to.Id == id);
    tuberToChange.DeliveredOnDate = DateTime.Now;

    return Results.Ok(tuberToChange);
});



app.Run();
//don't touch or move this!
public partial class Program { }