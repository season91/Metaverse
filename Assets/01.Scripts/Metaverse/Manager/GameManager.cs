using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 싱글톤 처리
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // Player 이동, 바라보는 처리를 위한 호출
    public PlayerController Player {  get; private set; }

    // UI 전환을 위해 호출
    private UIManager uiManager;

    private void Awake()
    {
        instance = this;
        Player = FindObjectOfType<PlayerController>();
        Player.Init(this);

        uiManager = FindObjectOfType<UIManager>();
    }

    public void StartMiniGame()
    {
        SceneManager.LoadScene("FlappyPlane");
    }

}
