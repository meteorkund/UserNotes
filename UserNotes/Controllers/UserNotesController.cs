using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using UserNotes.Infrastructure;
using UserNotes.Models;

namespace UserNotes.Controllers
{
	public class UserNotesController : Controller
	{
		readonly UserNotesContext _context;

		public UserNotesController(UserNotesContext context)
		{
			_context = context;
		}

		public async Task<ActionResult> Index()
		{
			var userNotes = _context.UserNotes.Include(c => c.UserNoteChildrens).ToList();

			return View(userNotes);
		}

		public IActionResult Create() => View();
		public IActionResult CreateChild() => View();

		#region PARENT CREATE
	
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(UserNote userNote)
		{
			if (ModelState.IsValid)
			{
				_context.Add(userNote);
				await _context.SaveChangesAsync();

				TempData["Success"] = "Yeni not başarıyla eklendi!";

				return RedirectToAction("Index");
			}

			return View(userNote);
		}
		#endregion

		#region PARENT EDIT
		public async Task<ActionResult> Edit(int id)
		{
			UserNote userNoteItem = await _context.UserNotes.FindAsync(id);
			if (userNoteItem == null)
			{
				return NotFound();
			}

			return View(userNoteItem);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(UserNote userNote)
		{
			if (ModelState.IsValid)
			{
				userNote.UpdatedDate = DateTime.Now;
				_context.Update(userNote);
				await _context.SaveChangesAsync();

				TempData["Success"] = "Not güncellendi!";

				return RedirectToAction("Index");
			}

			return View(userNote);
		}
		#endregion

		#region PARENT DELETE
		public async Task<ActionResult> Delete(int id)
		{
			UserNote userNoteItem = await _context.UserNotes.FindAsync(id);
			if (userNoteItem == null)
			{
				TempData["Error"] = "Böyle bir not bulunamadı!";
			}
			else
			{
				_context.UserNotes.Remove(userNoteItem);
				await _context.SaveChangesAsync();

				TempData["Success"] = "Not silindi!";
			}

			return RedirectToAction("Index");
		}
		#endregion

		#region CHILD CREATE
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateChild(UserNote userNote)
		{
			UserNoteChild userNoteChild = new UserNoteChild();
			if (ModelState.IsValid)
			{

				userNoteChild.Content = userNote.Content;
				userNoteChild.UserNoteId = userNote.Id;
				_context.UserNoteChildren.Add(userNoteChild);
				await _context.SaveChangesAsync();

				TempData["Success"] = "Yeni alt not başarıyla eklendi!";

				return RedirectToAction("Index");
			}

			return View(userNoteChild);
		}
		#endregion

		#region CHILD EDIT
		public async Task<ActionResult> EditChild(int id)
		{
			UserNoteChild userNoteChildItem = await _context.UserNoteChildren.FindAsync(id);
			if (userNoteChildItem == null)
			{
				return NotFound();
			}

			return View(userNoteChildItem);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditChild(UserNoteChild userNoteChild)
		{
			if (ModelState.IsValid)
			{
				userNoteChild.UpdatedDate = DateTime.Now;
				_context.Update(userNoteChild);
				await _context.SaveChangesAsync();

				TempData["Success"] = "Alt Not Güncellendi!";

				return RedirectToAction("Index");
			}

			return View(userNoteChild);
		}

		#endregion

		#region CHILD DELETE
		public async Task<ActionResult> DeleteChild(int id)
		{
			UserNoteChild userNoteChildItem = await _context.UserNoteChildren.FindAsync(id);
			if (userNoteChildItem == null)
			{
				TempData["Error"] = "Böyle bir alt not bulunamadı!";
			}
			else
			{
				_context.UserNoteChildren.Remove(userNoteChildItem);
				await _context.SaveChangesAsync();

				TempData["Success"] = "Alt not silindi!";
			}

			return RedirectToAction("Index");
		}
		#endregion
	}
}
