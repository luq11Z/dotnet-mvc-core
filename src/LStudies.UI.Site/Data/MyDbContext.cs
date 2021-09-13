using LStudies.UI.Site.Models;
using Microsoft.EntityFrameworkCore;

namespace LStudies.UI.Site.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<Aluno> Alunos { get; set; }
    }

    //add-migration para "criar estrutura da bd"
    //update-database vai gerar uma bd com base na migration
    //e possivel remover as migrations a partir da pasta migrations
}
