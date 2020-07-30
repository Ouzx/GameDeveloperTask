// Oz
using System.Collections;
using UnityEngine;
public class Pipe : MonoBehaviour {

    public enum PipeType {
        Rounded,
        Straight,
        Cross,
        Tee,
        Wye
    }
    public PipeType Type;
    public bool Rigidity;

    // Only X axis 
    private int RotateAmount = 90;

    float RotateSpeed = 1000;

    // SOUND.CS

    private void OnMouseDown () {
        if(Type != PipeType.Rounded)transform.root.transform.Rotate(90, 0 , 0);
        else transform.root.transform.Rotate(0, 90 , 0);

    }

}