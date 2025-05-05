using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    public override void Init(UIManager uiManager)
    {
        // BaseUI���� UIManager ����
        base.Init(uiManager);

        // ��ư Ŭ�� �̺�Ʈ ����
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    // ���� ��ŸƮ
    public void OnClickStartButton()
    {
        // GameManager ���� ���� ����
    }

    // ���� ����
    public void OnClickExitButton()
    {
        Application.Quit();
    }

    // UI ���� ����
    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

}
