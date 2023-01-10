
using System.Data;
using MySql.Data.MySqlClient;

namespace simrs.Models
{
    public class KasirDataAccess
    {
        string connectionString = "server=localhost; userid=root; password=; database=simrs; SslMode=None";

        // Memanggil data pasien dan dokter 
        // Data selanjutnya diolah oleh KasirController                
        public IEnumerable<IgdViewModel> GetDataKunjunganKasir()
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

        // Mengambil data pasien, dokter serta kunjungan  
        public IgdViewModel GetKunjunganId(int? id)
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
                    data_kunjungan.nama = rdr["nama"].ToString();
                    data_kunjungan.nama_dokter = rdr["nama_dokter"].ToString();
                    data_kunjungan.tanggal_daftar = (DateTime?)rdr["tanggal_daftar"];
                    data_kunjungan.tarif = Convert.ToInt32(rdr["tarif"]);
                }
            }
            return data_kunjungan;
        }

        // Update status_bayar pasien berdasarkan id kunjungan
        public void UpdateStatusBayar(IgdViewModel Igd)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE kunjungan SET status_bayar=@status_bayar WHERE id_kunjungan=@id_kunjungan", con);
                cmd.Parameters.AddWithValue("@status_bayar", true);
                cmd.Parameters.AddWithValue("@id_kunjungan", Igd.id_kunjungan);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}