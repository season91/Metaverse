using System;
using UnityEngine;

// Metaverse - Player, 각 Enemy에 연결
public class ResourceController : MonoBehaviour
{
    // 데미지 적용 후처리를 위한 변수
    // 피해 후 무적 지속 시간
    [SerializeField] private float healthChangeDelay = .5f;
    // 변화를 가진 시간을 저장 -> 일정 시간 후에 다시 변화하기때문에
    // 마지막 체력 변경 이후 경과 시간
    private float timeSinceLastChange = float.MaxValue;
    // 현재 체력 (외부 접근만 허용)
    public float CurrentHealth { get; private set; }
    // 최대 체력은 StatHandler로부터 가져옴
    public float MaxHealth => statHandler.Health;
    // 체력이 바뀔 때 호출되는 이벤트 (현재 체력, 최대 체력 전달)
    private Action<float, float> OnChangeHealth;

    private BaseController baseController;
    private StatHandler statHandler;
    private AnimationHandler animationHandler;

    private void Awake()
    {
        statHandler = GetComponent<StatHandler>();
        baseController = GetComponent<BaseController>();
        animationHandler = GetComponent<AnimationHandler>();
    }
    private void Start()
    {
        CurrentHealth = statHandler.Health;
    }

    private void Update()
    {
        // 아직 무적 상태라면 시간 누적
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
        }
    }

    // 데미지 적용하는 부분 
    public bool ChangeHealth(float change)
    {
        // 데미지 받지 않음
        // 변화 없거나 무적 상태면 무시
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        // 데미지 받았다면 초기화
        // 다시 무적 시작
        timeSinceLastChange = 0f;

        // 체력 적용
        CurrentHealth += change;

        // 제한 예외처리
        // 데미지일 경우 (음수)
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        // 체력 변경 이벤트 호출 (UI 등에서 이 값을 수신해 처리함)
        // 델리게이트를 활용해서 OnChangeHealth 역으로 호출되는 구조를 만드는 것
        // OnChangeHealth와 연결된 함수가 있다면 두개를 넘겨주고 실행시키겠다는 것
        //OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

        if (change < 0)
        {
            animationHandler.Damage();// 맞는 애니메이션 실행

        }

        // 캐릭터 체력이 0 이하가 되면 사망 처리
        if (CurrentHealth <= 0f)
        {
            Death();
        }

        return true;
    }

    private void Death()
    {
        baseController.Death(); // 상속이 아니라서
    }

    // GameManager에서 호출할 것
    // 외부에서 체력 변경 이벤트를 등록하는 함수
    public void AddHealthChangeEvent(Action<float, float> action)
    {
        // 파라미터는 CurrentHealth, MaxHealth
        OnChangeHealth += action;
    }

    // 외부에서 체력 변경 이벤트를 제거하는 함수
    public void RemoveHealthChangeEvent(Action<float, float> action)
    {
        // 파라미터는 CurrentHealth, MaxHealth
        OnChangeHealth -= action;
    }
}