-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 31 Des 2022 pada 11.57
-- Versi server: 10.4.18-MariaDB
-- Versi PHP: 7.4.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `simrs`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `dokter`
--

CREATE TABLE `dokter` (
  `id_dokter` int(11) NOT NULL,
  `nik` int(20) NOT NULL,
  `nama_dokter` varchar(127) NOT NULL,
  `jenis_kelamin` varchar(127) NOT NULL,
  `tanggal_lahir` datetime NOT NULL DEFAULT current_timestamp(),
  `alamat` varchar(127) NOT NULL,
  `spesialis` varchar(127) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `dokter`
--

INSERT INTO `dokter` (`id_dokter`, `nik`, `nama_dokter`, `jenis_kelamin`, `tanggal_lahir`, `alamat`, `spesialis`, `status`) VALUES
(1, 234, 'Abdus Syukur, Prof. dr, SpB-KBD', 'Laki-laki', '2022-12-14 00:00:00', 'Jombang Selatan', 'Bedah Digestif', 0),
(2, 2010, 'Anggraini Dwi S, Dr, dr, SpRad', 'Peremepuan', '2022-12-08 00:00:00', 'Rungkut Kidul 7 no 88', 'Radiologi', 0),
(3, 46464646, 'Hadi Hartono. dr. SpJP. FIHA', 'Laki-laki', '2022-12-05 00:00:00', 'Bulak Banteng 3 no 55', 'Kardiologi', 0),
(4, 8181818, 'Made Putra Sedhana, dr. SpPD-KHOM', 'Laki-laki', '2022-12-06 00:00:00', 'Kalijudan indah 4 no 33', 'Interna', 0),
(20, 99090, 'SUTARMI NARKO . DR, dr. JP. FIHAP', 'Laki-laki', '2013-05-13 00:00:00', 'GONDANG LEGI WETAN ', 'Bedah Digestif', 0),
(21, 989776, 'Sofyan Ghani Syarifudin', 'Laki-laki', '2022-12-10 00:00:00', 'Kedung Rejo Selatan', 'Radiologi', 0);

-- --------------------------------------------------------

--
-- Struktur dari tabel `kunjungan`
--

CREATE TABLE `kunjungan` (
  `id_kunjungan` int(11) NOT NULL,
  `id_pasien_fk` int(20) NOT NULL,
  `id_dokter_fk` int(20) NOT NULL,
  `tanggal_daftar` datetime NOT NULL DEFAULT current_timestamp(),
  `tarif` int(20) NOT NULL,
  `tanggal_pulang` datetime NOT NULL DEFAULT current_timestamp(),
  `status_kunjung` tinyint(1) NOT NULL,
  `status_bayar` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `kunjungan`
--

INSERT INTO `kunjungan` (`id_kunjungan`, `id_pasien_fk`, `id_dokter_fk`, `tanggal_daftar`, `tarif`, `tanggal_pulang`, `status_kunjung`, `status_bayar`) VALUES
(1, 1, 3, '2022-12-28 00:09:38', 100000, '2022-12-28 02:14:38', 1, 1),
(2, 2, 3, '2022-12-28 00:22:32', 120000, '2022-12-28 02:15:10', 1, 1),
(3, 3, 2, '2022-12-28 20:56:56', 200000, '2022-12-28 21:59:11', 1, 1),
(6, 5, 2, '2022-12-28 21:20:34', 900000, '0000-00-00 00:00:00', 0, 0),
(7, 6, 3, '2022-12-28 23:07:29', 700000, '0000-00-00 00:00:00', 0, 1),
(8, 2, 2, '2022-12-28 23:59:36', 900000, '0000-00-00 00:00:00', 0, 0),
(9, 7, 20, '2022-12-29 22:09:53', 400000, '2022-12-29 22:11:22', 1, 1),
(10, 8, 4, '2022-12-30 22:01:32', 870000, '0000-00-00 00:00:00', 0, 0);

-- --------------------------------------------------------

--
-- Struktur dari tabel `pasien`
--

CREATE TABLE `pasien` (
  `id_pasien` int(11) NOT NULL,
  `nik_pasien` int(20) NOT NULL,
  `nama` varchar(127) NOT NULL,
  `jk_pasien` varchar(127) NOT NULL,
  `tl_pasien` datetime NOT NULL DEFAULT current_timestamp(),
  `alamat_pasien` varchar(127) NOT NULL,
  `status_kunjungan` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `pasien`
--

INSERT INTO `pasien` (`id_pasien`, `nik_pasien`, `nama`, `jk_pasien`, `tl_pasien`, `alamat_pasien`, `status_kunjungan`) VALUES
(1, 1212, 'Yuda Yudi', 'Laki-laki', '2022-12-23 00:00:00', 'Gumuk Porong 4 no 226', 0),
(2, 4343, 'Zainal Vindamar', 'Laki-laki', '2022-12-08 00:00:00', 'Semampir Tengah 3 no 42', 1),
(3, 121212, 'Bagio Sutarmaji', 'Laki-laki', '2022-12-11 00:00:00', 'Kedung Mangu 3 no 22', 0),
(5, 808080, 'Wahyu Fakrozi', 'Laki-laki', '2022-12-15 00:00:00', 'Gumuk Porong 4 no 226', 1),
(6, 887879, 'Juminten', 'Perempuan', '2022-12-01 00:00:00', 'Bangkalan', 1),
(7, 201842, 'Renaldi Dafikri', 'Laki-laki', '2022-12-15 00:00:00', 'Dlanggu Wetan 4', 0),
(8, 878787, 'Mustofa Sadikin', 'Laki-laki', '2022-12-06 00:00:00', 'Cerme Kidul 3', 1);

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `dokter`
--
ALTER TABLE `dokter`
  ADD PRIMARY KEY (`id_dokter`);

--
-- Indeks untuk tabel `kunjungan`
--
ALTER TABLE `kunjungan`
  ADD PRIMARY KEY (`id_kunjungan`),
  ADD KEY `fk_id_pasien` (`id_pasien_fk`),
  ADD KEY `fk_id_dokter` (`id_dokter_fk`);

--
-- Indeks untuk tabel `pasien`
--
ALTER TABLE `pasien`
  ADD PRIMARY KEY (`id_pasien`);

--
-- AUTO_INCREMENT untuk tabel yang dibuang
--

--
-- AUTO_INCREMENT untuk tabel `dokter`
--
ALTER TABLE `dokter`
  MODIFY `id_dokter` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- AUTO_INCREMENT untuk tabel `kunjungan`
--
ALTER TABLE `kunjungan`
  MODIFY `id_kunjungan` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT untuk tabel `pasien`
--
ALTER TABLE `pasien`
  MODIFY `id_pasien` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Ketidakleluasaan untuk tabel pelimpahan (Dumped Tables)
--

--
-- Ketidakleluasaan untuk tabel `kunjungan`
--
ALTER TABLE `kunjungan`
  ADD CONSTRAINT `fk_id_dokter` FOREIGN KEY (`id_dokter_fk`) REFERENCES `dokter` (`id_dokter`),
  ADD CONSTRAINT `fk_id_pasien` FOREIGN KEY (`id_pasien_fk`) REFERENCES `pasien` (`id_pasien`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
