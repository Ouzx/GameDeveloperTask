// Oz
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float force = 10;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, -force, 0));
    }

}
