using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �̱��� ó��
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // Player �̵�, �ٶ󺸴� ó���� ���� ȣ��
    public PlayerController Player {  get; private set; }

    private void Awake()
    {
        instance = this;
        Player = FindObjectOfType<PlayerController>();
        Player.Init(this);
    }
}
