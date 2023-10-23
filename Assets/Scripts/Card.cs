using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] DOTweenActions dOTweenActions;
    [SerializeField] int key;
    public int Key
    {
        get { return key; }
        set { key = value; }
    }

    [SerializeField] SpriteRenderer backImage;
    public SpriteRenderer BackImage
    {
        get { return backImage; }
    }

    [SerializeField] SpriteRenderer frontImage;
    public SpriteRenderer FrontImage
    {
        get { return frontImage; }
    }

    public void SetSprites(Sprite back, Sprite front)
    {
        backImage.sprite = back;
        frontImage.sprite = front;
        TurnCard(false);
    }

    public void TurnCard(bool turnFront)
    {
        if(turnFront)
        {
            dOTweenActions.DoAnimation();
        }
        else
        {
            dOTweenActions.DoAnimationBackward();
        }
    }

    public void DestroyCard()
    {
        backImage.enabled = false;
        frontImage.enabled = false;
    }

    private void OnMouseDown()
    {
        MemoryMatchPresenter.Instance.PickBtn(Key);
    }
}
