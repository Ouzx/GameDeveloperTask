// Oz
using System.Collections;
using UnityEngine;
public class Pipe : MonoBehaviour
{

    public enum PipeType
    {
        Rounded,
        Straight,
        Cross,
        Tee,
        Wyee,
        Barrel,
        Capsule
    }
    public PipeType Type;
    public bool Rigidity = false;
    public int solutionNum = 0;
    // Only x axis
    public int[] Rotations = { 0, 90, 180, 270 };
    public int rotation = 0;

    public void Rotate()
    {
        if (rotation < 4) rotation++;
        if (rotation == 4) rotation = 0;
    }
    private void Start()
    {
        rotation = Random.Range(0, 4);
    }
    private void Update()
    {
        if (!Rigidity)
        {
            transform.rotation = Quaternion.Euler(Rotations[rotation], 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(Rotations[solutionNum], 0, 0);
        }
    }


}