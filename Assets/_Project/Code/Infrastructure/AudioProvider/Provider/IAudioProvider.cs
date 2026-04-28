using _Project.Code.Core.Keys;
using UnityEngine;

namespace _Project.Code.Infrastructure
{
    public interface IAudioProvider
    {
        void SetAudioOutputVolume(AudioOutputId output, float value);
        void Play(AudioOutputId output, AudioClip clip, float volumeScale = 1.0f, AudioSourceSnapshot snapshot = null);
        void PlayOneShot(AudioOutputId output, AudioClip clip, float volumeScale = 1.0f, float pitch = 1.0f,
            AudioSourceSnapshot snapshot = null);
        void Play(AudioOutputId output, AudioClip clip, Vector3 position, float volumeScale = 1.0f,
            AudioSourceSnapshot snapshot = null);
        void Play(AudioOutputId output, AudioClip clip, Transform parent, float volumeScale = 1.0f,
            AudioSourceSnapshot snapshot = null);
    }
}