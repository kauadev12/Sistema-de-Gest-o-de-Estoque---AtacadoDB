using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Gestão_de_Estoque.data;
using Sistema_de_Gestão_de_Estoque.Models;

namespace SistemaDeGestao.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly BancoContext _context;

        public FornecedoresController(BancoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string filtro, int pagina = 1)
        {
            var TamanhoPagina = 10;

            var qry = _context.Fornecedores.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var f = filtro.Trim();

                // Busca DB-side (melhor para performance e case-insensitive em SQL Server)
                qry = qry.Where(x =>
                    EF.Functions.Like(x.Nome, $"%{f}%") ||
                    EF.Functions.Like(x.CNPJ, $"%{f}%")
                );
            }

            int TotalFornecedores = await qry.CountAsync();

            var fornecedores = await qry.OrderBy(p => p.Id)
                                        .Skip((pagina - 1) * TamanhoPagina)
                                        .Take(TamanhoPagina)
                                        .ToListAsync();

            ViewBag.PaginaAtual = pagina;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)TotalFornecedores / TamanhoPagina);
            ViewBag.Filtro = filtro; // se quiser usar na View
            return View(fornecedores);
        }
        

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fornecedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedor);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null) return NotFound();
            return View(fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Fornecedor fornecedor)
        {
            if (id != fornecedor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(fornecedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedor);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null) return NotFound();
            return View(fornecedor);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor != null)
            {
                _context.Fornecedores.Remove(fornecedor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null) return NotFound();
            return View(fornecedor);
        }
    }
}
