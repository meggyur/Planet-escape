using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip finish;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem seceed;

     AudioSource aus;       

     bool isTransitioning = false;
     bool collisionDisable = false;

    void Start() 
    {
        aus = GetComponent<AudioSource>();             
    }

    void Update()
    {
        RespontTODebugKeys();
    }

    void RespontTODebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }

    void OnCollisionEnter(Collision other) 
    {
       if (isTransitioning || collisionDisable) { return; }

       switch (other.gameObject.tag)
       {
            case "Finish":
                StartLoadingNextLevel();
                break;
            case "Friendly":
                Debug.Log("Nothing hapenned");
                break;
            default:                
                StartCrashSequence();
                break;
       }
    }

    void StartCrashSequence()
    {       
        isTransitioning = true;
        aus.Stop();
        aus.PlayOneShot(crash);
        explosion.Play();
        GetComponent<Movement>().enabled = false ;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    void StartLoadingNextLevel()
    {        
        isTransitioning = true;
        aus.Stop();
        aus.PlayOneShot(finish);
        seceed.Play();            
        GetComponent<Movement>().enabled = false ;
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }   
}
