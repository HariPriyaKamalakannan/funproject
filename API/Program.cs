
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using API.Middleware;
using Microsoft.AspNetCore.Mvc;
using API.Errors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductRepository,ProductsRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<ApiBehaviorOptions>(options=>
{
    options.InvalidModelStateResponseFactory=ActionContext=>
    {
        var error=ActionContext.ModelState
        .Where(e=>e.Value.Errors.Count > 0)
        .SelectMany(x=>x.Value.Errors)
        .Select(x=>x.ErrorMessage).ToArray();

        var errorResponse=new APIValidationErrorResponse
        {
            Errors=error
            
        };
        return new BadRequestObjectResult(errorResponse);
    };
});

builder.Services.AddCors(opt=>
{
    opt.AddPolicy("CorsPolicy",policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
    } );
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/error/{0}");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

using var scope=app.Services.CreateScope();
var services=scope.ServiceProvider;
var context=services.GetRequiredService<StoreContext>();
var logger=services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);

}
catch(Exception ex)
{
logger.LogError(ex,"An error occured during migration");
}

app.Run();
