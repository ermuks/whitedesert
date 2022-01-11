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

    // ������ ����� �����յ�
    public List<AudioPlayer> audioList = new List<AudioPlayer>();

    // AudioPlayer ������
    private GameObject audioPrefab;

    // ����� �ҽ���
    private Dictionary<string, AudioClip> audioSources = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        // Resources �������� AudioPlayer �������� ������
        audioPrefab = Resources.Load<GameObject>("Prefabs/AudioPlayer");

        // REsources/Audio �������� ���� ���ϵ鸸 ������
        AudioClip[] audios = Resources.LoadAll<AudioClip>("Audio");

        // Dictionary�� <"BGM", �����> �������� �߰��� ���ش�. (���� �̸��� Ű������ ��)
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