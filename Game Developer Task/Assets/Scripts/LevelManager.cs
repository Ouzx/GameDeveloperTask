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
    public void SetLevel(int _levelNum)
    {
        if (_levelNum < levels.Count)
        {
            levelNum = _levelNum;
            levels[levelNum].isLocked = false;
        }

    }
    public void SetLevel()
    {
        foreach (PipeProperty var in levels[levelNum].pipes)
        {
            GameObject obj = Instantiate(var.prefab, var.pos, Quaternion.identity);
            Pipe temp = obj.GetComponent<Pipe>();
            if (temp != null)
            {
                temp.Rigidity = var.Rigidity;
                temp.Type = var.Type;
                temp.solutionNum = var.solutionNum;
                temp.playType = levels[levelNum].playType;
            }
            levelPipes.Add(obj);
        }
    }

    public void ListLevels(GameObject menu, GameObject locked, GameObject open)
    {
        foreach (Level level in levels)
        {
            if (level.isLocked)
            {
                Instantiate(locked, new Vector3(), Quaternion.identity, menu.transform);
            }
            else
            {
                Instantiate(open, new Vector3(), Quaternion.identity, menu.transform).GetComponent<LevelSelector>().levelNum = levels.IndexOf(level);
            }
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
    public string GetLevelName() => (levelNum + 1).ToString();
    public int GetLevelCount() => levels.Count;
    public void Win()
    {
        levels[levelNum].completed = true;
    }
}
