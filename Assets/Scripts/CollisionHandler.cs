using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

  
  [SerializeField] Material landingMaterial;
  [SerializeField] float levelLoadDelay = 4f;
  [SerializeField] float levelCrashDelay = 2f;
  [SerializeField] AudioClip crashSound;
  [SerializeField] AudioClip victorySound;
  [SerializeField] ParticleSystem crashParticle;
  [SerializeField] ParticleSystem victoryParticle;

  AudioSource audioSource;
 

  bool isTransitioning = false;
  bool collisionDisabled = false;

  void Start() 
  {
    
    audioSource = GetComponent<AudioSource>();
  }

  void Update() 
  {
    ResopondToDebugGeys();
  }

  private void OnCollisionEnter(Collision other) 
   {
       if(isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag) 
        {
          case "Finish": 
            StarVictorySequence();
            break;
          case "Friendly": 
            break; 
          default:
            StartCrashSequence();
            break;
        }

        
   }
    //When player hitted the ground, collison handler activates a sequence that ends Movement Component. Then, we
    // Invoked ReloadLevel Method after 1 second so player can see its failure
  void StartCrashSequence() 
    {

     audioSource.Stop();  
     isTransitioning = true;
     audioSource.PlayOneShot(crashSound);
     crashParticle.Play();

     GetComponent<Movement>().enabled = false;
     Invoke ("ReloadLevel", levelCrashDelay);

     
     
    }
  void StarVictorySequence() 
    {
      audioSource.Stop();  
      isTransitioning = true; 
      audioSource.PlayOneShot(victorySound);
      victoryParticle.Play();

      GetComponent<Movement>().enabled = false;
      Invoke ("LoadNextLevel", levelLoadDelay);
      ChangeMaterialColor(landingMaterial, Color.green);
    }

     public void ChangeMaterialColor (Material mat, Color color)
     {
         
         mat.color = color;
        
     }
     
  //Build Settingsteki sahne indexine göre o sırada oynanan bölümü tekrarlıyor. 
  // This method will reload taht player currently playig
  void ReloadLevel()
   {

    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    SceneManager.LoadScene(currentSceneIndex);
   }

  // Build settingsteki sahne indexine göre bir üst metotta bulunan sayının üstüne bir ekleyip, sonraki sahneye geçiş yapmaya yarıyor.
  // eğer son sahneye gelmiş ise, 0. sahneye dönüyor.
  void LoadNextLevel()
   {

    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;
    if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
    {
      nextSceneIndex = 0;
    }
    ChangeMaterialColor(landingMaterial, Color.red);
    SceneManager.LoadScene(nextSceneIndex);
   }
   void ResopondToDebugGeys()
  {
    if (Input.GetKeyDown(KeyCode.L))
          {
            LoadNextLevel();
           
          }
    else if (Input.GetKeyDown(KeyCode.C))
          {
           collisionDisabled = !collisionDisabled;
           Debug.Log("Çarpışma modu");
          }      
  }

  
}
