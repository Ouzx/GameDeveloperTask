// Oz
using UnityEngine;
using System.Collections.Generic;
public class LevelManager : MonoBehaviour
{
    #region Singelton
    public static LevelManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    #endregion

    public List<PixelToObject> pixelColorMappings = new List<PixelToObject>(1);
    public List<Level> levels = new List<Level>(1);
    private List<GameObject> levelPipes = new List<GameObject>();
    public GameObject levelTemp;
    public int levelNum;

    public void PreviewObjects(int num)
    {
        foreach (PipeProperty var in levels[num].pipes)
        {
            Instantiate(var.prefab, var.pos, Quaternion.identity, levelTemp.transform);
        }
    }

    public void SetLevel()
    {
        foreach (PipeProperty var in levels[levelNum].pipes)
        {
            levelPipes.Add(Instantiate(var.prefab, var.pos, Quaternion.identity));
        }
    }

    public void ClearLevel()
    {
        foreach (GameObject obj in levelPipes) GameObject.Destroy(obj);
        levelPipes.Clear();
    }


    public int GetLevelTime() => levels[levelNum].levelTime;

    public int GetLevelPoint() => levels[levelNum].sucssesPoint;
    public int GetFailTime() => levels[levelNum].failTime;
}
