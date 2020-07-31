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

    private void OnMouseDown () {
        if(Type != PipeType.Rounded)transform.root.transform.Rotate(RotateAmount, 0 , 0);
        else transform.root.transform.Rotate(0, RotateAmount , 0);
       
    }


}