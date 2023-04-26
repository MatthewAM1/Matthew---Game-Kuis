using UnityEngine;
using TMPro;

public class LevelMenuDataManager : MonoBehaviour
{
    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private TextMeshProUGUI _tempatKoin = null;

    // Start is called before the first frame update
    void Start()
    {
        _tempatKoin.text = $"{_playerProgress.progressData.koin}";
    }
}
