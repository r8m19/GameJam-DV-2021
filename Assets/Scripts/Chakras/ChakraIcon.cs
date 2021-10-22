using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChakraIcon : MonoBehaviour
{
    public Image image;
    public Sprite openImage, closedImage;

    private void Awake()
    {
        if (!image)
            image = GetComponent<Image>();
    }

    public void UpdateImage(bool open)
    {
        if (open)
            image.sprite = openImage;
        else
            image.sprite = closedImage;
    }

}
