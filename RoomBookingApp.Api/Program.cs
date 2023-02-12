using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Persistence;
using RoomBookingApp.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Add Sqlite in memory
var connString = "DataSource=:memory:";
var conn = new SqliteConnection(connString);
conn.Open();
builder.Services.AddDbContext<RoomBookingAppDbContext>(opt =>
{
    opt.UseSqlite(conn);
});
EnsureDatabaseCreated(conn);
#endregion

#region add injectible services
builder.Services.AddScoped<IRoomBookingRequestProcessor, RoomBookingRequestProcessor>();
builder.Services.AddScoped<IRoomBookingService, RoomBookingService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//make sure the db is actually created at runtime
static void EnsureDatabaseCreated(SqliteConnection conn)
{
    var builder = new DbContextOptionsBuilder<RoomBookingAppDbContext>();
    builder.UseSqlite(conn);

    using var context = new RoomBookingAppDbContext(builder.Options);
    context.Database.EnsureCreated();
}
