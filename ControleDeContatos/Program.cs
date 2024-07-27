using ControleDeContatos.Data;
using ControleDeContatos.Repositorio;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;


namespace ControleDeContatos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Adiciona o contexto do banco de dados
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<BancoContext>(o => o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            
            // Injeção de dependência. Toda vez que a interface for invocada, a injeção automaticamente vai usar tudo da classe repositorio
            builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
