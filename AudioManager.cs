using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<AudioManager>();
            return _instance;
        }
    }

    // 생성된 오디오 프리팹들
    public List<AudioPlayer> audioList = new List<AudioPlayer>();

    // AudioPlayer 프리팹
    private GameObject audioPrefab;

    // 오디오 소스들
    private Dictionary<string, AudioClip> audioSources = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        // Resources 폴더에서 AudioPlayer 프리팹을 가져옴
        audioPrefab = Resources.Load<GameObject>("Prefabs/AudioPlayer");

        // REsources/Audio 폴더에서 음향 파일들만 가져옴
        AudioClip[] audios = Resources.LoadAll<AudioClip>("Audio");

        // Dictionary에 <"BGM", 오디오> 형식으로 추가를 해준다. (파일 이름을 키값으로 씀)
        foreach (var v in audios)
        {
            audioSources.Add(v.name, v);
        }

        Debug.Log(audioSources.Count);
    }

    public void AddAudio(AudioClip src, Transform parent, Vector3 pos, bool isWorld, bool isLoop)
    {
        if (isWorld)
        {
            GameObject go = Instantiate(audioPrefab);
            go.transform.position = parent.position + pos;
            go.GetComponent<AudioPlayer>().Source(src).Play();
            go.GetComponent<AudioPlayer>().Loop(isLoop);
        }
        else
        {
            GameObject go = Instantiate(audioPrefab, parent);
            go.transform.localPosition = pos;
            go.GetComponent<AudioPlayer>().Source(src).Play();
            go.GetComponent<AudioPlayer>().Loop(isLoop);
        }
    }

    public AudioClip GetAudio(string name)
    {
        if (audioSources.TryGetValue(name, out var src))
        {
            return src;
        }
        return null;
    }
}