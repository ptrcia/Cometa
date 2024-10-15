using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio")]
    [SerializeField] Slider sliderMaster;
    [SerializeField] Slider sliderMusic;
    [SerializeField] Slider sliderSFX;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        sliderMusic.onValueChanged.AddListener(volumeMusic);
        sliderMaster.onValueChanged.AddListener(volumeMaster);
        sliderSFX.onValueChanged.AddListener(volumeSFX);
        AudioListener.pause = false;

    }
    private void Start()
    {
        sliderMaster.value = PlayerPrefs.GetFloat("volumeMaster", 0.75f);
        sliderMusic.value = PlayerPrefs.GetFloat("volumeMusic", 0.75f);
        sliderSFX.value = PlayerPrefs.GetFloat("volumeSFX", 0.75f);
    }

    public void PlaySound(AudioClip clipToPlay)
    {
        SFXSource.clip = clipToPlay;
        SFXSource.PlayOneShot(clipToPlay);
    }
    public void PlayMusic(AudioClip clipToPlay)
    {
        MusicSource.clip = clipToPlay;
        MusicSource.PlayOneShot(clipToPlay);
    }

    #region Volume Audio
    public void volumeMaster(float volume)
    {
        audioMixer.SetFloat("volumeMaster", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volumeMaster", volume);
    }
    public void volumeMusic(float volume)
    {
        audioMixer.SetFloat("volumeMusic", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volumeMusic", volume);

    }
    public void volumeSFX(float volume)
    {
        audioMixer.SetFloat("volumeSFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volumeSFX", volume);

    }
    #endregion
}
