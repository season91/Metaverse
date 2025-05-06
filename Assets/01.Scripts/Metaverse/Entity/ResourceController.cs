using System;
using UnityEngine;

// Metaverse - Player, �� Enemy�� ����
public class ResourceController : MonoBehaviour
{
    // ������ ���� ��ó���� ���� ����
    // ���� �� ���� ���� �ð�
    [SerializeField] private float healthChangeDelay = .5f;
    // ��ȭ�� ���� �ð��� ���� -> ���� �ð� �Ŀ� �ٽ� ��ȭ�ϱ⶧����
    // ������ ü�� ���� ���� ��� �ð�
    private float timeSinceLastChange = float.MaxValue;
    // ���� ü�� (�ܺ� ���ٸ� ���)
    public float CurrentHealth { get; private set; }
    // �ִ� ü���� StatHandler�κ��� ������
    public float MaxHealth => statHandler.Health;
    // ü���� �ٲ� �� ȣ��Ǵ� �̺�Ʈ (���� ü��, �ִ� ü�� ����)
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
        // ���� ���� ���¶�� �ð� ����
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
        }
    }

    // ������ �����ϴ� �κ� 
    public bool ChangeHealth(float change)
    {
        // ������ ���� ����
        // ��ȭ ���ų� ���� ���¸� ����
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        // ������ �޾Ҵٸ� �ʱ�ȭ
        // �ٽ� ���� ����
        timeSinceLastChange = 0f;

        // ü�� ����
        CurrentHealth += change;

        // ���� ����ó��
        // �������� ��� (����)
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        // ü�� ���� �̺�Ʈ ȣ�� (UI ��� �� ���� ������ ó����)
        // ��������Ʈ�� Ȱ���ؼ� OnChangeHealth ������ ȣ��Ǵ� ������ ����� ��
        // OnChangeHealth�� ����� �Լ��� �ִٸ� �ΰ��� �Ѱ��ְ� �����Ű�ڴٴ� ��
        //OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

        if (change < 0)
        {
            animationHandler.Damage();// �´� �ִϸ��̼� ����

        }

        // ĳ���� ü���� 0 ���ϰ� �Ǹ� ��� ó��
        if (CurrentHealth <= 0f)
        {
            Death();
        }

        return true;
    }

    private void Death()
    {
        baseController.Death(); // ����� �ƴ϶�
    }

    // GameManager���� ȣ���� ��
    // �ܺο��� ü�� ���� �̺�Ʈ�� ����ϴ� �Լ�
    public void AddHealthChangeEvent(Action<float, float> action)
    {
        // �Ķ���ʹ� CurrentHealth, MaxHealth
        OnChangeHealth += action;
    }

    // �ܺο��� ü�� ���� �̺�Ʈ�� �����ϴ� �Լ�
    public void RemoveHealthChangeEvent(Action<float, float> action)
    {
        // �Ķ���ʹ� CurrentHealth, MaxHealth
        OnChangeHealth -= action;
    }
}