using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//use this enum to change the function of this particular button
public enum ButtonFunction : byte
{
	transition = 1,
    text  = 2,
	closetext = 3,
    startGame = 4,
    quit =5
	// if more functions need to be added to the button you can put them here
};

public class VRButton : MonoBehaviour,Interactable
{
	public Slider slider;
	private Image spr;
	[SerializeField] private bool active;
	public float decrement;
	private float timer;
	public float timerInitial;
	public ButtonFunction buttonFunction;
	public GameObject newLocation;
	public GameObject pl;
	public bool wasLookedAt = false;
	public float fadeIncrement;
    public VideoController[] videoControllerToTurnOff;
    public AudioPlayer audioPlayerToTurnOff;
	
	void Start()
	{
		spr = GetComponent<Image>();
		timer = timerInitial;
        if(slider!=null)
        slider.gameObject.SetActive(false);
    }

	public void TransitionToNewSphere()
	{
		// go to a new location or as some would say "A WHOLE NEW WORLD!!!!!!!!!!"
		pl.transform.position = newLocation.transform.position;
		timer = timerInitial;

        for(int i =0;i<videoControllerToTurnOff.Length;i++)
        {
            if(videoControllerToTurnOff[i] != null)
            {
                videoControllerToTurnOff[i].CloseAndEndVideo(); //Make sure the video is turned off when you leave
            }
        }

        if(audioPlayerToTurnOff!=null)
        {
            audioPlayerToTurnOff.StopPlaying(); //Make sure the audio is turned off when you leave
        }
	}

    public void BeingInteractedWith()
    {
        if(!slider.IsActive())
        {
            slider.gameObject.SetActive(true);
        }
        slider.value += 0.05f;
        if(slider.value>=1.0f)
        {
			//Do things
			switch (buttonFunction)
			{
				case ButtonFunction.transition:
					TransitionToNewSphere();
					break;
				case ButtonFunction.text:
					FadeIn();
					break;
				case ButtonFunction.closetext:
					FadeOut();
					break;
                case ButtonFunction.startGame:
                    SceneManager.LoadScene("Game");
                    break;
                case ButtonFunction.quit:
                    Application.Quit();
                    break;
            }
           
        }
    }

    public void LookedAway()
    {
        slider.value = 0.0f;
        slider.gameObject.SetActive(false);       
    }

	public void FadeIn()
	{
		for (float i = 0; i < spr.color.a; i += fadeIncrement)
		{
			spr.color = new Color(1, 1, 1, i);
		}
	}
	public void FadeOut()
	{
		for (float i = 1; i > spr.color.a; i -= fadeIncrement)
		{
			spr.color = new Color(1, 1, 1, i);
		}
	}
}