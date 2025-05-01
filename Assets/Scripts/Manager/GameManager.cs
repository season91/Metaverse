using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �̱��� ó��
    public static GameManager instance;

    // Player �̵�, �ٶ󺸴� ó���� ���� ȣ��
    public PlayerController Player {  get; private set; }

    private void Awake()
    {
        instance = this;
        Player = FindObjectOfType<PlayerController>();
        Player.Init(this);
    }
}
