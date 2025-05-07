using UnityEngine;

// Metaverse - SoundManager 연결. 배경음악 매니저
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;

    private AudioSource musicAudioSource; // 배경 음악용 AudioSource
    public AudioClip musicClip;// 기본 배경 음악 클립

    // 효과음을 재생할 프리팹 (SoundSource)
    public SoundSource soundSourcePrefab;

    private void Awake()
    {
        // 중복 제거
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 싱글톤 
        instance = this;

        // 배경음 설정
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;
    }

    // 기본 배경 음악
    private void Start()
    {
        ChangeBackGroundMusic(musicClip);
    }

    // 기본 배경음악
    public void BasicBackGroundMusic()
    {
        ChangeBackGroundMusic(musicClip);
    }


    // 다른 배경음악으로 변경
    public void ChangeBackGroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    // 간편하게 접근하기 위해 static 각 변수들 접근 권한이 없기 때문에 static 변수 통해서 가져올 것
    public void PlayClip(AudioClip clip)
    {
        // SoundSource 프리팹 인스턴스 생성 후 재생
        SoundSource obj = Instantiate(instance.soundSourcePrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }

    public void MusicStop()
    {
        musicAudioSource.Stop();
    }
}
