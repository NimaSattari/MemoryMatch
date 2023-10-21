using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryMatchPresenter : MonoBehaviour
{
    [Header("Set In Inspector")]
    [SerializeField] Transform cardsPanel;
    [SerializeField] Card cardPrefab;
    [SerializeField] List<Card> cardInstantList = new List<Card>();
    [SerializeField] TextMeshProUGUI endText, scoreText;
    [SerializeField] GameObject endPanel, startPanel, gamePanel;
    [SerializeField] ParticleSystem winParticle, loseParticle;
    [SerializeField] LevelScriptableObject levelConfig;

    bool canClick = true;
    int howManyCards;
    float initTimer;
    MemoryMatchModel gameModel = new MemoryMatchModel();
    Sprite[] cardFrontSprites, cardBackSprites;
    Card firstCard, secondCard;

    private void Awake()
    {
        levelConfig = CrossSceneData.levelConfig;
        CrossSceneData.levelConfig = null;
        cardBackSprites = levelConfig.backSprites;
        cardFrontSprites = levelConfig.frontSprites;
        howManyCards = levelConfig.howManyCards;
        initTimer = levelConfig.timeToComplete;
        //cardBackSprites = Resources.LoadAll<Sprite>("BackSprite");
        //cardFrontSprites = Resources.LoadAll<Sprite>("FrontSprites");
    }

    private void Start()
    {
        gameModel.onRightChoiceEvent += CorrectChoice;
        gameModel.onWrongChoiceEvent += IncorrectChoice;
        gameModel.onWinEvent += GameWon;
        gameModel.onChoiceChangeEvent += ChangeScoreUI;
    }

    public void StartGameBtn()
    {
        MakeButtons(gameModel.MakeCards(howManyCards));
        TimeManager.Instance.AddTimer(initTimer, GameLost, false);
        gamePanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void PickBtn(int Index)
    {
        if (!canClick) return;

        cardInstantList[Index].TurnCard(true);
        if (gameModel.IsFirstPick(Index))
        {
            firstCard = cardInstantList[Index];
        }
        else
        {
            secondCard = cardInstantList[Index];
            StartCoroutine(CheckCardsCoroutine());
        }
    }

    private void CorrectChoice()
    {
        firstCard.DestroyCard();
        secondCard.DestroyCard();
    }

    private void IncorrectChoice()
    {
        firstCard.TurnCard(false);
        secondCard.TurnCard(false);
    }

    private IEnumerator CheckCardsCoroutine()
    {
        canClick = false;
        yield return new WaitForSeconds(1f);
        gameModel.CheckForMatch();
        canClick = true;
    }

    private void MakeButtons(Dictionary<int, int> ButtonsToMake)
    {
        int i = 0;
        foreach(int key in ButtonsToMake.Keys)
        {
            Card btn = Instantiate(cardPrefab);
            cardInstantList.Add(btn);

            btn.name = "" + i;
            btn.transform.SetParent(cardsPanel, false);
            btn.SetSprites(cardBackSprites[0], cardFrontSprites[ButtonsToMake[key]]);
            btn.MyButton.onClick.AddListener(() => GetComponent<MemoryMatchPresenter>().PickBtn(key));

            i++;
        }
    }

    private void GameWon(int Choice)
    {
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
        endText.text = "You Won With: " + Choice + " Moves";
        StartCoroutine(InstantiateParticle(winParticle));
    }

    private void GameLost()
    {
        gamePanel.SetActive(false);
        endPanel.SetActive(true);
        endText.text = "You Lost";
        StartCoroutine(InstantiateParticle(winParticle));
    }

    IEnumerator InstantiateParticle(ParticleSystem particleToInstantiate)
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(particleToInstantiate.gameObject, new Vector2(Random.Range(-5f, 5f), Random.Range(-2f, 2f)), Quaternion.identity);
    }

    private void ChangeScoreUI(int score)
    {
        scoreText.text = score.ToString();
    }
}