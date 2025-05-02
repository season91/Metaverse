using UnityEngine;

// Metaverse - Canvas ����
// UI ����
public enum UIState
{
    Home,
    Popup,
}
public class UIManager : MonoBehaviour
{
    PopupUI popupUI;

    private UIState currentState;

    private void Awake()
    {
        popupUI = GetComponentInChildren<PopupUI>(true);
        popupUI.Init(this);
    }

    // ���� UI ���¸� �����ϰ�, �� UI ������Ʈ�� ���¸� ����
    public void ChangeState(UIState state)
    {
        currentState = state; //�ֽ�ȭ
    }

}
