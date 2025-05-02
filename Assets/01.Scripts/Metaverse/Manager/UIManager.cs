using UnityEngine;

// Metaverse - Canvas 연결
// UI 상태
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

    // 현재 UI 상태를 변경하고, 각 UI 오브젝트에 상태를 전달
    public void ChangeState(UIState state)
    {
        currentState = state; //최신화
    }

}
