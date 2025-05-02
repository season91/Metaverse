using UnityEngine;

// UI Canvas �߻� Ŭ����
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
        // ���ؼ� ����
        this.gameObject.SetActive(GetUIState() == state);
    }
}
