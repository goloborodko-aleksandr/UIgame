using System;
using System.Collections.Generic;
using System.Linq;
using Engine.AxGridUnityTools.Base;
using Engine.AxGridUnityTools.Model;
using Engine.AxGridUnityTools.Utils;
using UnityEngine;

namespace Scritps.Main {
    
    /// <summary>
    /// Менеджер звуков, создает и проигрывает звуки, из пула AudioSource
    /// </summary>
    public class BasicSoundManager : MonoBehaviourExtBind {

        public class BasicSoundManagerAudioSource : MonoBehaviour
        {
            public AudioSource source;
            public Queue<AudioClip> clipsForPlaying = new Queue<AudioClip>();
            public bool isPlaying => clipsForPlaying.Count > 0 || source.isPlaying;

            public void AddAudioClip(AudioClip clip) => clipsForPlaying.Enqueue(clip);

            public long StartTime { get; private set; } = DateTime.Now.ToFileTimeUtc();
            
            public float volume
            {
                get { return source.volume; }
                set { source.volume = value;  }
            }
            
            public void Play(AudioClip[] clips, bool loop = false) {
                StartTime = DateTime.Now.ToFileTimeUtc();
                this.loop = loop;
                foreach (var clip in clips)
                    clipsForPlaying.Enqueue(clip);
                Play();
            }
            
            public void Play()
            {
                StartTime = DateTime.Now.ToFileTimeUtc();
                if (clipsForPlaying.Count == 0)
                    return;

                source.clip = clipsForPlaying.Dequeue();
                source.loop = clipsForPlaying.Count == 0 && loop;
                source.Play();
            }

            public void Stop()
            {
                clipsForPlaying.Clear();
                source.volume = 0;
                source.Stop();
                StartTime = new DateTime(1980, 01, 01).ToFileTimeUtc();

            }

            public bool loop = false;

            public void Update()
            {
                if (!source.isPlaying && clipsForPlaying.Count > 0)
                    Play();
            }
        }
        
        
        [Serializable]
        public class SoundClip {
            public AudioClip clip;
            public string name;
        }

        public SoundClip[] clips;        
        public int poolStartSize = 5;
        public int maxPoolSize = 40;

        private List<BasicSoundManagerAudioSource> sourcePool;
        private Dictionary<string, AudioClip> _clips;
        
        [OnAwake]
        public void Init() {
            sourcePool = new List<BasicSoundManagerAudioSource>();
            for(var i=0;i<poolStartSize;i++)
                sourcePool.Add(CreateNewSource());
            _clips = new Dictionary<string, AudioClip>();
            foreach(var clip in clips)
                if (!_clips.ContainsKey(clip.name == "" ? clip.clip.name : clip.name))
                    _clips.Add(clip.name == "" ? clip.clip.name : clip.name, clip.clip);
                else
                    _clips[clip.name == "" ? clip.clip.name : clip.name] = clip.clip;
        }

        private BasicSoundManagerAudioSource CreateNewSource() {
            var go = new GameObject();
            var audioSource = go.AddComponent<BasicSoundManagerAudioSource>();
            audioSource.source = go.AddComponent<AudioSource>();
            audioSource.source.playOnAwake = false;
            go.transform.SetParent(transform, false);
            return audioSource;
        }


        private BasicSoundManagerAudioSource GetFreeSource() {
            var source = sourcePool.Find(item => !item.isPlaying);
            if (source != null) return source;
            if (sourcePool.Count >= maxPoolSize)
                return sourcePool.OrderBy(s => s.StartTime).First();
            source = CreateNewSource();
            sourcePool.Add(source);
            return source;
        }
        
        [Bind]
        public void OnSoundManagerPlay(string soundName, string extName = "", float volume = 1.0f) {
            if (!_clips.ContainsKey(soundName))
                return;

            var source = GetFreeSource();
            if (source == null) return;
    
            source.name = string.IsNullOrEmpty(extName) ? soundName : extName;
            source.volume = volume;
            source.AddAudioClip(_clips[soundName]);
            source.loop = false;
            source.Play();
        }

        
        [Bind]
        public void OnSoundsManagerPlayArray(string[] soundName, string extName = "", float volume = 1.0f) {
            var sounds = new List<string>(soundName).Where(item => _clips.ContainsKey(item)).Select(item => _clips[item]).ToList();
            if (sounds.Count == 0) return;
            var source = GetFreeSource();
            if (source == null) return;
            source.name = string.IsNullOrEmpty(extName) ? soundName[0] : extName;
            source.volume = volume;
            sounds.ForEach(source.AddAudioClip);
            source.loop = false;
            source.Play();
        }
        
        
        [Bind]
        public void OnSoundManagerLoop(string soundName, string extName = "", float volume = 1.0f) {
            if (!_clips.ContainsKey(soundName))
                return;
            var source = GetFreeSource();
            if (source == null) return;
            source.volume = volume;
            source.name = string.IsNullOrEmpty(extName) ? soundName : extName;
            source.AddAudioClip(_clips[soundName]);
            source.loop = true;
            source.Play();
        }
        
        
        [Bind]
        public void OnSoundsManagerLoopArray(string[] soundName, string extName = "", float volume = 1.0f) {
            var sounds = new List<string>(soundName).Where(item => _clips.ContainsKey(item)).Select(item => _clips[item]).ToList();
            if (sounds.Count == 0)
                return;
            var source = GetFreeSource();
            if (source == null) return;
            source.name = string.IsNullOrEmpty(extName) ? soundName[0] : extName;
            source.volume = volume;
            sounds.ForEach(source.AddAudioClip);
            source.loop = true;
            source.Play();
        }
        
        [Bind]
        public void OnSoundManagerStop(string soundName) {
            foreach (var source in GetComponentsInChildren<BasicSoundManagerAudioSource>())
            {
                if (source.name == soundName)
                    source.Stop();
            }
        
        }
        
        [Bind]
        public void OnSoundManagerOn()
        {
            GetComponentsInChildren<BasicSoundManagerAudioSource>()
                .ForEach(i => i.volume = 1);
        }
        
        [Bind]
        public void OnSoundManagerOn(string soundName)
        {
            GetComponentsInChildren<BasicSoundManagerAudioSource>()
                .Where(i=> i.name.StartsWith(soundName))
                .ForEach(i => i.volume = 1);
        }
        
        [Bind]
        public void OnSoundManagerOff()
        {
            GetComponentsInChildren<BasicSoundManagerAudioSource>()
                .ForEach(i => i.volume = 0);
        }
        
        [Bind]
        public void OnSoundManagerOff(string soundName)
        {
            GetComponentsInChildren<BasicSoundManagerAudioSource>()
                .Where(i=> i.name.StartsWith(soundName))
                .ForEach(i => i.volume = 0);
        }

    }
}