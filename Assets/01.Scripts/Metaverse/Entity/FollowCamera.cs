using UnityEngine;
using UnityEngine.SceneManagement;

// Metaverse - Main Camera 에 연결
public class FollowCamera : MonoBehaviour
{
    // player 위치로 메인 카메라가 따라가게 하기 위해 선언
    public Transform playerTransform;
    public float smoothSpeed = 5f; // 부드러운 이동 속도
    private Vector3 offset; // 초기 거리

    void Start()
    {
        if (playerTransform == null) playerTransform = GameObject.FindWithTag("Player").transform;
        offset = transform.position - playerTransform.position;
    }

    // 이동이 끝난 후 실행
    void LateUpdate()
    {
        if (playerTransform == null) return;

        Vector3 pos = playerTransform.position + offset;
        pos.z = transform.position.z;

        // 부드러운 이동 처리
        transform.position = Vector3.Lerp(transform.position, pos, smoothSpeed * Time.deltaTime);
    }
}
