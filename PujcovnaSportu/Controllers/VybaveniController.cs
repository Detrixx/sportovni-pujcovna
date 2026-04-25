using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class VybaveniController : Controller
{
    private readonly AppDbContext _context;

    public VybaveniController(AppDbContext context)
    {
        _context = context;
    }

    // Seznam vybavení - vidí všichni přihlášení
    public async Task<IActionResult> Index()
    {
        if (HttpContext.Session.GetInt32("UzivatelId") == null)
            return RedirectToAction("Login", "Account");

        var vybaveni = await _context.Vybaveni
            .Include(v => v.TypVybaveni)
            .Include(v => v.StavVybaveni)
            .ToListAsync();

        return View(vybaveni);
    }

    // Přidat vybavení - pouze Admin
    //public IActionResult Create()
    //{
    //    if (HttpContext.Session.GetString("UzivatelRole") != "Admin")
    //        return RedirectToAction("Index");

    //    return View();
    //}

    public async Task<IActionResult> Create()
    {
        if (HttpContext.Session.GetString("UzivatelRole") != "Admin")
            return RedirectToAction("Index");

        ViewBag.Typy = await _context.TypyVybaveni.ToListAsync();
        ViewBag.Stavy = await _context.StavyVybaveni.ToListAsync();
        return View();
    }

    //public async Task<IActionResult> Create(Vybaveni vybaveni)
    //{
    //    if (HttpContext.Session.GetString("UzivatelRole") != "Admin")
    //        return RedirectToAction("Index");

    //    _context.Vybaveni.Add(vybaveni);
    //    await _context.SaveChangesAsync();
    //    return RedirectToAction("Index");
    //}
    
    public async Task<IActionResult> Edit(int id)
    {
        if (HttpContext.Session.GetString("UzivatelRole") != "Admin")
            return RedirectToAction("Index");

        var vybaveni = await _context.Vybaveni.FindAsync(id);
        if (vybaveni == null)
            return NotFound();

        ViewBag.Typy = await _context.TypyVybaveni.ToListAsync();
        ViewBag.Stavy = await _context.StavyVybaveni.ToListAsync();
        return View(vybaveni);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Vybaveni vybaveni, string? novyTyp)
    {
        if (HttpContext.Session.GetString("UzivatelRole") != "Admin")
            return RedirectToAction("Index");

        // Zkontroluj duplicitní název (kromě sebe sama)
        if (await _context.Vybaveni.AnyAsync(v => v.Nazev == vybaveni.Nazev && v.IdVybaveni != id))
        {
            ViewBag.Chyba = "Vybavení s tímto názvem již existuje!";
            ViewBag.Typy = await _context.TypyVybaveni.ToListAsync();
            ViewBag.Stavy = await _context.StavyVybaveni.ToListAsync();
            return View(vybaveni);
        }

        // Pokud zadal nový typ, vytvoř ho
        if (!string.IsNullOrWhiteSpace(novyTyp))
        {
            var existujiciTyp = await _context.TypyVybaveni
                .FirstOrDefaultAsync(t => t.Nazev == novyTyp);

            if (existujiciTyp != null)
                vybaveni.IdTyp = existujiciTyp.IdTyp;
            else
            {
                var typ = new TypVybaveni { Nazev = novyTyp };
                _context.TypyVybaveni.Add(typ);
                await _context.SaveChangesAsync();
                vybaveni.IdTyp = typ.IdTyp;
            }
        }

        if (string.IsNullOrEmpty(vybaveni.Metadata))
            vybaveni.Metadata = null;

        vybaveni.IdVybaveni = id;
        _context.Update(vybaveni);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }





    [HttpPost]
    public async Task<IActionResult> Create(Vybaveni vybaveni, string? novyTyp)
    {
        if (HttpContext.Session.GetString("UzivatelRole") != "Admin")
            return RedirectToAction("Index");

        // Zkontroluj duplicitní název
        if (await _context.Vybaveni.AnyAsync(v => v.Nazev == vybaveni.Nazev))
        {
            ViewBag.Chyba = "Vybavení s tímto názvem již existuje!";
            ViewBag.Typy = await _context.TypyVybaveni.ToListAsync();
            return View(vybaveni);
        }

        // Pokud zadal nový typ, vytvoř ho
        if (!string.IsNullOrWhiteSpace(novyTyp))
        {
            // Zkontroluj jestli typ už existuje
            var existujiciTyp = await _context.TypyVybaveni
                .FirstOrDefaultAsync(t => t.Nazev == novyTyp);

            if (existujiciTyp != null)
            {
                vybaveni.IdTyp = existujiciTyp.IdTyp;
            }
            else
            {
                var typ = new TypVybaveni { Nazev = novyTyp };
                _context.TypyVybaveni.Add(typ);
                await _context.SaveChangesAsync();
                vybaveni.IdTyp = typ.IdTyp;
            }
        }

        // Automaticky nastav stav na Dostupné (id_stav = 1)
        vybaveni.IdStav = 1;

        if (string.IsNullOrEmpty(vybaveni.Metadata))
            vybaveni.Metadata = null;

        _context.Vybaveni.Add(vybaveni);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }


    [HttpPost]
    public async Task<IActionResult> Rezervovat(int idVybaveni, DateTime datumOd, DateTime datumDo)
    {
        var idUzivatel = HttpContext.Session.GetInt32("UzivatelId");
        if (idUzivatel == null)
            return RedirectToAction("Login", "Account");

        var vybaveni = await _context.Vybaveni.FindAsync(idVybaveni);
        if (vybaveni == null)
            return NotFound();

        // Vytvoř rezervaci
        var rezervace = new Rezervace
        {
            IdUzivatel = idUzivatel.Value,
            DatumVytvoreni = DateTime.Now,
            DatumOd = datumOd,
            DatumDo = datumDo,
            IdStavRez = 1, // NOVA
            Poznamka = null
        };
        _context.Rezervace.Add(rezervace);
        await _context.SaveChangesAsync();

        // Vytvoř položku rezervace
        var polozka = new RezervacePolozka
        {
            IdRezervace = rezervace.IdRezervace,
            IdVybaveni = idVybaveni,
            Mnozstvi = 1,
            CenaZaDen = vybaveni.CenaZaDen
        };
        _context.RezervacePolozky.Add(polozka);

        // Změň stav vybavení na Rezervované (id_stav = 4)
        vybaveni.IdStav = 6;
        _context.Update(vybaveni);

        await _context.SaveChangesAsync();

        TempData["Uspech"] = "Rezervace byla úspěšně vytvořena!";
        return RedirectToAction("Index");
    }


}