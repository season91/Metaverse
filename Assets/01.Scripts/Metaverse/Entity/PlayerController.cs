using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : BaseController
{
    // 마우스 위치를 월드 좌표로 변환하기 위한 메인 카메라 참조
    private Camera _camera; 
    private GameManager gameManager;
    private GameObject touchItem;
   

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
        if (_camera == null)
        {
            return;
        }

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
    private void OnFire(InputValue inputValue)
    {
        isAttacking = inputValue.isPressed;
    }
    protected override void HandleAction()
    {
        base.HandleAction();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (touchItem != null)
            {
                if (touchItem.gameObject.name == "FlappyGame")
                {
                    GameManager.Instance.StartMiniGame();
                }
                if (touchItem.gameObject.name == "WaveGame")
                {
                    DontDestroyOnLoad(this.gameObject);
                    transform.position = Vector2.zero;
                    GameManager.Instance.StartWaveGame();
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "FlappyGame" || collision.gameObject.name == "WaveGame")
        {
            touchItem = collision.gameObject;
            GameManager.Instance.SetMiniGamePopup(collision.gameObject.transform.position, true);
            // uiManager를 통해 PressUI Active
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "FlappyGame" || collision.gameObject.name == "WaveGame")
        {
            touchItem = null;
            GameManager.Instance.SetMiniGamePopup(collision.gameObject.transform.position, false);
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬 전환 후 카메라 재설정을 위해 사용
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _camera = Camera.main;
    }

    public override void Death()
    {
        base.Death();
        gameManager.GameOver();
    }
}
