using System.ComponentModel.DataAnnotations;


namespace sistem_informasi_rs.Models;


// Pembuatan Variabel Pasien
// Setter dan Getter untuk diolah oelh Controller,View dan Pasien Data Access
public class PasienViewModel
{
    public int? id_pasien { get; set; }

    [Required(ErrorMessage = "Nik Tidak Boleh Kosong")]
    public int? nik_pasien { get; set; }

    [Required(ErrorMessage = "Nama Tidak Boleh Kosong")]
    public string? nama { get; set; }
    // [Required]
    public string? jk_pasien { get; set; }

    public DateTime? tl_pasien { get; set; }

    [Required(ErrorMessage = "Alamat Tidak Boleh Kosong")]
    public string? alamat_pasien { get; set; }
    public Boolean status_kunjungan { get; set; }

}


