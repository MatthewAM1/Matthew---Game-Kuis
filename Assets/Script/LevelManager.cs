using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private LevelPackKuis _soalSoal = null;

    [SerializeField]
    private UI_Pertanyaan _tempatPertanyaan = null;

    [SerializeField]
    private UI_PoinJawaban[] _tempatPilihanJawaban = new UI_PoinJawaban[0];

    [SerializeField]
    private GameSceneManager _gameSceneManager = null;

    [SerializeField]
    private string _namaScenePilihMenu = string.Empty;

    [SerializeField]
    private PemanggilSuara _pemanggilSuara = null;

    [SerializeField]
    private AudioClip _suaraMenang = null;

    [SerializeField]
    private AudioClip _suaraKalah = null;

    private int _indexSoal = -1;

    private void Start()
    {
        _soalSoal = _inisialData.levelPack;
        _indexSoal = _inisialData.levelIndex - 1;

        NextLevel();
        AudioManager.instance.PlayBgm(1);

        // Subscribe events
        UI_PoinJawaban.EventJawabSoal += UI_PoinJawaban_EventJawabSoal;
    }

    private void OnDestroy()
    {
        // Unsubscribe events
        UI_PoinJawaban.EventJawabSoal -= UI_PoinJawaban_EventJawabSoal;
    }

    private void OnApplicationQuit()
    {
        _inisialData.SaatKalah = false;
    }

    private void UI_PoinJawaban_EventJawabSoal(string jawaban, bool adalahBenar)
    {
        _pemanggilSuara.PanggilSuara(adalahBenar ? _suaraMenang : _suaraKalah);

        // Cek jika tidak benar, maka abaikan prosedur
        if (!adalahBenar) return;

        var namaLevelPack = _inisialData.levelPack.name;
        var levelTerakhir = _playerProgress.progressData.progressLevel[namaLevelPack];

        // Cek apabila level terakhir kali main telah diselesaikan
        if (_indexSoal + 2 > levelTerakhir)
        {
            // Tambahkan koin sebagai hadiah dari menyelesaikan soal kuis
            _playerProgress.progressData.koin += 20;

            // Membuka level selanjutnya agar dapat diakses di menu level
            _playerProgress.progressData.progressLevel[namaLevelPack] = _indexSoal + 2;

            _playerProgress.SimpanProgress();
        }
    }

    public void NextLevel()
    {
        // Soal index selanjutnya
        _indexSoal++;

        // Jika index melampaui soal terakhir, ulang dari awal
        if(_indexSoal >= _soalSoal.banyakLevel)
        {
            //_indexSoal = 0;
            _gameSceneManager.BukaScene(_namaScenePilihMenu);
            return;
        }

        // Ambil data pertanyaan
        LevelSoalKuis soal = _soalSoal.AmbilLevelKe(_indexSoal);

        // Set informasi pertanyaan
        _tempatPertanyaan.SetPertanyaan($"Soal {_indexSoal + 1}" ,soal.pertanyaan, soal.petunjukJawaban);

        for (int i = 0; i < _tempatPilihanJawaban.Length; i++)
        {
            UI_PoinJawaban poin = _tempatPilihanJawaban[i];
            LevelSoalKuis.OpsiJawaban opsi = soal.opsiJawaban[i];
            poin.SetJawaban(opsi.pilihanJawaban, opsi.adalahBenar);
        }
    }
}
