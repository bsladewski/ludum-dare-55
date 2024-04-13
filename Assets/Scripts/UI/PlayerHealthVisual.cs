using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthVisual : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private Sprite emptySprite;

    [SerializeField]
    private Sprite fullSprite;

    public void SetEmpty()
    {
        image.sprite = emptySprite;
    }

    public void SetFull()
    {
        image.sprite = fullSprite;
    }
}
