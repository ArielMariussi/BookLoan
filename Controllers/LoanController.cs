using EmprestimoLivros.Data;
using EmprestimoLivros.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmprestimoLivros.Controllers
{
    [Authorize]
    public class LoanController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public LoanController(
            ApplicationDbContext db,
            UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        private string GetUserId()
        {
            return _userManager.GetUserId(User) ?? string.Empty;
        }



        public IActionResult Index()
        {
            var userId = GetUserId();

            IEnumerable<Loan> loans = _db.Loans
                .Where(l => l.UserId == userId)
                .ToList();

            return View(loans);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Loan loan)
        {

            loan.UserId = GetUserId();
            loan.LastUpdatedAt = DateTime.UtcNow;

            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                _db.Loans.Add(loan);
                _db.SaveChanges();

                TempData["SuccessMessage"] = "Loan created successfully";

                return RedirectToAction("Index");
            }
            return View(loan);

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            var userId = GetUserId();

            var loan = _db.Loans
                .FirstOrDefault(l => l.Id == id && l.UserId == userId);

            if (loan == null)
            {
                TempData["ErrorMessage"] = "Loan not found!";
                return RedirectToAction("Index");
            }

            return View(loan);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Loan loan)
        {
            var userId = GetUserId();

            var loanFromDb = _db.Loans
                .AsNoTracking()
                .FirstOrDefault(l => l.Id == loan.Id && l.UserId == userId);

            if (loanFromDb == null)
            {
                TempData["ErrorMessage"] = "You do not have permission to edit this loan!";
                return RedirectToAction("Index");
            }

            loan.UserId = userId;
            loan.LastUpdatedAt = DateTime.UtcNow;
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                _db.Loans.Update(loan);
                _db.SaveChanges();

                TempData["SuccessMessage"] = "Loan updated successfully";

                return RedirectToAction("Index");
            }

            return View(loan);

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {

            var userId = GetUserId();

            var loan = _db.Loans
                .FirstOrDefault(l => l.Id == id && l.UserId == userId);

            if (loan == null)
            {
                TempData["ErrorMessage"] = "Loan not found!";
                return RedirectToAction("Index");
            }

            return View(loan);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Loan loan)
        {
            var userId = GetUserId();

            var loanFromDb = _db.Loans
                .FirstOrDefault(l => l.Id == loan.Id && l.UserId == userId);

            if (loanFromDb == null)
            {
                TempData["ErrorMessage"] = "Loan not found!";
                return RedirectToAction("Index");
            }

            _db.Loans.Remove(loanFromDb);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Loan deleted successfully!";
            return RedirectToAction("Index");


        }
    }
}
