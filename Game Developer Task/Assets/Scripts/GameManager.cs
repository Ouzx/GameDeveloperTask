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
    public TextMeshPro score;

    private int activeLevel = 1;
    private bool isWin = false;
    private void Start()
    {
        LevelManager.Instance.levelNum = activeLevel;
        LevelManager.Instance.SetLevel();
        Slider.GetComponent<ProgressBar>().maxTime = LevelManager.Instance.GetLevelTime();
    }

    private void Update()
    {

        //     if(LevelManager.Instance.CheckSolution()){
        //         // Pop up end of level menu dont stop game
        //     }
        //     else{//fail level}
    }
    public void WinLevel()
    {
        FindObjectOfType<AudioManager>().Play("Succses");
        score.text = (Convert.ToInt32(score.text) + LevelManager.Instance.GetLevelPoint()).ToString();
        Debug.Log("win");
        isWin = true;
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
                Debug.Log("fail");
                FindObjectOfType<AudioManager>().Play("Fail");
                failKey = true;
            }
        }
    }

    public GameObject fluid;
    public float capsulationTime;
    float toStart = 0;

    public void StartGame()
    {
        toStart += Time.deltaTime;
        if (toStart >= capsulationTime)
        {
            fluid.SetActive(true);
            FailLevel();
        }
    }
}
