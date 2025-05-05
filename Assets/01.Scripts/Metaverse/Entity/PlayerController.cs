using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    // 마우스 위치를 월드 좌표로 변환하기 위한 메인 카메라 참조
    private Camera _camera; 
    private GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        _camera = Camera.main;
    }

    // InputSystem 적용
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

    // 공격 - 마우스 좌클릭
    void OnFire(InputValue inputValue)
    {
        // Canvas를 만들면 EventSystem도 같이 생성되면서 이벤트시스템이 UI클릭 판독함
        // 이부분에 이벤트 시스템이 UI에 마우스에 올라가있을때는 되지 않게끔
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // 키가 눌려 졌는지 InputAction을 Any로 해서
        if (gameManager.isWaveGamePlaying)
            isAttacking = inputValue.isPressed;

    }
}
