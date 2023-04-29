using UnityEngine;
using TMPro;

public class UI_PesanLevel : MonoBehaviour
{
    // Variable
    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private GameObject _opsiMenang = null;

    [SerializeField]
    private GameObject _opsiKalah = null;

    [SerializeField]
    private TextMeshProUGUI _tempatPesan = null;

    // Method
    public string Pesan
    {
        get => _tempatPesan.text;
        set => _tempatPesan.text = value;
    }

    private void Awake()
    {
        // Untuk mematikan halaman pesan level
        if (gameObject.activeSelf)
            gameObject.SetActive(false);

        // Subscribe event
        UI_Timer.EventWaktuHabis += UI_Timer_EventWaktuHabis;
        UI_PoinJawaban.EventJawabSoal += UI_PoinJawaban_EventJawabSoal;
    }

    private void OnDestroy()
    {
        // Unsubscribe event
        UI_Timer.EventWaktuHabis -= UI_Timer_EventWaktuHabis;
        UI_PoinJawaban.EventJawabSoal -= UI_PoinJawaban_EventJawabSoal;
    }

    private void UI_PoinJawaban_EventJawabSoal(string jawabanTeks, bool adalahBenar)
    {
        Pesan = $"Jawaban anda {adalahBenar} (Jawab: {jawabanTeks})";
        gameObject.SetActive(true);

        if (adalahBenar)
        {
            _opsiMenang.SetActive(true);
            _opsiKalah.SetActive(false);
        }
        else
        {
            _opsiMenang.SetActive(false);
            _opsiKalah.SetActive(true);
        }

        _animator.SetBool("Menang", adalahBenar);
    }

    private void UI_Timer_EventWaktuHabis()
    {
        Pesan = "Waktu Sudah Habis !";
        gameObject.SetActive(true);

        _opsiMenang.SetActive(false);
        _opsiKalah.SetActive(true);
    }
}
