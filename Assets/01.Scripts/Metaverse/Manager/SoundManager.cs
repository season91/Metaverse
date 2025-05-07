using UnityEngine;

// Metaverse - SoundManager ����. ������� �Ŵ���
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;

    private AudioSource musicAudioSource; // ��� ���ǿ� AudioSource
    public AudioClip musicClip;// �⺻ ��� ���� Ŭ��

    // ȿ������ ����� ������ (SoundSource)
    public SoundSource soundSourcePrefab;

    private void Awake()
    {
        // �ߺ� ����
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // �̱��� 
        instance = this;

        // ����� ����
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;
    }

    // �⺻ ��� ����
    private void Start()
    {
        ChangeBackGroundMusic(musicClip);
    }

    // �⺻ �������
    public void BasicBackGroundMusic()
    {
        ChangeBackGroundMusic(musicClip);
    }


    // �ٸ� ����������� ����
    public void ChangeBackGroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    // �����ϰ� �����ϱ� ���� static �� ������ ���� ������ ���� ������ static ���� ���ؼ� ������ ��
    public void PlayClip(AudioClip clip)
    {
        // SoundSource ������ �ν��Ͻ� ���� �� ���
        SoundSource obj = Instantiate(instance.soundSourcePrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }

    public void MusicStop()
    {
        musicAudioSource.Stop();
    }
}
