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
    public int[] Rotations = { 0, 90, 180, 270 };
    public int rotation = 0;

    private void OnMouseDown()
    {
        if (rotation < 4) rotation++;
        if (rotation == 4) rotation = 0;
    }
    private void Start()
    {
        rotation = Rotations[Random.Range(0, 4)];
    }
    private void Update()
    {
        if (!Rigidity)
        {
            if (Type != PipeType.Rounded) transform.root.transform.rotation = Quaternion.Euler(rotation, 0, 0);
            else transform.root.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
        else
        {
            if (Type != PipeType.Rounded) transform.root.transform.rotation = Quaternion.Euler(Rotations[solutionNum], 0, 0);
            else transform.root.transform.rotation = Quaternion.Euler(0, Rotations[solutionNum], 0);
        }
    }


}