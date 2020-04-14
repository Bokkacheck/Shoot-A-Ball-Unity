using UnityEngine;
using UnityEngine.UI;

public class MakeAnimation : MonoBehaviour
{
    public Image showedImage;
    public Sprite[] allImages;
    void Update()
    {
        showedImage.sprite = allImages[(allImages.Length-1)-((int)(Time.time*10)%allImages.Length)];
    }
}
