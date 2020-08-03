// Oz
using UnityEngine;

public class OpenCapsule : MonoBehaviour
{

    public static OpenCapsule Instance;
    private void Awake()
    {
        Instance = this;

    }
    public void Capsulete()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
