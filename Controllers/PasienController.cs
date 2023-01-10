using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using simrs.Models;

namespace simrs.Controllers;

public class PasienController : Controller
{
    private readonly ILogger<PasienController> _logger;

    PasienDataAccess obj_pasien = new PasienDataAccess();
    public PasienController(ILogger<PasienController> logger)
    {
        _logger = logger;
    }

    // Menampilkan data seluruh pasien
    // Data didaptkan  method GetAllPasiens di PasienDataAccess
    public IActionResult Index()
    {
        List<PasienViewModel> list_pasien = new List<PasienViewModel>();
        list_pasien = obj_pasien.GetAllPasiens().ToList();
        return View(list_pasien);
    }
    // Menampilkan Halaman Create sebelum data di insert
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // Validasi data yang di input 
    // insert data ke database 
    // jika berhasil langsung diarahkan halaman Create , IGD untuk kunjungan 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind] PasienViewModel Pasien)
    {
        PasienViewModel data_pasien = obj_pasien.GetPasienData(Pasien.nik_pasien);
        if (data_pasien.nik_pasien == Pasien.nik_pasien)
        {
            TempData["AlertMessage"] = "Nik Sudah Terpakai";
            return RedirectToAction("Create");
        }
        if (ModelState.IsValid)
        {
            obj_pasien.AddPasien(Pasien);
            return RedirectToAction("Create", "IGD");
        }
        return View(Pasien);
    }

    // Mengambil data pasien berdasrkan nik 
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        PasienViewModel data_pas = obj_pasien.GetPasienData(id);
        if (data_pas == null)
        {
            return NotFound();
        }
        return View(data_pas);
    }
    // Update data pasien berdasrkan data yang sudah di inputkan
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind] PasienViewModel pasien)
    {
        if (id != pasien.nik_pasien)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            obj_pasien.UpdatePasien(pasien);
            return RedirectToAction("Index");
        }
        return View(pasien);
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

