using UnityEngine;
using TMPro;

public class UI_PesanLevel : MonoBehaviour
{
    // Variable
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
    }
}
