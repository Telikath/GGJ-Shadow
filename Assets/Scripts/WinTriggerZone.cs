using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTriggerZone : MonoBehaviour
{
    public int numSceneToLoad = 0;
    public AudioSource winSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (winSound != null)
            {
                winSound.Play();
                while (winSound.isPlaying) ;
            }
            SceneManager.LoadScene(numSceneToLoad);
        }
    }
}
