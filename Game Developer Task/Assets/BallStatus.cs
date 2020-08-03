// Oz
using UnityEngine;

public class BallStatus : MonoBehaviour
{
  
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Ball"){
            GameManager.Instance.WinLevel();
        }
    }
}
