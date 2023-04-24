using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    // Variable
    [SerializeField]
    private UI_PesanLevel _tempatPesan = null;

    [SerializeField]
    private Slider _timeBar = null;

    [SerializeField]
    private float _waktuJawab = 30f;

    private float _sisaWaktu = 0f;
    private bool _waktuBerjalan = true;

    // Method
    public bool WaktuBerjalan
    {
        get => _waktuBerjalan;
        set => _waktuBerjalan = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        UlangWaktu();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_waktuBerjalan)
            return;

        _sisaWaktu -= Time.deltaTime;
        _timeBar.value = _sisaWaktu / _waktuJawab;

        if (_sisaWaktu <= 0f)
        {
            _tempatPesan.Pesan = "Waktu Sudah Habis !";
            _tempatPesan.gameObject.SetActive(true);
            //Debug.Log("Waktu Habis");
            _waktuBerjalan = false;
            return;
        }

       //Debug.Log(_sisaWaktu);
    }

    public void UlangWaktu()
    {
        _sisaWaktu = _waktuJawab;
    }
}
