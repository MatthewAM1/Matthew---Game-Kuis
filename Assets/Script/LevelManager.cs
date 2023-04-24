using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private LevelPackKuis _soalSoal = null;

    [SerializeField]
    private UI_Pertanyaan _tempatPertanyaan = null;

    [SerializeField]
    private UI_PoinJawaban[] _tempatPilihanJawaban = new UI_PoinJawaban[0];

    private int _indexSoal = -1;

    private void Start()
    {
        if (!_playerProgress.MuatProgress())
        {
            _playerProgress.SimpanProgress();
        }

        NextLevel();
    }

    public void NextLevel()
    {
        // Soal index selanjutnya
        _indexSoal++;

        // Jika index melampaui soal terakhir, ulang dari awal
        if(_indexSoal >= _soalSoal.banyakLevel)
        {
            _indexSoal = 0;
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
