// Oz
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singelton
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        foreach (Transform child in LevelManager.Instance.levelTemp.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    #endregion

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
