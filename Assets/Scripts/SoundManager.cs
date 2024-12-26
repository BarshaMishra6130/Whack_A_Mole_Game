using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image bgSoundOnIcon;
    [SerializeField] Image bgSoundOffIcon;
    [SerializeField] Image sfxSoundOnIcon;
    [SerializeField] Image sfxSoundOffIcon;
    [SerializeField] AudioSource bgAudioSource;
    [SerializeField] AudioSource sfxAudioSource;

    private bool bgMuted = false;
    private bool sfxMuted = false;

    void Start()
    {
        if (!PlayerPrefs.HasKey("bgMuted")) PlayerPrefs.SetInt("bgMuted", 0);
        if (!PlayerPrefs.HasKey("sfxMuted")) PlayerPrefs.SetInt("sfxMuted", 0);

        Load();
        UpdateButtonIcons();
        ApplyAudioSettings();
    }

    public void OnBgButtonPress()
    {
        bgMuted = !bgMuted;
        ApplyAudioSettings();
        Save();
        UpdateButtonIcons();
    }

    public void OnSfxButtonPress()
    {
        sfxMuted = !sfxMuted;
        ApplyAudioSettings();
        Save();
        UpdateButtonIcons();
    }

    private void UpdateButtonIcons()
    {
        bgSoundOnIcon.enabled = !bgMuted;
        bgSoundOffIcon.enabled = bgMuted;
        sfxSoundOnIcon.enabled = !sfxMuted;
        sfxSoundOffIcon.enabled = sfxMuted;
    }

    private void ApplyAudioSettings()
    {
        bgAudioSource.mute = bgMuted;
        sfxAudioSource.mute = sfxMuted;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("bgMuted", bgMuted ? 1 : 0);
        PlayerPrefs.SetInt("sfxMuted", sfxMuted ? 1 : 0);
    }

    private void Load()
    {
        bgMuted = PlayerPrefs.GetInt("bgMuted") == 1;
        sfxMuted = PlayerPrefs.GetInt("sfxMuted") == 1;
    }
}
