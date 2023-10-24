using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MemoryMatchPresenter : MonoBehaviour
{
    #region Singleton
    public static MemoryMatchPresenter Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    #region InspectorFields
    [Header("Set In Inspector")]
    [SerializeField] Transform cardsPanel;
    [SerializeField] Card cardPrefab;
    [SerializeField] TextMeshProUGUI endText, scoreText;
    [SerializeField] GameObject endPanel, startPanel, gamePanel;
    [SerializeField] ParticleSystem winParticle, loseParticle;
    [SerializeField] LevelScriptableObject levelConfig;
    [SerializeField] int howManyInARow = 5;
    #endregion

    #region PrivateFields
    bool canClick = true;
    int howManyCards;
    float initTimer;
    MemoryMatchModel gameModel = new MemoryMatchModel();
    Sprite[] cardFrontSprites, cardBackSprites;
    Card firstCard, secondCard;
    List<Card> cardInstantList = new List<Card>();
    #endregion

    #region UnityMethods
    private void Start()
    {
        SetupLevelConfig();

        gameModel.onRightChoiceEvent += CorrectChoice;
        gameModel.onWrongChoiceEvent += IncorrectChoice;
        gameModel.onWinEvent += GameWon;
        gameModel.onChoiceChangeEvent += ChangeScoreUI;
    }
    #endregion

    #region PrivateMethods
    private void SetupLevelConfig()
    {
        levelConfig ??= CrossSceneData.levelConfig;

        cardBackSprites = levelConfig.backSprites;
        cardFrontSprites = levelConfig.frontSprites;
        howManyCards = levelConfig.howManyCards;
        initTimer = levelConfig.timeToComplete;
    }

    private void MakeButtons(Dictionary<int, int> ButtonsToMake)
    {
        int i = 0;
        foreach (int key in ButtonsToMake.Keys)
        {
            Card btn = Instantiate(cardPrefab);
            cardInstantList.Add(btn);

            btn.name = "" + i;
            btn.transform.SetParent(cardsPanel, false);
            btn.SetSprites(cardBackSprites[0], cardFrontSprites[ButtonsToMake[key]]);
            btn.Key = key;

            i++;
        }
        SortMatrix(cardsPanel, cardInstantList.Count, 1.2f);
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
        yield return new WaitForSeconds(0.75f);
        gameModel.CheckForMatch();
        canClick = true;
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
        StartCoroutine(InstantiateParticle(loseParticle));
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

    private void SortMatrix(Transform father, int howMany, float tileSize)
    {
        int rows = howMany / howManyInARow;
        int i = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < howManyInARow; col++)
            {
                print(rows + " " + howMany + " " + howManyInARow + " " + col + " " + row);
                float posX = col * tileSize;
                float posY = row * tileSize;
                cardInstantList[i].transform.position = new Vector2(posX, posY);
                i++;
            }
        }
        if (howMany % howManyInARow > 0)
        {
            for (int col = 0; col < howMany % howManyInARow; col++)
            {
                print(rows + " " + howMany + " " + howManyInARow + " " + col + " " + rows);
                float posX = col * tileSize;
                float posY = rows * tileSize;
                cardInstantList[i].transform.position = new Vector2(posX, posY);
                i++;
            }
        }

        float gridW = howManyInARow * tileSize;
        float gridH = rows * tileSize;
        father.position = new Vector2(-((gridW / 2) - (tileSize / 2)), -((gridH / 2) - (tileSize / 2)));
    }

    #endregion

    #region PublicMethods
    public void StartGameBtn()
    {
        MakeButtons(gameModel.MakeCards(howManyCards));
        TimeManager.Instance.AddTimer(initTimer, GameLost, false);
        gamePanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void PickBtn(int key)
    {
        if (!canClick) return;

        cardInstantList[key].TurnCard(true);
        if (gameModel.IsFirstPick(key))
        {
            firstCard = cardInstantList[key];
        }
        else
        {
            secondCard = cardInstantList[key];
            StartCoroutine(CheckCardsCoroutine());
        }
    }

    #endregion
}