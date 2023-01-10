using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using simrs.Models;

namespace simrs.Controllers;

public class KasirController : Controller
{
    private readonly ILogger<KasirController> _logger;

    public KasirController(ILogger<KasirController> logger)
    {
        _logger = logger;
    }

    IgdDataAccess obj_igd = new IgdDataAccess();
    KasirDataAccess obj_kasir = new KasirDataAccess();

    // Menampilkan data status pembayaran pasien kunjungan 
    public IActionResult Index()
    {
        List<IgdViewModel> list_status_bayar = new List<IgdViewModel>();
        list_status_bayar = obj_kasir.GetDataKunjunganKasir().ToList();
        return View(list_status_bayar);
    }

    // Mengambil data pasien untuk melakukan pembayaran berdasarkan id kunjungan
    [HttpGet]
    public IActionResult Pembayaran(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        IgdViewModel data_pembayaran = obj_kasir.GetKunjunganId(id);
        if (data_pembayaran == null)
        {
            return NotFound();
        }
        return View(data_pembayaran);
    }
    // Mengupdate status_bayar pasien 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Pembayaran(int id, [Bind] IgdViewModel igd)
    {
        if (id != igd.id_kunjungan)
        {
            return NotFound();
        }
        obj_kasir.UpdateStatusBayar(igd);
        return RedirectToAction("Index", "IGD");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
