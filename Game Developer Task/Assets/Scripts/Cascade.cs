// Oz
using UnityEngine;
using UnityEngine.Animations;
public class Cascade : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    float temp = 0;
    void Update()
    {
        animator.SetBool("timeOut", ProgressBar.timeOut);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("capsule"))
        {
           if(ProgressBar.timeOut) GameManager.Instance.StartGame();

        }
    }
}
