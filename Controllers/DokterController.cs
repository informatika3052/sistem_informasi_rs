using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using sistem_informasi_rs.Models;

namespace sistem_informasi_rs.Controllers;

public class DokterController : Controller
{
    private readonly ILogger<DokterController> _logger;
    DokterDataAccess obj_dokter = new DokterDataAccess();

    public DokterController(ILogger<DokterController> logger)
    {
        _logger = logger;
    }
    // Menampilkan data seluruh Dokter
    // Data didapatkan method GetAllDokter di DokterDataAccess
    public IActionResult Index()
    {
        List<DokterViewModel> listDokter = new List<DokterViewModel>();
        listDokter = obj_dokter.GetAllDokter().ToList();
        return View(listDokter);
    }

    // Menampilkan data seluruh Dokter
    // Data ditampilakan saat kunjungan pasien
    // Data dikembalikan dalam bentuk Json dan di request ajax di file layout cshtml
    public IActionResult GetData()
    {
        List<DokterViewModel> listDokter = new List<DokterViewModel>();
        listDokter = obj_dokter.GetAllDokter().ToList();
        return Json(listDokter);
    }
    // Menampilkan Halaman Create sebelum data di insert
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    // Validasi data yang di input 
    // insert data ke database 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind] DokterViewModel Dokter)
    {
        DokterViewModel data_dokter = obj_dokter.GetDokterData(Dokter.nik);
        if (data_dokter.nik == Dokter.nik)
        {
            TempData["AlertMessage"] = "Nik Sudah Terpakai";
            return RedirectToAction("Create");
        }
        if (ModelState.IsValid)
        {
            obj_dokter.AddDokter(Dokter);
            return RedirectToAction("Index");
        }
        return View(Dokter);
    }
    // Mengambil data dokter berdasrkan nik 
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        DokterViewModel data_dokter_id = obj_dokter.GetDokterData(id);
        if (data_dokter_id == null)
        {
            return NotFound();
        }
        return View(data_dokter_id);
    }
    // Update data dokter berdasrkan data yang sudah di inputkan
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind] DokterViewModel dokter)
    {
        if (id != dokter.nik)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            obj_dokter.UpdateDokter(dokter);
            return RedirectToAction("Index");
        }
        return View(dokter);
    }

    // Mengambil data dokter berdasrkan nik 
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        DokterViewModel dokter = obj_dokter.GetDokterData(id);
        if (dokter == null) { return NotFound(); }
        return View(dokter);
    }
    // Delete data dokter berdasarkan data diambil sebelumnya
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int? nik)
    {
        obj_dokter.DeleteDokter(nik);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

