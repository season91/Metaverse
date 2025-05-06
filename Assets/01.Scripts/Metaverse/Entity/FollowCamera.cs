using UnityEngine;
using UnityEngine.SceneManagement;

// Metaverse - Main Camera �� ����
public class FollowCamera : MonoBehaviour
{
    // player ��ġ�� ���� ī�޶� ���󰡰� �ϱ� ���� ����
    public Transform playerTransform;
    public float smoothSpeed = 5f; // �ε巯�� �̵� �ӵ�
    private Vector3 offset; // �ʱ� �Ÿ�

    void Start()
    {
        if (playerTransform == null) playerTransform = GameObject.FindWithTag("Player").transform;
        offset = transform.position - playerTransform.position;
    }

    // �̵��� ���� �� ����
    void LateUpdate()
    {
        if (playerTransform == null) return;

        Vector3 pos = playerTransform.position + offset;
        pos.z = transform.position.z;

        // �ε巯�� �̵� ó��
        transform.position = Vector3.Lerp(transform.position, pos, smoothSpeed * Time.deltaTime);
    }
}
