using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("씬 이름 설정 (유니티 씬 파일명과 똑같이 적어주세요)")]
    public string titleSceneName = "TitleScene";       // 타이틀 씬 이름
    public string stageSelectSceneName = "StageScene"; // 스테이지 선택 씬 이름
    public string charSelectSceneName = "CharacterSelectScene";   // 캐릭터 선택 씬 이름
    public string gameSceneName = "GameScene";         // 게임 씬 이름

    [Header("배경음악 파일 연결")]
    public AudioClip titleBGM;       // 타이틀 음악
    public AudioClip stageSelectBGM; // 스테이지 선택 음악
    public AudioClip charSelectBGM;  // 캐릭터 선택 음악
    public AudioClip gameBGM;        // 게임 음악

    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 바뀔 때마다 실행되는 함수
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string currentScene = scene.name;

        if (currentScene == titleSceneName)
        {
            PlayMusic(titleBGM);
        }
        else if (currentScene == stageSelectSceneName)
        {
            PlayMusic(stageSelectBGM);
        }
        else if (currentScene == charSelectSceneName)
        {
            PlayMusic(charSelectBGM);
        }
        else if (currentScene == gameSceneName)
        {
            PlayMusic(gameBGM);
        }
    }

    void PlayMusic(AudioClip clip)
    {
        // 음악 파일이 비어있으면 아무것도 안 함 (에러 방지)
        if (clip == null) return;

        // 지금 재생 중인 노래와 같으면 다시 틀지 않음 (끊김 방지)
        if (audioSource.clip == clip) return;

        audioSource.clip = clip;
        audioSource.Play();
    }
}
