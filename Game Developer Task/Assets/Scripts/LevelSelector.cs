using UnityEngine;
using TMPro;
public class LevelSelector : MonoBehaviour
{
    [HideInInspector]
    public int levelNum = 0;
    private void Start()
    {
        if (transform.childCount != 0)
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (levelNum + 1).ToString();
    }
    public void Locked()
    {
        FindObjectOfType<AudioManager>().Play("Fail");
        
    }
    public void OpenLevel()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        GameManager.Instance.GetCustomLevel(levelNum);
    }
}