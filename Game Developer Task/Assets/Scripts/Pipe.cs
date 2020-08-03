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
        //StartRotation();

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
        // if (!Rigidity)
        // {
        //     targetRotation = new Vector3(Rotations[rotation], 0, 0);
        // }
        // else
        // {
        //     targetRotation = new Vector3(Rotations[solutionNum], 0, 0);
        // }
    }
    // public Vector3 targetRotation;
    // private float rotatingSpeed = .5f;
    // private bool rotating;
    // public void StartRotation()
    // {
    //     //if (!rotating)
    //         StartCoroutine(Rotate(targetRotation, rotatingSpeed));
    // }

    // private IEnumerator Rotate(Vector3 angles, float duration)
    // {
    //     rotating = true;
    //     Quaternion startRotation = transform.rotation;
    //     Quaternion endRotation = Quaternion.Euler(angles) * startRotation;
    //     for (float t = 0; t < duration; t += Time.deltaTime)
    //     {
    //         transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
    //         yield return null;
    //     }
    //     transform.rotation = endRotation;
    //     rotating = false;
    // }



}