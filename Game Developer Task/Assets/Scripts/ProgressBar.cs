// Oz
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    // private ParticleSystem ps;
    [HideInInspector]
    public float maxTime = 3;
    public static bool timeOut = false;
    private void Awake()
    {
        // ps = GameObject.Find("ProgressParticle").GetComponent<ParticleSystem>();
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        slider.maxValue = maxTime;
        slider.value = slider.maxValue;
        //if(!ps.isPlaying) ps.Play();
    }

    private void Update()
    {
        slider.value -= Time.deltaTime;
        if(slider.value == 0) {
            // ps.Stop();
            timeOut = true;
        }
    }
    public void ResetProgressBar(){
        timeOut = false;
        slider.maxValue = maxTime;
        slider.value = slider.maxValue;
        //if(!ps.isPlaying) ps.Play();
    }
}
