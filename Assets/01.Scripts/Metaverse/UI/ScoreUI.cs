using TMPro;
using UnityEngine;

public class ScoreUI : BaseUI
{
    // ����� Text ����
    [SerializeField] private TextMeshProUGUI ScorePonintText;
    [SerializeField] private TextMeshProUGUI bestScorePonitText;
    public override void Init(UIManager uiManager)
    {
        // BaseUI���� UIManager ����
        base.Init(uiManager);
    }
    
    // Score ��� �Լ�
    public void SetScoreText(int score, int bestScore)
    {
        ScorePonintText.text = score.ToString();
        bestScorePonitText.text = bestScore.ToString();
    }

    // UI ���� ����
    protected override UIState GetUIState()
    {
        return UIState.Score;
    }
}
