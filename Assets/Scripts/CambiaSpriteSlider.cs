using UnityEngine;
using UnityEngine.UI;

public class CambiaSpriteSlider : MonoBehaviour
{
    public Slider slider;           // El slider que estás usando
    public Image fillImage;         // La imagen del Fill Area
    public Sprite spriteVacio;
    public Sprite spriteMedio;
    public Sprite spriteLleno;

    void Start()
    {
        slider.onValueChanged.AddListener(CambiarSprite);
        CambiarSprite(slider.value); // Actualiza el sprite al inicio
    }

    void CambiarSprite(float valor)
    {
        if (valor < 0.33f)
        {
            fillImage.sprite = spriteVacio;
        }
        else if (valor < 0.66f)
        {
            fillImage.sprite = spriteMedio;
        }
        else
        {
            fillImage.sprite = spriteLleno;
        }
    }
}
