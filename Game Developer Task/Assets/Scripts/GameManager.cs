// Oz
using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singelton
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);
        foreach (Transform child in LevelManager.Instance.levelTemp.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    #endregion
    public GameObject Slider;
    public TextMeshProUGUI score;
    public TextMeshProUGUI level;
    public GameObject Menu;
    public GameObject LevelList;
    public GameObject locked;
    public GameObject open;


    private int activeLevel = 0;
    private bool isWin = false;

    public void SetGame()
    {
        Menu.GetComponent<Pause>().ResetMenu();
        foreach (Transform child in LevelList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        LevelManager.Instance.ClearLevel();
        LevelManager.Instance.SetLevel(activeLevel);
        LevelManager.Instance.SetLevel();
        Slider.GetComponent<ProgressBar>().maxTime = LevelManager.Instance.GetLevelTime();
        Slider.GetComponent<ProgressBar>().ResetProgressBar();
        level.text = LevelManager.Instance.GetLevelName();
        isWin = false;
        toFail = 0;
        failKey = false;
        toStart = 0;
    }
    private void Start()
    {
        Menu.GetComponent<Pause>().StartGame();
    }

    public void WinLevel()
    {
        FindObjectOfType<AudioManager>().Play("Succses");
        isWin = true;
        if (LevelManager.Instance.levels[activeLevel].completed)
        {
            Menu.GetComponent<Pause>().AlreadyWin();
        }
        else
        {
            score.text = (Convert.ToInt32(score.text) + LevelManager.Instance.GetLevelPoint()).ToString();
            LevelManager.Instance.Win();
            Menu.GetComponent<Pause>().Win();
        }

    }

    float toFail = 0;
    bool failKey = false;
    public void FailLevel()
    {
        if (!failKey)
        {
            toFail += Time.deltaTime;
            if (toFail >= LevelManager.Instance.GetFailTime() && !isWin)
            {
                FindObjectOfType<AudioManager>().Play("Fail");
                failKey = true;
                Menu.GetComponent<Pause>().Fail();

            }
        }
    }

    public void NextLevel()
    {
        activeLevel++;
        SetGame();
    }

    public void ListLevels()
    {
        LevelManager.Instance.ListLevels(LevelList, locked, open);
    }
    public void GetCustomLevel(int customLevel)
    {
        if (customLevel < LevelManager.Instance.GetLevelCount())
        {
            activeLevel = customLevel;
            SetGame();
        }
    }

    public float capsulationTime = 1.7f;
    float toStart = 0;

    public void StartGame()
    {
        toStart += Time.deltaTime;
        if (toStart >= capsulationTime)
        {
            OpenCapsule.Instance.Capsulete();
            FailLevel();
        }
    }
}
