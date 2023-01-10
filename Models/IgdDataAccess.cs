
using System.Data;
using MySql.Data.MySqlClient;

namespace simrs.Models
{
    public class IgdDataAccess
    {
        //  Pembuatan Koneksi dengan Database
        string connectionString = "server=localhost; userid=root; password=; database=simrs; SslMode=None";

        // Memanggil data pasien dan dokter 
        // Data selanjutnya diolah oleh IGDController           
        public IEnumerable<IgdViewModel> GetDataKunjungan()
        {
            List<IgdViewModel> lstIgdViewModel = new List<IgdViewModel>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM kunjungan INNER JOIN pasien ON kunjungan.id_pasien_fk=pasien.id_pasien INNER JOIN dokter ON kunjungan.id_dokter_fk=dokter.id_dokter", con);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    IgdViewModel IgdwModels = new IgdViewModel();
                    IgdwModels.id_kunjungan = Convert.ToInt32(rdr["id_kunjungan"]);
                    IgdwModels.id_pasien = Convert.ToInt32(rdr["id_pasien"]);
                    IgdwModels.nama = rdr["nama"].ToString();
                    IgdwModels.tanggal_daftar = ((DateTime)rdr["tanggal_daftar"]);
                    IgdwModels.nama_dokter = rdr["nama_dokter"].ToString();
                    IgdwModels.status_kunjung = Convert.ToBoolean(rdr["status_kunjung"]);
                    IgdwModels.status_bayar = Convert.ToBoolean(rdr["status_bayar"]);
                    lstIgdViewModel.Add(IgdwModels);
                }
                con.Close();
            }
            return lstIgdViewModel;
        }
        // Mengambil data pasien berdasarkan id kunjungan
        public IgdViewModel GetKunjunganIdPulang(int? id)
        {
            IgdViewModel data_kunjungan = new IgdViewModel();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM kunjungan INNER JOIN pasien ON kunjungan.id_pasien_fk=pasien.id_pasien INNER JOIN dokter ON kunjungan.id_dokter_fk=dokter.id_dokter WHERE id_kunjungan=" + id;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    data_kunjungan.id_kunjungan = Convert.ToInt32(rdr["id_kunjungan"]);
                    data_kunjungan.id_pasien = Convert.ToInt32(rdr["id_pasien"]);
                    data_kunjungan.nama = rdr["nama"].ToString();
                    data_kunjungan.nama_dokter = rdr["nama_dokter"].ToString();
                    data_kunjungan.tanggal_daftar = (DateTime?)rdr["tanggal_daftar"];
                    data_kunjungan.tarif = Convert.ToInt32(rdr["tarif"]);
                    data_kunjungan.status_kunjungan = Convert.ToBoolean(rdr["status_kunjungan"]);
                }
            }
            return data_kunjungan;
        }

        // Query untuk memanggil pasien yang baru daftar
        public IgdViewModel GetPasienToIGD()
        {
            IgdViewModel Pasien = new IgdViewModel();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM pasien ORDER BY id_pasien DESC LIMIT 1";
                // string sqlQuery = "SELECT * FROM pasien WHERE nik= " + nik;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Pasien.id_pasien = Convert.ToInt32(rdr["id_pasien"]);
                    Pasien.nama = rdr["nama"].ToString();
                }
            }
            return Pasien;
        }

        // Memanggil data pasien yang sudah pernah daftar/kunjungan
        public IgdViewModel GetPasienLamaToIGD(int? id)
        {
            IgdViewModel Pasien = new IgdViewModel();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM pasien WHERE nik_pasien= " + id;
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                con.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Pasien.id_pasien = Convert.ToInt32(rdr["id_pasien"]);
                    Pasien.nama = rdr["nama"].ToString();
                }
            }
            return Pasien;
        }

        // insert database kunjungan 
        public void AddIgdPasien(IgdViewModel igd)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("INSERT INTO kunjungan VALUES('', @id_pasien_fk, @id_dokter_fk,  @tanggal_daftar, @tarif, '', '', '')", con);
                cmd.Parameters.AddWithValue("@id_pasien_fk", igd.id_pasien);
                cmd.Parameters.AddWithValue("@id_dokter_fk", igd.id_dokter);
                cmd.Parameters.AddWithValue("@tanggal_daftar", DateTime.Now);
                cmd.Parameters.AddWithValue("@tarif", igd.tarif);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        // Update status_kunjungan pasien menjadi true
        public void UpdateStatusPasien(IgdViewModel Igd)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE pasien SET status_kunjungan=@status_kunjungan WHERE id_pasien=@id_pasien", con);
                cmd.Parameters.AddWithValue("@status_kunjungan", true);
                cmd.Parameters.AddWithValue("@id_pasien", Igd.id_pasien);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        // Update status_kunjung pasien di tabel kunjungan 
        public void UpdateStatPulangKunjung(IgdViewModel Igd)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE kunjungan SET status_kunjung=@status_kunjung, tanggal_pulang=@tanggal_pulang WHERE id_kunjungan=@id_kunjungan", con);
                cmd.Parameters.AddWithValue("@status_kunjung", true);
                cmd.Parameters.AddWithValue("@id_kunjungan", Igd.id_kunjungan);
                cmd.Parameters.AddWithValue("@tanggal_pulang", DateTime.Now);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        // Update status_kunjungan pasien di master pasien
        public void UpdateStatPulangPasien(IgdViewModel Igd)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE pasien SET status_kunjungan=@status_kunjungan WHERE id_pasien=@id_pasien", con);
                cmd.Parameters.AddWithValue("@status_kunjungan", false);
                cmd.Parameters.AddWithValue("@id_pasien", Igd.id_pasien);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}