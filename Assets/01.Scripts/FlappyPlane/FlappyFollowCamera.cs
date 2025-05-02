using UnityEngine;

// Flappy - Main Camera 연결
public class FlappyFollowCamera : MonoBehaviour
{
    // player 위치로 메인 카메라가 따라가게 하기 위해 선언
    public Transform playerTransform;
    float offsetX;

    void Start()
    {
        if (playerTransform == null) return;
        offsetX = transform.position.x - playerTransform.position.x;
    }

    void Update()
    {
        if (playerTransform == null) return;

        Vector3 pos = transform.position;
        pos.x = playerTransform.position.x + offsetX;
        transform.position = pos;
    }
}
