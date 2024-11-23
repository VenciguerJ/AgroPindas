using agropindas.Models;
using agropindas.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5067); //
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

//string connectionString = @"Server=localhost;Database=PINDUCAS_farm;Integrated Security=True;
//                        TrustServerCertificate=True;";

string connectionString = @"Server=DESKTOP-ADLTFRR\DATABASEXEANSAO;Database=PINDUCAS_farm;Integrated Security=True;TrustServerCertificate=True;";


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers(); // pode tirar
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));

builder.Services.AddScoped<ILogin<Funcionario>, LoginRepository>();
builder.Services.AddScoped<ICrudRepository<Funcionario>, FuncionarioRepository>();
builder.Services.AddScoped<ICrudRepository<Fornecedor>, FornecedorRepository>();
builder.Services.AddScoped<ICrudRepository<Produto>, ProdutoRepository>();
builder.Services.AddScoped<ISelectItems<ProdAssets>, ProdAssetsRepository>();
builder.Services.AddScoped<ICrudRepository<Compra>, CompraRepositoy>();
builder.Services.AddScoped<ICrudRepository<Producao>, ProducaoRepository>();
builder.Services.AddScoped < ICrudRepository < Cliente >, ClienteRepository > ();
builder.Services.AddScoped<EstoqueRepository>();
builder.Services.AddScoped<EstoqueRepository>();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

//builder.Services.AddControllersWithViews();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(" / Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
