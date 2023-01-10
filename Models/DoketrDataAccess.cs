using System.Data;
using MySql.Data.MySqlClient;

namespace sistem_informasi_rs.Models
{
    public class DokterDataAccess
    {
        //  Pembuatan Koneksi dengan Database
        string connectionString = "server=localhost; userid=root; password=; database=simrs; SslMode=None";

        // Memanggil semua data Dokter
        // Data selanjutnya diolah oleh DokterController          
        public IEnumerable<DokterViewModel> GetAllDokter()
        {
            List<DokterViewModel> lstDokterViewModel = new List<DokterViewModel>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("select * from dokter", con);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DokterViewModel DokterModel = new DokterViewModel();
                    DokterModel.nik = Convert.ToInt32(rdr["nik"]);
                    DokterModel.id_dokter = Convert.ToInt32(rdr["id_dokter"]);
                    DokterModel.nama_dokter = rdr["nama_dokter"].ToString();
                    DokterModel.jenis_kelamin = rdr["jenis_kelamin"].ToString();
                    DokterModel.tanggal_lahir = ((DateTime)rdr["tanggal_lahir"]);
                    DokterModel.alamat = rdr["alamat"].ToString();
                    DokterModel.spesialis = rdr["spesialis"].ToString();
                    lstDokterViewModel.Add(DokterModel);
                }
                con.Close();
            }
            return lstDokterViewModel;
        }

        // Insert data dokter ke database
        // Data didapatkan dari View Create 
        public void AddDokter(DokterViewModel Dokter)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO dokter VALUES('', @nik, @nama_dokter, @jenis_kelamin, @tanggal_lahir, @alamat, @spesialis,'')", con);
                // cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nik", Dokter.nik);
                cmd.Parameters.AddWithValue("@nama_dokter", Dokter.nama_dokter);
                cmd.Parameters.AddWithValue("@jenis_kelamin", Dokter.jenis_kelamin);
                cmd.Parameters.AddWithValue("@tanggal_lahir", Dokter.tanggal_lahir);
                cmd.Parameters.AddWithValue("@alamat", Dokter.alamat);
                cmd.Parameters.AddWithValue("@spesialis", Dokter.spesialis);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        // Update data dokter ke database
        // Data didapatkan dari View Create   
        public void UpdateDokter(DokterViewModel Pasien)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE dokter SET nama_dokter=@nama_dokter ,jenis_kelamin=@jenis_kelamin, alamat=@alamat, spesialis=@spesialis WHERE nik=@nik", con);
                cmd.Parameters.AddWithValue("@nik", Pasien.nik);
                cmd.Parameters.AddWithValue("@nama_dokter", Pasien.nama_dokter);
                cmd.Parameters.AddWithValue("@jenis_kelamin", Pasien.jenis_kelamin);
                cmd.Parameters.AddWithValue("@tanggal_lahir", Pasien.tanggal_lahir);
                cmd.Parameters.AddWithValue("@alamat", Pasien.alamat);
                cmd.Parameters.AddWithValue("@spesialis", Pasien.spesialis);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Mengambil data dokter berdasrkan nik dokter
        // Data ditampilkan ke View Edit  
        public DokterViewModel GetDokterData(int? nik)
        {
            DokterViewModel data_dokter = new DokterViewModel();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM dokter WHERE nik= " + nik;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    data_dokter.nik = Convert.ToInt32(rdr["nik"]);
                    data_dokter.nama_dokter = rdr["nama_dokter"].ToString();
                    data_dokter.jenis_kelamin = rdr["jenis_kelamin"].ToString();
                    data_dokter.tanggal_lahir = (DateTime?)rdr["tanggal_lahir"];
                    data_dokter.alamat = rdr["alamat"].ToString();
                    data_dokter.spesialis = rdr["spesialis"].ToString();
                }
            }
            return data_dokter;
        }
        // Menghapus data dokter       
        public void DeleteDokter(int? nik)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("DELETE FROM dokter where nik=@nik", con);
                cmd.Parameters.AddWithValue("@nik", nik);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


    }
}