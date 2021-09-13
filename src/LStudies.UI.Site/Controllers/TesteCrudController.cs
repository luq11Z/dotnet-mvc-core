using LStudies.UI.Site.Data;
using LStudies.UI.Site.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LStudies.UI.Site.Controllers
{
    public class TesteCrudController : Controller
    {
        private readonly MyDbContext _context;

        public TesteCrudController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var aluno = new Aluno 
            {
                Nome = "Lucas",
                DataNascimento = DateTime.UtcNow,
                Email = "lucas.learning@learning.com"
            };

            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            var aluno2 = _context.Alunos.Find(aluno.Id);
            var aluno3 = _context.Alunos.FirstOrDefault(x => x.Email == "lucas.learning@learning.com");

            aluno.Nome = "Lukas";
            _context.Alunos.Update(aluno);
            _context.SaveChanges();

            _context.Alunos.Remove(aluno);
            _context.SaveChanges();

            return View("_Layout");

            //por breakpoint na linha 20 para validar as operacoes de persistencia na bd
        }
    }
}
