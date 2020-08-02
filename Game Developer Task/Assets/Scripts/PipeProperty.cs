using UnityEngine;
[System.Serializable]
public class PipeProperty {
    public Pipe.PipeType Type;
    public bool Rigidity = false;
    public int solutionNum = 0;
    public Vector3 pos;
    public GameObject prefab;
}