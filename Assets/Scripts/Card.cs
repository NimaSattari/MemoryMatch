using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    [SerializeField] Button myButton;
    public Button MyButton
    {
        get { return myButton; }
    }

    [SerializeField] Image myImage;
    public Image MyImage
    {
        get { return myImage; }
    }
    [SerializeField] Sprite backSprite;
    public Sprite BackSprite
    {
        get { return backSprite; }
        set { backSprite = value; }
    }
    [SerializeField] Sprite frontSprite;
    public Sprite FrontSprite
    {
        get { return frontSprite; }
        set { frontSprite = value; }
    }

    public void SetSprites(Sprite back, Sprite front)
    {
        backSprite = back;
        frontSprite = front;
        TurnCard(false);
    }

    public void TurnCard(bool turnFront)
    {
        if(turnFront)
        {
            myImage.sprite = frontSprite;
        }
        else
        {
            myImage.sprite = BackSprite;
        }
    }

    public void DestroyCard()
    {
        myImage.enabled = false;
    }
}
