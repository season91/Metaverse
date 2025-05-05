using TMPro;
using UnityEngine;

public class ScoreUI : BaseUI
{
    // 출력할 Text 선언
    [SerializeField] private TextMeshProUGUI ScorePonintText;
    [SerializeField] private TextMeshProUGUI bestScorePonitText;
    public override void Init(UIManager uiManager)
    {
        // BaseUI에서 UIManager 저장
        base.Init(uiManager);
    }
    
    // Score 출력 함수
    public void SetScoreText(int score, int bestScore)
    {
        ScorePonintText.text = score.ToString();
        bestScorePonitText.text = bestScore.ToString();
    }

    // UI 상태 변경
    protected override UIState GetUIState()
    {
        return UIState.Score;
    }
}
