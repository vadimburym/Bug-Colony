using System.Collections.Generic;
using _Project.Code.Core.Keys;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Audio;

namespace _Project.Code.Infrastructure
{
    public sealed class AudioProvider : SerializedMonoBehaviour, IAudioProvider
    {
        private const float CHANNEL_VOLUME_MAXIMUM = 0.0f;
        private const float CHANNEL_VOLUME_MINIMUM = -80.0f;
        
        [SerializeField] private AudioMixer _audioMixer;
        [OdinSerialize] private Dictionary<AudioOutputId, string> _audioGroupsName;
        [OdinSerialize] private Dictionary<AudioOutputId, AudioSourceAdapter> _audioSources;
        [SerializeField] private AudioSourcePool _audioSourcePool;

        private readonly Dictionary<AudioOutputId, AudioSourceSnapshot> _defaultSnapshots = new();
        private readonly Dictionary<AudioOutputId, AudioMixerGroup> _audioMixerGroups = new();
        
        private void Awake()
        {
            foreach (var source in _audioSources)
                _defaultSnapshots[source.Key] = source.Value.GetSnapshot();
            var outputs = EnumUtils<AudioOutputId>.Values;
            for (int i = 0; i < outputs.Length; i++)
            {
                var outputName = EnumUtils<AudioOutputId>.ToString(outputs[i]);
                var mixerGroups = _audioMixer.FindMatchingGroups(outputName);
                if (mixerGroups == null)
                    Debug.LogError($"There is no output {outputName}");
                else
                    _audioMixerGroups.Add(outputs[i], mixerGroups[0]);
            }
        }
        
        public void SetAudioOutputVolume(AudioOutputId output, float value)
        {
            if (_audioGroupsName.TryGetValue(output, out var groupName))
                _audioMixer.SetFloat(groupName, Mathf.Clamp(value, CHANNEL_VOLUME_MINIMUM, CHANNEL_VOLUME_MAXIMUM));
        }

        public void Play(
            AudioOutputId output,
            AudioClip clip,
            float volumeScale = 1.0f,
            AudioSourceSnapshot snapshot = null)
        {
            var source = _audioSources[output];
            source.SetAudioSourceSnapshot(snapshot ?? _defaultSnapshots[output]);
            source.Play(clip, volumeScale);
        }

        public void PlayOneShot(
            AudioOutputId output,
            AudioClip clip,
            float volumeScale = 1.0f,
            float pitch = 1.0f,
            AudioSourceSnapshot snapshot = null)
        {
            var source = _audioSources[output];
            source.SetAudioSourceSnapshot(snapshot ?? _defaultSnapshots[output]);
            source.PlayOneShot(clip, volumeScale, pitch);
        }

        public void Play(
            AudioOutputId output,
            AudioClip clip,
            Vector3 position,
            float volumeScale = 1.0f,
            AudioSourceSnapshot snapshot = null)
        {
            var source = _audioSourcePool.GetAudioSource();
            source.SetAudioSourceOutput(_audioMixerGroups[output]);
            source.SetAudioSourceSnapshot(snapshot ?? _defaultSnapshots[output]);
            source.SetPosition(position);
            source.Play(clip, volumeScale);
        }
        
        public void Play(
            AudioOutputId output,
            AudioClip clip,
            Transform parent,
            float volumeScale = 1.0f,
            AudioSourceSnapshot snapshot = null)
        {
            var source = _audioSourcePool.GetAudioSource();
            source.SetAudioSourceOutput(_audioMixerGroups[output]);
            source.SetAudioSourceSnapshot(snapshot ?? _defaultSnapshots[output]);
            source.SetParent(parent, false);
            source.Play(clip, volumeScale);
        }
    }
}