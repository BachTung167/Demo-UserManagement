using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Controllers
{
	public class NguoiDungController : Controller
	{
		private readonly UserManagementDbContext _context;

		public NguoiDungController(UserManagementDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var danhSach = await _context.NguoiDungs.ToListAsync();
			return View(danhSach);
		}

		public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(NguoiDung nguoiDung)
        {
            // Kiểm tra trùng Email trong database
            if (_context.NguoiDungs.Any(u => u.Email == nguoiDung.Email))
            {
                ModelState.AddModelError("Email", "Email này đã tồn tại trong hệ thống");
            }
            if (nguoiDung.NgaySinh.HasValue && nguoiDung.NgaySinh.Value >= DateTime.Now)
            {
                ModelState.AddModelError("NgaySinh", "Ngày sinh phải nhỏ hơn ngày hiện tại.");
            }
            if (ModelState.IsValid) // Chỉ lưu nếu tất cả validate đều qua
            {
                _context.Add(nguoiDung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nguoiDung);
        }

        public async Task<IActionResult> Edit(int? id)
		{
			if (id == null) return NotFound();
			var nguoiDung = await _context.NguoiDungs.FindAsync(id);
			if (nguoiDung == null) return NotFound();
			return View(nguoiDung);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, NguoiDung model)
		{
			if (id != model.Ma) return NotFound();
			if (ModelState.IsValid)
			{
				_context.Update(model);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) return NotFound();
			var nguoiDung = await _context.NguoiDungs.FindAsync(id);
			if (nguoiDung != null)
			{
				_context.NguoiDungs.Remove(nguoiDung);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
		}
	}
}