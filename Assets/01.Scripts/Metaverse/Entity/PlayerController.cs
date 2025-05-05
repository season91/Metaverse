using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ�ϱ� ���� ���� ī�޶� ����
    private Camera _camera; 
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        _camera = Camera.main;
    }

    // InputSystem ����
    private void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }

    private void OnLook(InputValue inputValue)
    {
        Vector2 mousePosition = inputValue.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }

    // ���� - ���콺 ��Ŭ��
    void OnFire(InputValue inputValue)
    {
        // Canvas�� ����� EventSystem�� ���� �����Ǹ鼭 �̺�Ʈ�ý����� UIŬ�� �ǵ���
        // �̺κп� �̺�Ʈ �ý����� UI�� ���콺�� �ö��������� ���� �ʰԲ�
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // Ű�� ���� ������ InputAction�� Any�� �ؼ�
        if (gameManager.isWaveGamePlaying)
            isAttacking = inputValue.isPressed;

    }
}
