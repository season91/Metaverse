using UnityEngine;
using UnityEngine.UI;

public class PopupUI : BaseUI
{
    [SerializeField] private Button startButton;

    public override void Init(UIManager uiManager)
    {
        // BaseUI에서 UIManager 저장
        base.Init(uiManager);

        // 버튼 클릭 이벤트 연결
        startButton.onClick.AddListener(OnClickStartButton);
    }

    // 게임 스타트
    public void OnClickStartButton()
    {
        // GameManager 통해 게임 시작
        GameManager.Instance.StartMiniGame();
    }

    // UI 상태 변경
    protected override UIState GetUIState()
    {
        return UIState.Popup;
    }

}
