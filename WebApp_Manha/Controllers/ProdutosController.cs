using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp_Manha.Entidades;
using WebApp_Manha.Models;
using Microsoft.AspNetCore.Hosting;

namespace WebApp_Manha.Controllers
{
    public class ProdutosController : Controller
    {
        private Contexto db;
        private IWebHostEnvironment webHostEnvironment;
        
        public ProdutosController(Contexto contexto, IWebHostEnvironment _web)
        {
            db = contexto;
            webHostEnvironment = _web;
            
        }


        public IActionResult Lista()
        {
            List<Produtos> model = new List<Produtos>();
            model = db.Produtos.Include(a => a.Categoria).ToList();
            return View(  model  );
        }

        public IActionResult Cadastro()
        {
            NovoProdutoModelView model = new NovoProdutoModelView();
            model.ListaCategorias = db.Categorias.ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult SalvarDados(Produtos dados, IFormFile Imagem )
        {
            if(Imagem.Length > 0)
            {
                string caminho = webHostEnvironment.WebRootPath + "\\upload\\";
                if (!Directory.Exists(caminho))
                {
                    Directory.CreateDirectory(caminho);
                }

                using(var stream = System.IO.File.Create(caminho+Imagem.FileName))
                {
                    Imagem.CopyToAsync(stream);
                }
                dados.CaminhoImagem = Imagem.FileName;
            }
            db.Produtos.Add(dados);
            db.SaveChanges();
            return RedirectToAction("Lista");
        }
    }
}
