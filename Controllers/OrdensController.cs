using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
//using Ejemplo.Context;
using Ejemplo.Helpers;
using Ejemplo.Models;

namespace Ejemplo.Controllers
{
    public class OrdensController : Controller
    {
        private readonly MauroContext _context;
        private readonly AzureStoreConfig _config;
        public OrdensController(MauroContext context, IOptions<AzureStoreConfig>config)
        {
            _context = context;
            _config = config.Value;
        }

        // GET: Ordens
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orden.Include(e=>e.Cliente).ToListAsync());
        }

        // GET: Ordens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var orden = await _context.Orden
                .Include(e=>e.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // GET: Ordens/Create
        public async Task<IActionResult> Create()
        {
            var cliente = await _context.Cliente.ToListAsync();
            ViewBag.Cliente = new SelectList(cliente, "Id", "NombreCliente");
            return View();
        }

        // POST: Ordens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Numero,Costo,Fecha,ClienteId")] Orden orden, IFormFile foto)
        {
            if ("Id,Nombre,Numero,Costo,Fecha,ClienteId.Id".Split(',').All(campo=>ModelState.ContainsKey(campo)))
            {
                //var foto = archivo.FirstOrDefault();
                if (foto == null)
                {
                    //var nombre = $"{Guid.NewGuid()}.png";
                    //orden.Foto = await StorageHelper.SubirArchivo
                        //(foto.OpenReadStream(), nombre, _config);

                    //_context.Add(orden);
                    //await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));

                    orden.Foto = StorageHelper.URL_imagen_default;
                }
                else
                {
                    string extension = foto.FileName.Split(".")[1];
                    string nombre = $"{Guid.NewGuid()}.{extension}";
                    orden.Foto = await StorageHelper.SubirArchivo(foto.OpenReadStream(),nombre, _config);
                }
                _context.Set<Orden>().Add(orden);
                _context.Entry(orden.Cliente).State = EntityState.Unchanged;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orden);
        }

        // GET: Ordens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden.FindAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            return View(orden);
        }

        // POST: Ordens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Numero,Foto,Costo,Fecha")] Orden orden)
        {
            if (id != orden.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenExists(orden.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orden);
        }

        // GET: Ordens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orden==null)
            {
                return NotFound();
            }

            var orden = await _context.Orden
                .Include(e=>e.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // POST: Ordens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {                                  
            if (_context.Orden == null)
            {
                return Problem("Entity set 'jaragongcontext.Orden' is null. ");
            }
            var orden = await _context.Orden.FindAsync(id);
            if (orden != null) 
            {
                _context.Orden.Remove(orden);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenExists(int id)
        {
            return (_context.Orden?.Any(e => e.Id == id)).GetValueOrDefault();
            
        }
    }
}
