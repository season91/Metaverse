using UnityEngine;

// Metaverse - Player, 각 Enemy 연결
public class StatHandler : MonoBehaviour
{
    [Range(1, 100)][SerializeField] private int health = 10;
    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 100); // Clamp로 0~100만 쓰게 제한
    }

    // Movment에서 속도 사용할 것
    [Range(1f, 20f)][SerializeField] private float speed = 5;
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 20);
    }
}
