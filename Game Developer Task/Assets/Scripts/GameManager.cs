// Oz
using UnityEngine;

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

    private int activeLevel = 1;
    private void Start() {
        LevelManager.Instance.levelNum = activeLevel;
        LevelManager.Instance.SetLevel();
    }
    private void Update() {
    //     if(LevelManager.Instance.CheckSolution()){
    //         // Pop up end of level menu dont stop game
    //     }
    //     else{//fail level}
    }

    public GameObject fluid;
    public float capsulationTime;
    float temp = 0;

    public void StartGame()
    {
        temp += Time.deltaTime;
        if (temp >= capsulationTime)
            fluid.SetActive(true);
    }   
}
