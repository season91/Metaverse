using UnityEngine;

// Metaverse - Canvas 연결
// UI 상태
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

    // 현재 UI 상태를 변경하고, 각 UI 오브젝트에 상태를 전달
    public void ChangeState(UIState state)
    {
        currentState = state;

        scoreUI.SetActive(currentState);
        popupUI.SetActive(currentState);
    }

    // Score UI에 출력
    public void SetScoreUI()
    {
        scoreUI.SetScoreText(GameManager.Instance.Score, GameManager.Instance.BestScore);
    }

}
