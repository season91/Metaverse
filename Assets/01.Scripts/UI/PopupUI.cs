using UnityEngine;
using UnityEngine.UI;

public class PopupUI : BaseUI
{
    [SerializeField] private Button startButton;

    public override void Init(UIManager uiManager)
    {
        // BaseUI���� UIManager ����
        base.Init(uiManager);

        // ��ư Ŭ�� �̺�Ʈ ����
        startButton.onClick.AddListener(OnClickStartButton);
    }

    // ���� ��ŸƮ
    public void OnClickStartButton()
    {
        // GameManager ���� ���� ����
        GameManager.Instance.StartMiniGame();
    }

    // UI ���� ����
    protected override UIState GetUIState()
    {
        return UIState.Popup;
    }

}
