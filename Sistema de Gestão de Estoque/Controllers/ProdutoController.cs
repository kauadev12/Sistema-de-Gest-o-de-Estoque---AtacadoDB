using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Gestão_de_Estoque.data;
using Sistema_de_Gestão_de_Estoque.Models;

namespace SistemaDeGestao.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly BancoContext _context;

        public ProdutosController(BancoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string filtro, int pagina = 1)
        {
            var TamanhoPagina = 10;

            var produtosQuery = _context.Produtos.Include(p => p.Fornecedor).AsQueryable().AsNoTracking();

            if (!string.IsNullOrEmpty(filtro))
            {
                produtosQuery = produtosQuery.Where(p => (p.Nome.Contains(filtro) || p.Fornecedor.Nome.Contains(filtro)));
            }

           int TotalProdutos = await produtosQuery.CountAsync();

            var produtos = await produtosQuery.OrderBy(p => p.Id)
                                              .Skip((pagina - 1) * TamanhoPagina)
                                              .Take(TamanhoPagina)
                                              .ToListAsync();

            ViewBag.PaginaAtual = pagina;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)TotalProdutos / TamanhoPagina);
            ViewBag.Filtro = filtro;

            return View( produtos);

        }


        public IActionResult Create()
        {
            ViewBag.Fornecedores = new SelectList(_context.Fornecedores, "Id", "Nome");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Fornecedores = new SelectList(_context.Fornecedores, "Id", "Nome", produto.FornecedorId);
            return View(produto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound();

            ViewBag.Fornecedores = new SelectList(_context.Fornecedores, "Id", "Nome", produto.FornecedorId);
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            if (id != produto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Fornecedores = new SelectList(_context.Fornecedores, "Id", "Nome", produto.FornecedorId);
            return View(produto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _context.Produtos.Include(p => p.Fornecedor).FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null) return NotFound();

            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var produto = await _context.Produtos.Include(p => p.Fornecedor).FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null) return NotFound();

            return View(produto);
        }
    }
}
