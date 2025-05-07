using UnityEngine;

// SoundSource Prefab 연결
public class SoundSource : MonoBehaviour
{
    private AudioSource _audioSource; // 소리를 재생할 AudioSource 컴포넌트

    // 효과음을 재생하는 함수
    public void Play(AudioClip clip, float soundEffectVolume, float soundEffectPitchVariance)
    {
        // 오디오소스
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        // 재사용을 위해 캔슬 달아 줌
        // 이전 Invoke 예약이 있다면 취소
        CancelInvoke();
        _audioSource.clip = clip; ;
        _audioSource.volume = soundEffectVolume;
        _audioSource.Play();

        // 피치에 약간의 랜덤성을 줘서 같은 사운드라도 다양하게 들리게 함
        // 피치란 높낮이조절해서 매번 같은 소리가 아니라 다른 소리가 나도록
        _audioSource.pitch = 1f + Random.Range(-soundEffectPitchVariance, soundEffectPitchVariance);

        // 메소드 이름 넣으면 지연 실행 가능
        // 사운드 길이 + 여유 시간 이후에 자동 제거
        Invoke("Disable", clip.length + 2);
    }

    // 2초 뒤에 실행될 것
    public void Disable()
    {
        _audioSource.Stop();
        Destroy(this.gameObject);
    }
}
