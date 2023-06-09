using UnityEngine;
using UnityEngine.UI;
namespace CarmaRoad.UI
{
    public class KarmaBar : MonoBehaviour
    {
        [SerializeField] private Image fillImage;

        private int maxKarma;
        private float fillValue;
        private Slider slider;
        private Color prevColor;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            maxKarma = KarmaaManager.Instance.MaxKarma;
            prevColor = fillImage.color;
        }

        private void OnEnable()
        {
            KarmaaManager.Instance.OnKarmaChanged += ChangeSliderFillValue;
        }

        private void OnDisable()
        {
            KarmaaManager.Instance.OnKarmaChanged -= ChangeSliderFillValue;
        }

        private void ChangeSliderFillValue(int newKarma)
        {
            fillValue = (float)newKarma / (float)maxKarma;
            slider.value = fillValue;
            if (slider.value <= slider.minValue)
            {
                fillImage.enabled = false;
            }

            if (slider.value > slider.minValue && !fillImage.enabled)
            {
                fillImage.enabled = true;
            }

            if (fillValue <= slider.maxValue / 2)
            {
                fillImage.color = Color.red;
            }
            else if (fillValue > slider.maxValue / 2)
            {
                fillImage.color = prevColor;
            }
        }
    }
}
