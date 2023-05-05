using UnityEngine;
using UnityEngine.UI;

public class KarmaBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private float maxKarma;
    private float fillValue;
    private Slider slider;
    private Color prevColor;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        maxKarma = Karma01Tracker.Instance.MaxKarma;
        prevColor = fillImage.color;
    }

    private void OnEnable()
    {
        Karma01Tracker.Instance.OnKarmaChanged += ChangeSliderFillValue;
    }

    private void OnDisable()
    {
        Karma01Tracker.Instance.OnKarmaChanged -= ChangeSliderFillValue;
    }

    private void ChangeSliderFillValue(int newKarma)
    {    
        fillValue = (float)newKarma / maxKarma;
        slider.value = fillValue;
        if(slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }

        if(slider.value > slider.maxValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }

        if(fillValue <= slider.maxValue / 2)
        {
            fillImage.color = Color.red;
        }
        else if(fillValue > slider.maxValue / 2)
        {
            fillImage.color = prevColor;
        }
    }
}
