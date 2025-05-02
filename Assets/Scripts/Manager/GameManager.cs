using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 처리
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // Player 이동, 바라보는 처리를 위한 호출
    public PlayerController Player {  get; private set; }

    private void Awake()
    {
        instance = this;
        Player = FindObjectOfType<PlayerController>();
        Player.Init(this);
    }
}
