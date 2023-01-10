using System.ComponentModel.DataAnnotations;

namespace sistem_informasi_rs.Models;


// Pembuatan Variabel Pasien dan Dokter
// Setter dan Getter untuk diolah oelh Controller,View dan IGD Data Access
public class IgdViewModel
{

    public int id_kunjungan { get; set; }
    public int id_dokter { get; set; }
    public int id_pasien { get; set; }
    public string? nama { get; set; }
    public DateTime? tanggal_daftar { get; set; }
    public int tarif { get; set; }
    public DateTime tanggal_pulang { get; set; }
    public Boolean status_kunjung { get; set; }
    public Boolean status_bayar { get; set; }

    public Boolean status_kunjungan { get; set; }
    public int nik { get; set; }
    public string? nama_dokter { get; set; }
    public string? jenis_kelamin { get; set; }
    public string? status { get; set; }
    public string? spesialis { get; set; }

}


