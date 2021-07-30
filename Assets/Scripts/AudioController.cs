using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource[] musicList;
    private float[] musicVolumeList;

    private void Start()
    {
        musicVolumeList = new float[musicList.Length];
        for (int i = 0; i < musicVolumeList.Length; i++)
        {
            musicVolumeList[i] = musicList[i].volume;
        }
    }

    public void TogglePause(bool isPaused)
    {
        if (Time.timeScale == 0)
        {
            for (int i = 0; i < musicList.Length; i++)
            {
                musicList[i].volume = musicList[i].volume * 0.25f;
            }
        }
        else
        {
            for (int i = 0; i < musicList.Length; i++)
            {
                musicList[i].volume = musicVolumeList[i];
            }
        }

    }
}
