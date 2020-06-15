using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject ContinueButton;
    public GameObject NextLevelButton;

    public GameObject PlayPanel;
    public GameObject InGamePanel;

    public GameObject CurrentLevel;

    public Text ToLevelText;
    public Text FromLevelText;
    public Text CurrentLevelText;
    public Text LevelCompletedText;
    public Text CurrentLevelInfoText;

    public bool isActiveGame = false;
    public bool isLevelUp = false;

    public GameManager GameManager;

    private void Start()
    {
        TaptoTextsAnimation();
        SetCurrentLevelText();
        SetBarLevelTexts();
    }

    public void StartGame()
    {
        isActiveGame = true;
        PlayPanel.SetActive(false);
    }

    public void TaptoTextsAnimation()
    {
        StartButton.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        ContinueButton.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        NextLevelButton.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    public void ShowLevelCompletedText()
    {
        LevelCompletedText.gameObject.SetActive(true);
    }

    public void SetCurrentLevelText()
    {
        int currentLevelNo = PlayerPrefs.GetInt("LEVELNUM") + 1;
        CurrentLevelText.text = "Level " + currentLevelNo.ToString() + " (13 pt)";
        CurrentLevelInfoText.text = "LEVEL\n" + currentLevelNo.ToString();
        LevelCompletedText.text = "Level " + currentLevelNo.ToString() + "\ncompleted !";
    }

    public void GetNextLevel()
    {
        RestartGame();
        GameManager.UpdateLevel();
        SetCurrentLevelText();
        SetBarLevelTexts();
        NextLevelButton.SetActive(false);
        LevelCompletedText.gameObject.SetActive(false);
    }

    public void SetBarLevelTexts()
    {
        int currentLevelNo = PlayerPrefs.GetInt("LEVELNUM") + 1;
        FromLevelText.text = currentLevelNo.ToString();
        int toLevelNo = currentLevelNo + 1;
        ToLevelText.text = toLevelNo.ToString();
    }

    public void ShowContinueGame()
    {
        isLevelUp = false;
        CurrentLevel.SetActive(true);
        ContinueButton.SetActive(true);
        GameManager.UpdateLevel();
    }

    public void RestartGame()
    {
        isActiveGame = true;
        BarController.bar.ResetBar();
        CurrentLevel.SetActive(false);
        ContinueButton.SetActive(false);
        GameManager.ResetObjectOnScene();
        GameManager.PrepareGame();
        SetCurrentLevelText();
        SetBarLevelTexts();
    }
}
