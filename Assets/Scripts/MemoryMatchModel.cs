using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryMatchModel
{
    #region PrivateFields
    Dictionary<int,int> cardPlaceValue = new Dictionary<int,int>();
    List<int> cardValues = new List<int>();
    bool firstChoice;
    int firstIndex, secondIndex;
    int CorrectChoice;
    int allGameChoice;
    int howManyClicked;
    RandomNumberArrayGenerator randomNumberGenerator = new RandomNumberArrayGenerator();
    #endregion

    #region PublicFields
    public event Action onRightChoiceEvent, onWrongChoiceEvent;
    public delegate void OnGameEndDelegate(int choices);
    public event OnGameEndDelegate onWinEvent;
    public delegate void OnChoiceChangeDelegate(int score);
    public event OnChoiceChangeDelegate onChoiceChangeEvent;
    #endregion

    #region PrivateMethods

    /// <summary>
    /// Checks if all cards have matched.
    /// </summary>
    private void CheckWin()
    {
        CorrectChoice++;
        onChoiceChangeEvent?.Invoke(CorrectChoice);
        if (CorrectChoice == allGameChoice)
        {
            onWinEvent?.Invoke(howManyClicked);
        }
    }

    #endregion

    #region PublicMethods

    /// <summary>
    /// returns a dictionary of card's place and spriteInt.
    /// </summary>
    public Dictionary<int, int> MakeCards(int howManyCards)
    {
        allGameChoice = howManyCards / 2;
        cardValues.AddRange(randomNumberGenerator.GenerateRandomIntegersWithoutRepeat(howManyCards / 2, (howManyCards / 2)));
        cardValues.AddRange(randomNumberGenerator.GenerateRandomIntegersWithoutRepeat(howManyCards / 2, (howManyCards / 2)));
        for (int i = 0; i < howManyCards; i += 2)
        {
            cardPlaceValue.Add(i, cardValues[i]);
            cardPlaceValue.Add(i + 1, cardValues[i + 1]);
        }
        return cardPlaceValue;
    }

    /// <summary>
    /// Checks if this is the first card getting picked.
    /// </summary>
    public bool IsFirstPick(int key)
    {
        if (!firstChoice)
        {
            firstChoice = true;
            firstIndex = cardPlaceValue[key];
            return true;
        }
        else
        {
            secondIndex = cardPlaceValue[key];
            howManyClicked++;
            return false;
        }
    }

    /// <summary>
    /// Checks if two cards have the same SpriteInt and excutes right events.
    /// </summary>
    public void CheckForMatch()
    {
        firstChoice = false;
        if (firstIndex == secondIndex)
        {
            onRightChoiceEvent?.Invoke();
            CheckWin();
        }
        else
        {
            onWrongChoiceEvent?.Invoke();
        }
    }
    #endregion
}