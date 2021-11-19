using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using AbductionCar.Framework;
using System;

namespace AbductionCar.Managers
{
    public class AudioManager : SingletonMonoBehaviour<AudioManager>
    {
        public const string DataFileName = "volume.save";

        public enum AudioGroup
        {
            Master,
            BGM,
            SE
        }

        public readonly AudioGroup[] saveAudioGroups =
        {
            AudioGroup.BGM,
            AudioGroup.SE
        };

        [SerializeField] private AudioMixer audioMixer;

        // Start is called before the first frame update
        void Start()
        {
            Load();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// 現在の音量のデータをファイルに保存する関数
        /// </summary>
        public void Save()
        {
            VolumeDataListWrapper wrapper = new VolumeDataListWrapper();
            foreach (var audioGroup in saveAudioGroups)
            {
                VolumeData volumeData = new VolumeData(audioGroup, GetVolume(audioGroup));
                wrapper.volumeDataList.Add(volumeData);
            }
            string data = JsonUtility.ToJson(wrapper);
            FileManager.Save(DataFileName, data);
        }

        /// <summary>
        /// 音量のデータをファイルから読み取り適用する関数
        /// </summary>
        public void Load()
        {
            string data = FileManager.Load(DataFileName);
            VolumeDataListWrapper wrapper = JsonUtility.FromJson<VolumeDataListWrapper>(data);
            foreach (var volumeData in wrapper.volumeDataList)
            {
                SetVolume(volumeData.audioGroup, volumeData.volume);
            }
        }

        public float GetVolume(AudioGroup name)
        {
            float ret = 0;
            audioMixer.GetFloat(name.ToString(), out ret);
            return ret;
        }

        public void SetVolume(AudioGroup name, float value)
        {
            audioMixer.SetFloat(name.ToString(), value);
        }

        [Serializable]
        public class VolumeData
        {
            public AudioGroup audioGroup;
            public float volume;
            public VolumeData(AudioGroup audioGroup, float volume)
            {
                this.audioGroup = audioGroup;
                this.volume = volume;
            }
        }

        [Serializable]
        public class VolumeDataListWrapper
        {
            public List<VolumeData> volumeDataList;
            public VolumeDataListWrapper()
            {
                volumeDataList = new List<VolumeData>();
            }
        }
    }
}