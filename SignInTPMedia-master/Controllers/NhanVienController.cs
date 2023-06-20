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
    public class NhanVienController : Controller
    {
        private readonly Data.QLNSContext _context;
       /* private readonly string _imageFolderPath = "/images/nhanvien/";*/
        public NhanVienController(Data.QLNSContext context)
        {
            _context = context;
        }

        // GET: NhanVien
        public async Task<IActionResult> Index()
        {
            var qLNSContext = _context.Nhanviens.Include(n => n.MaChucVuNavigation).Include(n => n.MaPhongBanNavigation);
            return View(await qLNSContext.ToListAsync());
        }

        // GET: NhanVien/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nhanviens == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens
                .Include(n => n.MaChucVuNavigation)
                .Include(n => n.MaPhongBanNavigation)
                .FirstOrDefaultAsync(m => m.MaNv == id);

            if (nhanvien == null)
            {
                return NotFound();
            }

            ViewBag.TenChucVu = nhanvien.MaChucVuNavigation.TenChucVu;
            ViewBag.TenPhongBan = nhanvien.MaPhongBanNavigation.TenPhongBan;

            if (nhanvien.HinhAnhBytes != null && nhanvien.HinhAnhBytes.Length > 0)
            {
                string imageBase64Data = Convert.ToBase64String(nhanvien.HinhAnhBytes);
                string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                ViewBag.ImageDataUrl = imageDataURL;
            }

            return View(nhanvien);
        }


        // GET: NhanVien/Create
        public IActionResult Create()
        {
            ViewBag.ChucVuList = new SelectList(_context.Chucvus, "MaChucVu", "TenChucVu");
            ViewBag.PhongBanList = new SelectList(_context.Phongbans, "MaPhongBan", "TenPhongBan");
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Nhanvien nhanvien)
        {
            if (ModelState.IsValid)
            {
                if (nhanvien.HinhAnhFile != null && nhanvien.HinhAnhFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await nhanvien.HinhAnhFile.CopyToAsync(stream);
                        nhanvien.HinhAnhBytes = stream.ToArray();
                    }
                    nhanvien.HinhAnh = nhanvien.HinhAnhFile.FileName;
                }

                _context.Add(nhanvien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error.ErrorMessage);
            }
            ViewBag.ChucVuList = new SelectList(_context.Chucvus, "MaChucVu", "TenChucVu");
            ViewBag.PhongBanList = new SelectList(_context.Phongbans, "MaPhongBan", "TenPhongBan");
            return View(nhanvien);
        }


        // GET: NhanVien/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nhanviens == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens.FindAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }
            ViewBag.ChucVuList = new SelectList(_context.Chucvus, "MaChucVu", "TenChucVu");
            ViewBag.PhongBanList = new SelectList(_context.Phongbans, "MaPhongBan", "TenPhongBan");
            return View(nhanvien);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNv,TenNv,NgaySinh,GioiTinh,DiaChi,MaPhongBan,MaChucVu,HinhAnh,HinhAnhFile")] Nhanvien nhanvien)
        {
            if (id != nhanvien.MaNv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var nv = await _context.Nhanviens.FindAsync(id);

                    if (nhanvien.HinhAnhFile != null && nhanvien.HinhAnhFile.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await nhanvien.HinhAnhFile.CopyToAsync(stream);
                            nhanvien.HinhAnhBytes = stream.ToArray();
                        }
                        nhanvien.HinhAnh = nhanvien.HinhAnhFile.FileName;
                    }
                    else
                    {
                        // Giữ nguyên tập tin hình ảnh cũ
                        nhanvien.HinhAnh = nv.HinhAnh;
                        nhanvien.HinhAnhBytes = nv.HinhAnhBytes;
                    }

                    if (string.IsNullOrEmpty(nhanvien.HinhAnh))
                    {
                        // Sử dụng đường dẫn hình ảnh cũ
                        nhanvien.HinhAnh = nv.HinhAnh;
                        nhanvien.HinhAnhBytes = nv.HinhAnhBytes;
                    }


                    _context.Entry(nv).CurrentValues.SetValues(nhanvien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanvienExists(nhanvien.MaNv))
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

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error.ErrorMessage);
            }
            ViewBag.ChucVuList = new SelectList(_context.Chucvus, "MaChucVu", "TenChucVu", nhanvien.MaChucVu);
            ViewBag.PhongBanList = new SelectList(_context.Phongbans, "MaPhongBan", "TenPhongBan", nhanvien.MaPhongBan);
            return View(nhanvien);

        }


        // POST: NhanVien/Delete/5       
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Nhanviens == null)
            {
                return Problem("Entity set 'QLNSContext.Nhanviens'  is null.");
            }
            var nhanvien = await _context.Nhanviens.FindAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }
            _context.Nhanviens.Remove(nhanvien);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanvienExists(int id)
        {
          return (_context.Nhanviens?.Any(e => e.MaNv == id)).GetValueOrDefault();
        }
    }
}
