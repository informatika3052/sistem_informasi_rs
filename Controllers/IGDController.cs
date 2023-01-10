using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using sistem_informasi_rs.Models;

namespace sistem_informasi_rs.Controllers;

public class IGDController : Controller
{
    private readonly ILogger<IGDController> _logger;

    public IGDController(ILogger<IGDController> logger)
    {
        _logger = logger;
    }

    IgdDataAccess obj_igd = new IgdDataAccess();
    DokterDataAccess obj_dokter = new DokterDataAccess();
    PasienDataAccess obj_pasien = new PasienDataAccess();

    // Menampilkan data pemulangan pasien kunjungan 
    public IActionResult Index()
    {
        List<IgdViewModel> list_igd = new List<IgdViewModel>();
        list_igd = obj_igd.GetDataKunjungan().ToList();
        return View(list_igd);
    }

    // Mengambil data pasien yang baru daftar
    [HttpGet]
    public IActionResult Create()
    {
        IgdViewModel list_igd = obj_igd.GetPasienToIGD();
        return View(list_igd);
    }
    // Pendaftaran pasien ke IGD 
    // Update status_kunjungan pasien
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind] IgdViewModel IgdCreate)
    {
        if (ModelState.IsValid)
        {
            obj_igd.AddIgdPasien(IgdCreate);
            obj_igd.UpdateStatusPasien(IgdCreate);
            return RedirectToAction("Index");
        }
        return View(IgdCreate);
    }
    //  Mengambil data pasien yang sudah pernah daftar/kunjungan
    public IActionResult CreatePasienLama(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        // PasienViewModel data_pas = obj_pasien.GetPasienData(id);
        IgdViewModel list_igd = obj_igd.GetPasienLamaToIGD(id);
        if (list_igd == null)
        {
            return NotFound();
        }
        return View(list_igd);
    }
    // Pendaftaran pasien lama ke kunjungan IGD
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreatePasienLama([Bind] IgdViewModel IgdCreatePasienLama)
    {
        if (ModelState.IsValid)
        {
            obj_igd.AddIgdPasien(IgdCreatePasienLama);
            obj_igd.UpdateStatusPasien(IgdCreatePasienLama);
            return RedirectToAction("Index");
        }
        return View(IgdCreatePasienLama);
    }

    // Mengambil data pasien berdasarkan id kunjungan
    [HttpGet]
    public IActionResult UpdatePulang(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        IgdViewModel data_pulang = obj_igd.GetKunjunganIdPulang(id);
        if (data_pulang == null)
        {
            return NotFound();
        }
        return View(data_pulang);
    }
    // Update data status_kunjung pasien di kunjungan
    // Update status_kunjungan pasien di master pasien
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdatePulang(int id, [Bind] IgdViewModel igd)
    {
        if (id != igd.id_kunjungan)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            obj_igd.UpdateStatPulangKunjung(igd);
            obj_igd.UpdateStatPulangPasien(igd);
            return RedirectToAction("Index", "IGD");
        }
        return View(igd);
    }
    // Mengambil date pasien lama yang sudah pernah daftar
    public IActionResult PasienLama()
    {
        List<PasienViewModel> listPasienLama = new List<PasienViewModel>();
        listPasienLama = obj_pasien.PasienLama().ToList();
        return View(listPasienLama);
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}

// IgdViewModel IgdViewModels = new IgdViewModel();

// List<PasienViewModel> list_pasien = new List<PasienViewModel>();
// list_pasien = obj_pasien.GetAllPasiens().ToList();
// IgdViewModels.pasien = list_pasien;

// List<DokterViewModel> list_dokter = new List<DokterViewModel>();
// list_dokter = obj_dokter.GetAllDokter().ToList();
// IgdViewModels.dokter = list_dokter;


// list_igd.Add(IgdViewModels);

// return View(list_pasien);