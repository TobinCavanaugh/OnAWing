using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropDown;

    public TMP_Dropdown fullScreenSetting;

    public Slider refreshRateSlider;
    
    [SerializeField, ReadOnly]
    private int refreshRate = 60;

    private Resolution _resolution;

    private FullScreenMode _fullScreenMode;
    
    // Start is called before the first frame update
    void Start()
    {
        InitResolution();
        InitRefreshRate();
        InitFullscreen();
    }

    private void InitRefreshRate()
    {
        refreshRateSlider.value = (Screen.currentResolution.refreshRate);
    }

    public void SetRefreshRate(float r)
    {
        refreshRate = (int)r;
    }
    
    private void InitResolution()
    {
        resolutionDropDown.options = new List<TMP_Dropdown.OptionData>();
        
        Screen.resolutions.ForEach(x =>
        {
            var res = new TMP_Dropdown.OptionData($"{x.width} x {x.height}");

            if (!resolutionDropDown.options.Any(r => r.text == res.text))
            {
                resolutionDropDown.options.Add(res);    
            }
        });

        resolutionDropDown.value = Screen.resolutions.ToList().IndexOf(Screen.currentResolution);
    }


    private FullScreenMode[] _fullScreenModes = new[]
    {
        FullScreenMode.Windowed,
        FullScreenMode.MaximizedWindow,
        FullScreenMode.FullScreenWindow,
        FullScreenMode.ExclusiveFullScreen
    };
    
    private void InitFullscreen()
    {
        fullScreenSetting.options = new List<TMP_Dropdown.OptionData>();

        _fullScreenModes.ForEach(x => fullScreenSetting.options.Add(new(x.ToString())));
        
        var index = _fullScreenModes.ToList().IndexOf(Screen.fullScreenMode);
        
        fullScreenSetting.value = index;
    }

    public void UpdateResolution(int index)
    {
        Resolution r = new();

        var resolutions = Screen.resolutions;
        List<Resolution> newResolutions = new();

        resolutions.ForEach(r =>
        {
            if (!newResolutions.Any(nr => nr.height == r.height && nr.width == r.width))
            {
                newResolutions.Add(r);
            }
        });

        //if (!resolutionDropDown.options.Any(r => r.text == res.text))
        //{
        //    resolutionDropDown.options.Add(res);    
        //}
        
        _resolution = newResolutions[index];
        //RefreshScreen();
    }

    public void SetFullscreenMode(int index)
    {
        Debug.Log(index);
        _fullScreenMode = _fullScreenModes[index];
    }

    private bool _doVsync = true;
    public void ToggleVsync(bool state)
    {
        _doVsync = state;
    }

    private int _qualityLevel = 2;
    public void ChangeQualityLevel(int index)
    {
        _qualityLevel = index;
    }

    public void RefreshScreen()
    {
        Debug.Log($"W:{_resolution.width} H:{_resolution.height} F:{_fullScreenMode.ToString()} RR:{refreshRate}");
        Screen.SetResolution(_resolution.width, _resolution.height, _fullScreenMode, refreshRate);

        int vSync = 0;
        if (_doVsync)
        {
            QualitySettings.vSyncCount = vSync;
        }
        
        QualitySettings.SetQualityLevel(_qualityLevel);
    }
}
