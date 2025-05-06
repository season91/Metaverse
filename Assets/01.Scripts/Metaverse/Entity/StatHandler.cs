using UnityEngine;

// Metaverse - Player, �� Enemy ����
public class StatHandler : MonoBehaviour
{
    [Range(1, 100)][SerializeField] private int health = 10;
    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 100); // Clamp�� 0~100�� ���� ����
    }

    // Movment���� �ӵ� ����� ��
    [Range(1f, 20f)][SerializeField] private float speed = 5;
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 20);
    }
}
