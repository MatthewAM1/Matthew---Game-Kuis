using TMPro;
using UnityEngine;

public class UI_PoinJawaban : MonoBehaviour
{
    // Variable
    public static event System.Action<string, bool> EventJawabSoal;

    //[SerializeField]
    //private UI_PesanLevel _tempatPesan = null;

    [SerializeField]
    private TextMeshProUGUI _teksJawaban = null;

    [SerializeField]
    private bool _adalahBenar = false;

    public void PilihJawaban()
    {
        //Debug.Log($"{_teksJawaban.text} Adalah {_adalahBenar}");
        //_tempatPesan.Pesan = $"{_teksJawaban.text} Adalah {_adalahBenar}";
        EventJawabSoal?.Invoke(_teksJawaban.text, _adalahBenar);
    }

    public void SetJawaban(string teksJawaban, bool adalahBenar)
    {
        _teksJawaban.text = teksJawaban;
        _adalahBenar = adalahBenar;
    }
}
