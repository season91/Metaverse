using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    public override void Init(UIManager uiManager)
    {
        // BaseUI에서 UIManager 저장
        base.Init(uiManager);

        // 버튼 클릭 이벤트 연결
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    // 게임 스타트
    public void OnClickStartButton()
    {
        // GameManager 통해 게임 시작
    }

    // 게임 종료
    public void OnClickExitButton()
    {
        Application.Quit();
    }

    // UI 상태 변경
    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

}
