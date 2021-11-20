using AbductionCar.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AbductionCar.UI
{

    public class OptionsMenu : MonoBehaviour
    {
        [SerializeField] GameObject optionsPanel;

        [SerializeField] private Selectable enableSelectedObejct;
        [SerializeField] private Selectable disableSelectedObejct;

        //解像度
        [SerializeField] private Dropdown resolutionDropdown;
        private int beforeDropdownValue;
        List<Resolution> resolutionList;

        //フルスクリーン
        [SerializeField] private Toggle fullScreen;
        private bool beforeFullScreenValue;

        //音量
        [SerializeField] private VolumeController bgmVolumeController;
        [SerializeField] private VolumeController seVolumeController;

        void Start()
        {
            AudioManager.Instance.Load();

            resolutionList =  new List<Resolution>(Screen.resolutions);
            foreach (var item in resolutionList)
            {
                resolutionDropdown.options.Add(new Dropdown.OptionData(item.ToString()));
            }
            resolutionDropdown.value = CheckResolutionIndex();
            beforeDropdownValue = resolutionDropdown.value;
            fullScreen.isOn = Screen.fullScreen;
            bgmVolumeController.SliderReset();
            seVolumeController.SliderReset();
        }

        private int CheckResolutionIndex()
        {
            int count = 0;

            foreach (var item in resolutionList)
            {
                if (Screen.currentResolution.width != item.width ||
                    Screen.currentResolution.height != item.height ||
                    Screen.currentResolution.refreshRate != item.refreshRate)
                {
                    count++;
                }
                else
                {
                    return count;
                }
            }
            return -1;
        }

        public void Apply()
        {
            AudioManager.Instance.Save();
            Resolution resolution = resolutionList[resolutionDropdown.value];
            Screen.SetResolution(resolution.width, resolution.width, fullScreen.isOn);
        }

        public void Undo()
        {
            AudioManager.Instance.Load();
            fullScreen.isOn = beforeFullScreenValue;
            resolutionDropdown.value = beforeDropdownValue;
            bgmVolumeController.SliderReset();
            seVolumeController.SliderReset();
        }


        public void Enable()
        {
            if (enableSelectedObejct)
            {
                enableSelectedObejct.Select();
            }
            beforeDropdownValue = resolutionDropdown.value;
            beforeFullScreenValue = Screen.fullScreen;
            optionsPanel.SetActive(true);
        }

        public void Disable()
        {
            if (disableSelectedObejct)
            {
                disableSelectedObejct.Select();
            }
            optionsPanel.SetActive(false);
        }
    }

}