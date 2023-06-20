using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SignInTPMedia.Data;
using SignInTPMedia.Models;

namespace SignInTPMedia.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly Data.QLNSContext _context;

        public KhachHangController(Data.QLNSContext context)
        {
            _context = context;
        }

        // GET: KhachHang
        public async Task<IActionResult> Index()
        {
              return _context.Khachhangs != null ? 
                          View(await _context.Khachhangs.ToListAsync()) :
                          Problem("Entity set 'QLNSContext.Khachhangs'  is null.");
        }

        // GET: KhachHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Khachhangs == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }

        // GET: KhachHang/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKh,TenKh,Sdt,Email,DiaChiLienHe,TenCongty,DiaChiCongTy,MaSoThue")] Khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khachhang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khachhang);
        }

        // GET: KhachHang/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Khachhangs == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs.FindAsync(id);
            if (khachhang == null)
            {
                return NotFound();
            }
            return View(khachhang);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaKh,TenKh,Sdt,Email,DiaChiLienHe,TenCongty,DiaChiCongTy,MaSoThue")] Khachhang khachhang)
        {
            if (id != khachhang.MaKh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachhang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachhangExists(khachhang.MaKh))
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
            return View(khachhang);
        }


        // POST: KhachHang/Delete/5

        public async Task<IActionResult> Delete(int id)
        {
            var khachhang = await _context.Khachhangs.FindAsync(id);
            if (khachhang == null)
            {
                return NotFound();
            }

            // set references to null
            var baocaos = _context.Baocaos.Where(b => b.MaKh == id);
            _context.Baocaos.RemoveRange(baocaos);
            
            var kehoachs = _context.Kehoaches.Where(b => b.MaKh == id);
            _context.Kehoaches.RemoveRange(kehoachs);

            _context.Khachhangs.Remove(khachhang);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool KhachhangExists(int id)
        {
          return (_context.Khachhangs?.Any(e => e.MaKh == id)).GetValueOrDefault();
        }
    }
}
