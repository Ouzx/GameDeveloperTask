// Oz
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Level
{
    public Texture2D mapTexture;

    public List<PipeProperty> pipes = new List<PipeProperty>(1);
    public int levelTime;
    public int sucssesPoint;
    public int failTime;
    public bool playType;
    public bool completed;
    public bool isLocked = true;

}