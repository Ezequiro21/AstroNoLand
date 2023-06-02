using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
  //  public Slider Hp;
    public Slider OTwo;
  //public AudioSource music;
   // public AudioClip mSound;
    public Character characterInfo;
   // public GameObject deadPanel;
    //public GameObject pausePanel;
    //private bool pause;
    //  public Image powerUp;
  
    // Start is called before the first frame update
    void Start()
    {
        // Hp.maxValue = characterInfo.life;
        OTwo.maxValue = characterInfo.oxygen;
        //  powerUp.fillAmount = 0;
       // deadPanel.SetActive(false);
       // pausePanel.SetActive(false);
        //pause = false;
        //Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
       // Hp.value = characterInfo.life; //sacar el 1 y poner la vida
        OTwo.value = characterInfo.oxygen;
        //  powerUp.fillAmount = characterInfo.durationTime;
       /* if (characterInfo.life <= 0)
        {
            deadPanel.SetActive(true);
            Time.timeScale = 0;
            music.Stop();
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) && characterInfo.life >= 0)
        {
            pause = !pause;
            pausePanel.SetActive(pause);
            if (pause == true)
            {
                Time.timeScale = 0f;
                music.Stop();
                music.clip = mSound;
            }
            else
            {
                Time.timeScale = 1f;
                music.Play();
                music.clip = mSound;
            }
            
        }*/
    }

  /*  public void PauseOff() // esto se usa para el boton "CONTINUE"
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        music.Play();
    }
   */
}
