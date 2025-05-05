using UnityEngine;

// UI Canvas 추상 클래스
public abstract class BaseUI : MonoBehaviour
{
    protected UIManager uiManager;

    public virtual void Init(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    protected abstract UIState GetUIState();
    public void SetActive(UIState state)
    {
        // 비교해서 동작
        this.gameObject.SetActive(GetUIState() == state);
    }
}
