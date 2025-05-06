using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Metaverse - Canvas 연결

public class UIManager : MonoBehaviour
{
    // Popup UI
    GameObject pressUI;

    // Score UI
    GameObject scoreUI;
    [SerializeField] private TextMeshProUGUI scorePointText;
    [SerializeField] private TextMeshProUGUI bestScorePonitText;
    [SerializeField] private TextMeshProUGUI bestKillPonitText;

    // Wave Game UI
    GameObject gameUI;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private Slider hpSlider;


    private void Awake()
    {
        pressUI = transform.Find("PressUI").gameObject;
        scoreUI = transform.Find("ScoreUI").gameObject;
        gameUI = transform.Find("GameUI").gameObject;
    }

    private void Start()
    {
        // 시작 시 체력 슬라이더를 가득 채움 (100%)
        UpdateHPSlider(1);

        // kill 수 초기화
        ChangeKill(0);

        // 씬 이동시 파괴방지
        DontDestroyOnLoad(this.gameObject);
    }

    // Score UI
    public void SetScoreUI(bool isEnable, int score, int bestScore)
    {
        UpdateScoreUI(score, bestScore);
        scoreUI.SetActive(isEnable);
    }
    public void UpdateScoreUI(int score, int bestScore)
    {
        scorePointText.text = score.ToString();
        bestScorePonitText.text = bestScore.ToString();
        bestKillPonitText.text = killText.text;
    }

    // Press UI 
    public void SetPressUI(Vector3 miniGamePosition, bool isEnter)
    {
        if(isEnter) pressUI.transform.position = miniGamePosition + new Vector3(0, 1f, 0);
        pressUI.SetActive(isEnter);
    }

    public void UpdateHPSlider(float percentage)
    {
        hpSlider.value = percentage;
    }

    // Wave Game UI
    public void SetGameUI(bool isEnable)
    {
        gameUI.SetActive(isEnable);
    }

    public void ChangeWave(int wave)
    {
        waveText.text = wave.ToString();
    }
    public void ChangeKill(int killCount)
    {
        killText.text = killCount.ToString();
    }

    public void ChangePlayerHP(float currentHP, float maxHP)
    {
        UpdateHPSlider(currentHP / maxHP);
    }
}
