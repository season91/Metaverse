using UnityEngine;

// SoundSource Prefab ����
public class SoundSource : MonoBehaviour
{
    private AudioSource _audioSource; // �Ҹ��� ����� AudioSource ������Ʈ

    // ȿ������ ����ϴ� �Լ�
    public void Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)
    {
        // ������ҽ�
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        // ������ ���� ĵ�� �޾� ��
        // ���� Invoke ������ �ִٸ� ���
        CancelInvoke();
        _audioSource.clip = clip; ;
        _audioSource.volume = soundEffectVolume;
        _audioSource.Play();

        // ��ġ�� �ణ�� �������� �༭ ���� ����� �پ��ϰ� �鸮�� ��
        // ��ġ�� �����������ؼ� �Ź� ���� �Ҹ��� �ƴ϶� �ٸ� �Ҹ��� ������
        _audioSource.pitch = 1f + Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance);

        // �޼ҵ� �̸� ������ ���� ���� ����
        // ���� ���� + ���� �ð� ���Ŀ� �ڵ� ����
        Invoke("Disable", clip.length + 2);
    }

    // 2�� �ڿ� ����� ��
    public void Disable()
    {
        _audioSource.Stop();
        Destroy(this.gameObject);
    }
}
