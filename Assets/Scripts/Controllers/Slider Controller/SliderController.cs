using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();

        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        AudioManager.Instance.MasterSlider = slider;
        AudioManager.Instance.SetMasterVolume();
    }
}
