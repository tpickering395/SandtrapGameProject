using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Class for playing sounds and other helper functions for UI
public class UIFlavor : MonoBehaviour
{
    // All fields assigned via editor.
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;

    public AudioSource channel;

    public AudioClip playWav;
    public AudioClip optionsWav;
    public AudioClip quitWav;
    public AudioClip negativeWav;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(delegate { playUISoundEvent(0); });
        optionsButton.onClick.AddListener(delegate { playUISoundEvent(1); });
        quitButton.onClick.AddListener(delegate { playUISoundEvent(2); });
    }

    void playUISoundEvent(byte id)
    {
        switch(id)
        {
            // PlayButton
            case 0:
                channel.PlayOneShot(playWav);
                break;

            // Options Button
            case 1:
                channel.PlayOneShot(optionsWav);
                break;

            // Quit Button
            case 2:
                channel.PlayOneShot(quitWav);
                break;

            // Catch all
            default:
                channel.PlayOneShot(negativeWav);
                break;
        }
    }
}
