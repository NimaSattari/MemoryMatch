using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    [SerializeField] int key;
    public int Key
    {
        get { return key; }
        set { key = value; }
    }

    [SerializeField] SpriteRenderer myImage;
    public SpriteRenderer MyImage
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

    private void OnMouseDown()
    {
        MemoryMatchPresenter.Instance.PickBtn(Key);
    }
}
