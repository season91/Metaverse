using UnityEngine;

// Flappy - Main Camera ����
public class FlappyFollowCamera : MonoBehaviour
{
    // player ��ġ�� ���� ī�޶� ���󰡰� �ϱ� ���� ����
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
