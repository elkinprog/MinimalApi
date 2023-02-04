using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Context;
using PizzaStore.DTOS;

var builder = WebApplication.CreateBuilder(args);

var connectionstring = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source = Pizzas.db";
builder.Services.AddSqlite<PizzaDbContext>(connectionstring);


builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>{
    c.SwaggerDoc("v1",new OpenApiInfo{
        Title="PizzaStore API",
        Description="Makin the Pizzas you love",
        Version = "v1"});
});



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI (c=> {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API v1");
});


app.MapGet("/GetPizza", async(PizzaDbContext db) => await db.Pizza.ToListAsync());

app.MapGet("/GetPizzaId",async (PizzaDbContext db,int id)=> await db.Pizza.FindAsync(id));

app.MapPost("/Postpizza", async (PizzaDbContext db, PizzaDTO pizza)=>{
    await db.Pizza.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}",pizza);
});

app.MapPut("/PutPizza",async (PizzaDbContext  db, PizzaDTO updatepizza,int id) => {
    var pizza = await db.Pizza.FindAsync(id);
    if(pizza == null) return Results.NotFound();
    pizza.Name = updatepizza.Name;
    pizza.Description = updatepizza.Description;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/DeletePizza",async (PizzaDbContext db, int id) => {
    var pizza = await db.Pizza.FindAsync(id);
    if(pizza == null )
    {
        return Results.NotFound();
    }
        db.Pizza.Remove(pizza);
        await db.SaveChangesAsync();
        return Results.Ok();
});

app.Run();


