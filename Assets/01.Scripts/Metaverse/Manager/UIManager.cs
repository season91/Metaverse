using UnityEngine;

// Metaverse - Canvas ����
// UI ����
public enum UIState
{
    Home,
    Score,
    Popup,
    Wave
}
public class UIManager : MonoBehaviour
{
    ScoreUI scoreUI;
    PopupUI popupUI;

    private UIState currentState;

    private void Awake()
    {
        scoreUI = GetComponentInChildren<ScoreUI>(true);
        scoreUI.Init(this);

        popupUI = GetComponentInChildren<PopupUI>(true);
        popupUI.Init(this);
    }

    // ���� UI ���¸� �����ϰ�, �� UI ������Ʈ�� ���¸� ����
    public void ChangeState(UIState state)
    {
        currentState = state;

        scoreUI.SetActive(currentState);
        popupUI.SetActive(currentState);
    }

    // Score UI�� ���
    public void SetScoreUI()
    {
        scoreUI.SetScoreText(GameManager.Instance.Score, GameManager.Instance.BestScore);
    }

}
