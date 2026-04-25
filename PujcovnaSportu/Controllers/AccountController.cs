using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PujcovnaSportu.Models;
using System.Security.Cryptography;
using System.Text;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string jmeno, string prijmeni, string email, string heslo, string telefon)
    {
        // Zkontroluje jestli email už existuje
        if (await _context.Uzivatele.AnyAsync(u => u.Email == email))
        {
            ViewBag.Chyba = "Tento email je již registrován.";
            return View();
        }

        // Zahashuje heslo
        var hesloHash = HashHeslo(heslo);

        // Najdi roli Uzivatel
        var role = await _context.Role.FirstOrDefaultAsync(r => r.Nazev == "Uzivatel");

        var uzivatel = new Uzivatel
        {
            Jmeno = jmeno,
            Prijmeni = prijmeni,
            Email = email,
            HesloHash = hesloHash,
            Telefon = telefon,
            DatumRegistrace = DateTime.Now,
            IdRole = role.IdRole
        };

        _context.Uzivatele.Add(uzivatel);
        await _context.SaveChangesAsync();

        return RedirectToAction("Login");
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string heslo)
    {
        var hesloHash = HashHeslo(heslo);

        var uzivatel = await _context.Uzivatele
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email && u.HesloHash == hesloHash);

        if (uzivatel == null)
        {
            ViewBag.Chyba = "Špatný email nebo heslo.";
            return View();
        }

        // Ulož uživatele do session
        HttpContext.Session.SetInt32("UzivatelId", uzivatel.IdUzivatel);
        HttpContext.Session.SetString("UzivatelJmeno", uzivatel.Jmeno);
        HttpContext.Session.SetString("UzivatelRole", uzivatel.Role.Nazev);

        // Přesměruj podle role
        if (uzivatel.Role.Nazev == "ADMIN")
            return RedirectToAction("Index", "Admin");
        else
            return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    private string HashHeslo(string heslo)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(heslo));
        return Convert.ToBase64String(bytes);
    }
}