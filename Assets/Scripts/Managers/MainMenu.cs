using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  [SerializeField] AudioClip ButtonPress;

   public void PlayGame()
   {
     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
     SoundManager.instance.PlaySound(ButtonPress);
   }

   public void Quit()
   {
     Application.Quit();
     SoundManager.instance.PlaySound(ButtonPress);
   }

}