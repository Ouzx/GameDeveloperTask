using UnityEngine;

public class PipeClickHandller : MonoBehaviour
{
    private void OnMouseDown()
    {
        transform.root.GetComponent<Pipe>().Rotate();
        FindObjectOfType<AudioManager>().Play("Click");
    }
}