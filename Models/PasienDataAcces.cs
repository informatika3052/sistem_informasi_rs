using System.Data;
using MySql.Data.MySqlClient;

namespace simrs.Models
{
    public class PasienDataAccess
    {
        //  Pembuatan Koneksi dengan Database
        string connectionString = "server=localhost; userid=root; password=; database=simrs; SslMode=None";

        // Memanggil semua data pasien
        // Data selanjutnya diolah oleh PasienController 
        public IEnumerable<PasienViewModel> GetAllPasiens()
        {
            List<PasienViewModel> lstPasienViewModel = new List<PasienViewModel>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("select * from pasien", con);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    PasienViewModel PasienModel = new PasienViewModel();
                    PasienModel.nik_pasien = Convert.ToInt32(rdr["nik_pasien"]);
                    PasienModel.nama = rdr["nama"].ToString();
                    PasienModel.jk_pasien = rdr["jk_pasien"].ToString();
                    var dateAndTime = ((DateTime)rdr["tl_pasien"]);
                    var date = dateAndTime.Date;
                    PasienModel.tl_pasien = date;
                    PasienModel.alamat_pasien = rdr["alamat_pasien"].ToString();
                    lstPasienViewModel.Add(PasienModel);
                }
                con.Close();
            }
            return lstPasienViewModel;
        }

        // Insert data pasien ke database
        // Data didapatkan dari View Create          
        public void AddPasien(PasienViewModel Pasien)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO pasien VALUES('', @nik_pasien, @nama, @jk_pasien, @tl_pasien, @alamat_pasien, @status_kunjungan)", con);
                cmd.Parameters.AddWithValue("@nik_pasien", Pasien.nik_pasien);
                cmd.Parameters.AddWithValue("@nama", Pasien.nama);
                cmd.Parameters.AddWithValue("@jk_pasien", Pasien.jk_pasien);
                cmd.Parameters.AddWithValue("@tl_pasien", Pasien.tl_pasien);
                cmd.Parameters.AddWithValue("@alamat_pasien", Pasien.alamat_pasien);
                cmd.Parameters.AddWithValue("@status_kunjungan", Pasien.status_kunjungan);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Update data pasien ke database
        // Data didapatkan dari View Create   
        public void UpdatePasien(PasienViewModel Pasien)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE pasien SET nama=@nama ,jk_pasien=@jk_pasien, tl_pasien=@tl_pasien,alamat_pasien=@alamat_pasien WHERE nik_pasien=@nik_pasien", con);
                // cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nik_pasien", Pasien.nik_pasien);
                cmd.Parameters.AddWithValue("@nama", Pasien.nama);
                cmd.Parameters.AddWithValue("@jk_pasien", Pasien.jk_pasien);
                cmd.Parameters.AddWithValue("@tl_pasien", Pasien.tl_pasien);
                cmd.Parameters.AddWithValue("@alamat_pasien", Pasien.alamat_pasien);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        // Mengambil data pasien berdasrkan nik pasien
        // Data ditampilkan ke View Edit          
        public PasienViewModel GetPasienData(int? nik_pasien)
        {
            PasienViewModel Pasien = new PasienViewModel();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM pasien WHERE nik_pasien= " + nik_pasien;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Pasien.nik_pasien = Convert.ToInt32(rdr["nik_pasien"]);
                    Pasien.nama = rdr["nama"].ToString();
                    Pasien.jk_pasien = rdr["jk_pasien"].ToString();
                    var dateAndTime = ((DateTime)rdr["tl_pasien"]);
                    var date = dateAndTime.Date;
                    Pasien.tl_pasien = date;
                    Pasien.alamat_pasien = rdr["alamat_pasien"].ToString();
                }
            }
            return Pasien;
        }

        // Pengambilan data seluruh pasien beserta status nya
        // Data selanjutnya ditampilkan ke view PasienLama di folder IGD
        public IEnumerable<PasienViewModel> PasienLama()
        {
            List<PasienViewModel> lstPasienViewModel = new List<PasienViewModel>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("select * from pasien", con);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    PasienViewModel PasienModel = new PasienViewModel();
                    PasienModel.nik_pasien = Convert.ToInt32(rdr["nik_pasien"]);
                    PasienModel.nama = rdr["nama"].ToString();
                    PasienModel.jk_pasien = rdr["jk_pasien"].ToString();
                    var dateAndTime = ((DateTime)rdr["tl_pasien"]);
                    var date = dateAndTime.Date;
                    PasienModel.tl_pasien = date;
                    PasienModel.alamat_pasien = rdr["alamat_pasien"].ToString();
                    PasienModel.status_kunjungan = Convert.ToBoolean(rdr["status_kunjungan"]);
                    lstPasienViewModel.Add(PasienModel);
                }
                con.Close();
            }
            return lstPasienViewModel;
        }
    }
}