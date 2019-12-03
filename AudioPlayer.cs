using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour,Interactable
{
    private AudioSource audioSource;
    [SerializeField]
    private float max_time;
    [SerializeField]
    private UnityEngine.UI.Image image;

    // fields
    private float timer = 0f;

    public void BeingInteractedWith()
    {
        if (timer > max_time)
        {
            timer = max_time;
            if (audioSource.isPlaying)
                audioSource.Stop();
            else
                audioSource.Play();
        }
        else if (timer < max_time) timer += Time.deltaTime;
    }

    public void LookedAway()
    {
        if (timer == max_time) timer = 0f;
        else if (timer < 0f) timer = 0f;
        else if (timer > 0f) timer -= Time.deltaTime;
    }

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        image.color = Color.white * (1 - (timer / max_time)) + Color.black * (timer / max_time);
    }

    public void StopPlaying()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
