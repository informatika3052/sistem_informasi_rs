using System.ComponentModel.DataAnnotations;



namespace simrs.Models;



// Pembuatan Variabel Dokter
// Setter dan Getter untuk diolah oelh Controller, View dan Dokter Data Access

public class DokterViewModel
{
    public int? id_dokter { get; set; }
    [Required(ErrorMessage = "Nik Tidak Boleh Kosong")]
    public int? nik { get; set; }

    [Required(ErrorMessage = "Nama Tidak Boleh Kosong")]
    public string? nama_dokter { get; set; }

    [Required(ErrorMessage = "Jenis Kelamin Tidak Boleh Kosong")]
    public string? jenis_kelamin { get; set; }

    // [Required(ErrorMessage = "Tanggal Lahir Tidak Boleh Kosong")]
    public DateTime? tanggal_lahir { get; set; }

    [Required(ErrorMessage = "Alamat Tidak Boleh Kosong")]
    public string? alamat { get; set; }
    public string? status { get; set; }
    [Required(ErrorMessage = "Spesialis Tidak Boleh Kosong")]
    public string? spesialis { get; set; }

}


