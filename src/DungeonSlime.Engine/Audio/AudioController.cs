using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace DungeonSlime.Engine.Audio;

public class AudioController : IDisposable
{
    private readonly List<SoundEffectInstance> _activeSoundEffectIntances;
    private float _previousSongVolume;
    private float _previousSoundEffectVolume;

    public bool IsDisposed { get; private set; }
    public bool IsMuted { get; private set; }

    public float SongVolume
    {
        get
        {
            if (IsMuted)
            {
                return 0.0f;
            }
            return MediaPlayer.Volume;
        }

        set
        {
            if (IsMuted)
            {
                return;
            }
            MediaPlayer.Volume = Math.Clamp(value, 0.0f, 1.0f);
        }
    }

    public float SoundEffectVolume
    {
        get
        {
            if (IsMuted)
            {
                return 0.0f;
            }
            return SoundEffect.MasterVolume;
        }

        set
        {
            if (IsMuted)
            {
                return;
            }
            SoundEffect.MasterVolume = Math.Clamp(value, 0.0f, 1.0f);
        }
    }

    public AudioController() => _activeSoundEffectIntances = new List<SoundEffectInstance>();
    ~AudioController() => Dispose();


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool diposing)
    {
        if (IsDisposed)
        {
            return;
        }
        if (diposing)
        {
            foreach (SoundEffectInstance soundEffectInstance in _activeSoundEffectIntances)
            {
                soundEffectInstance.Dispose();
            }
            _activeSoundEffectIntances.Clear();
        }

        IsDisposed = true;
    }


    public void Update()
    {
        for (int i = 0; i < _activeSoundEffectIntances.Count; i++)
        {
            SoundEffectInstance instance = _activeSoundEffectIntances[i];

            if (instance.State == SoundState.Stopped)
            {
                if (!instance.IsDisposed)
                {
                    instance.Dispose();
                }
                _activeSoundEffectIntances.RemoveAt(i);
                i--;
            }
        }
    }

    public SoundEffectInstance PlaySoundEffect(SoundEffect soundEffect) => PlaySoundEffect(soundEffect, 1.0f, 0.0f, 0.0f, false);
    public SoundEffectInstance PlaySoundEffect(SoundEffect soundEffect, float volume, float pitch, float pan, bool isLooped)
    {
        SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();

        soundEffectInstance.Volume = volume;
        soundEffectInstance.Pitch = pitch;
        soundEffectInstance.Pan = pan;
        soundEffectInstance.IsLooped = isLooped;

        soundEffect.Play();
        _activeSoundEffectIntances.Add(soundEffectInstance);

        return soundEffectInstance;
    }

    public void PlaySong(Song song, bool isRepeating = true)
    {
        if (MediaPlayer.State == MediaState.Playing)
        {
            MediaPlayer.Stop();
        }
        MediaPlayer.Play(song);
        MediaPlayer.IsRepeating = isRepeating;
    }

    public void PauseAudio()
    {
        MediaPlayer.Pause();
        for (int i = 0; i < _activeSoundEffectIntances.Count; i++)
        {
            _activeSoundEffectIntances[i].Pause();
        }
    }
    public void ResumeAudio()
    {
        MediaPlayer.Resume();
        for (int i = 0; i < _activeSoundEffectIntances.Count; i++)
        {
            _activeSoundEffectIntances[i].Resume();
        }
    }

    public void MuteAudio()
    {
        _previousSongVolume = MediaPlayer.Volume;
        _previousSongVolume = SoundEffect.MasterVolume;


        MediaPlayer.Volume = 0.0f;
        SoundEffect.MasterVolume = 0.0f;

        IsMuted = true;
    }

    public void UnmuteAudio()
    {
        MediaPlayer.Volume = _previousSongVolume;
        SoundEffect.MasterVolume = _previousSoundEffectVolume;

        IsMuted = false;
    }

    public void ToggleMute()
    {
        if (IsMuted)
        {
            UnmuteAudio();
        }
        else
        {
            MuteAudio();
        }
    }
}