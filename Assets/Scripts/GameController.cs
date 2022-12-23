using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //The camera
    [SerializeField] private Camera mainCamera;
    private float lastMonster = 0.0f;
    private int monsterPos = 0;
    private int health = 20;
    private float startTime;
    //The player
    [SerializeField] private Skeleton player;
    [SerializeField] private GameObject[] monsters;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI healthNumb;
    //The gameobject where the countdown text will appear
    [SerializeField] private GameObject countDown;
    //A boolean to know if the game is starting
    private bool starting;
    //The pause menu, the resume button and the text that we will change depending on the language
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreNumb;
    [SerializeField] private TextMeshProUGUI resumeText;
    [SerializeField] private TextMeshProUGUI restartText;
    [SerializeField] private TextMeshProUGUI returnText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI timeNumb;
    //Booleans to know if the game is paused and if the game ended
    private bool paused;
    private bool gameEnded;
    //The keycode of the button that will pause the game
    private KeyCode pauseKey;
    void Start()
    {
        //We make the cursor invisible, because we don't need it when we are playing. We also lock it on the window.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        //We change the camera size depending on the resolution
        if (Mathf.Abs((float)PlayerPrefs.GetInt("resolutionW") / PlayerPrefs.GetInt("resolutionH") - 16.0f / 9.0f) < 0.0001f) mainCamera.orthographicSize = 15.76454f;
        else if (Mathf.Abs((float)PlayerPrefs.GetInt("resolutionW") / PlayerPrefs.GetInt("resolutionH") - 16.0f / 10.0f) < 0.0001f) mainCamera.orthographicSize = 17.51459f;
        else if (Mathf.Abs((float)PlayerPrefs.GetInt("resolutionW") / PlayerPrefs.GetInt("resolutionH") - 4.0f / 3.0f) < 0.0001f) mainCamera.orthographicSize = 21.01344f;
        healthNumb.text = health.ToString();
        startTime = Time.fixedTime;
        starting = true;
        paused = false;
        gameEnded = false;
        lastMonster = Time.fixedTime - 6.0f;
        //We change the text depending on the language
        if (PlayerPrefs.GetInt("language") == 0)
        {
            pauseText.text = "GAME PAUSED";
            resumeText.text = "Resume";
            restartText.text = "Restart";
            returnText.text = "Return to main menu";
            healthText.text = "Health:";
            timeText.text = "Score:";
        }
        else if (PlayerPrefs.GetInt("language") == 1)
        {
            pauseText.text = "JUEGO PAUSADO";
            resumeText.text = "Continuar";
            restartText.text = "Reiniciar";
            returnText.text = "Volver al menú principal";
            healthText.text = "Vida:";
            timeText.text = "Puntuación:";
        }
        else if (PlayerPrefs.GetInt("language") == 2)
        {
            pauseText.text = "JOKOA GELDITUA";
            resumeText.text = "Jarraitu";
            restartText.text = "Berrabiarazi";
            returnText.text = "Menu printzipalera itzuli";
            healthText.text = "Bizitza:";
            timeText.text = "Puntuazioa:";
        }
        //We put the player waiting
        player.SetWait(true);
        //Using refraction we find the keycode of the pause
        pauseKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("pause"));
        //We put the final texts in blanc because we don't need them until the end of the game
        scoreText.text = "";
        scoreNumb.text = "";
        //We deactivate the pause menu
        pauseMenu.SetActive(false);
    }


    void Update()
    {
        //We show a countdown to the player
        if (starting)
        {
            if (Time.fixedTime - startTime < 3.0f)
            {
                countDown.GetComponent<TextMeshProUGUI>().text = (3 - (int)(Time.fixedTime - startTime)).ToString();
            }
            else
            {
                countDown.GetComponent<TextMeshProUGUI>().text = "";
                starting = false;
                startTime = Time.fixedTime;
                player.SetWait(false);
            }
        }
        else
        {
            //We pause or unpause the game when the pause key is pressed, putting the player in waiting state. When the game is paused, we make the cursor visible.
            if (Input.GetKeyDown(pauseKey) && !gameEnded)
            {
                paused = !paused;
                Cursor.visible = paused;
                Time.timeScale = System.Convert.ToInt32(!paused);
                player.SetWait(paused);
                pauseMenu.SetActive(paused);
            }
            if (!paused)
            {
                //We save the time that has passed while playing to show it to the player.
                timeNumb.text = ((int)(Time.fixedTime - startTime)).ToString();
                //We want to make the game more challenging the more time the player survives, so we change the spawn time and the enemies that can spawn depending on the time that has passed.
                if (Time.fixedTime - startTime < 24.0f)
                {
                    if ((Time.fixedTime - lastMonster) > 6f)
                    {
                        monsterPos = Random.Range(1, 9);
                        Instantiate(monsters[0], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<Skeleton>().monsterPos = monsterPos;
                        lastMonster = Time.fixedTime;
                    }
                }
                else if (Time.fixedTime - startTime < 60.0f)
                {
                    if ((Time.fixedTime - lastMonster) > 4.5f)
                    {
                        monsterPos = Random.Range(1, 9);
                        Instantiate(monsters[Random.Range(0, 2)], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<Skeleton>().monsterPos = monsterPos;
                        lastMonster = Time.fixedTime;
                    }
                }
                else if (Time.fixedTime - startTime < 123.0f)
                {
                    if ((Time.fixedTime - lastMonster) > 3.5f)
                    {
                        monsterPos = Random.Range(1, 9);
                        Instantiate(monsters[Random.Range(0, 3)], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<Skeleton>().monsterPos = monsterPos;
                        lastMonster = Time.fixedTime;
                    }
                }
                else if (Time.fixedTime - startTime < 163.0f)
                {
                    if ((Time.fixedTime - lastMonster) > 3.0f)
                    {
                        monsterPos = Random.Range(1, 9);
                        Instantiate(monsters[Random.Range(0, 3)], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<Skeleton>().monsterPos = monsterPos;
                        lastMonster = Time.fixedTime;
                    }
                }
                else
                {
                    if ((Time.fixedTime - lastMonster) > 2.5f)
                    {
                        monsterPos = Random.Range(1, 9);
                        Instantiate(monsters[Random.Range(0, 3)], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<Skeleton>().monsterPos = monsterPos;
                        lastMonster = Time.fixedTime;
                    }
                }
            }            
        }        
    }

    //We pause the game when the player alt tabs.
    private void OnApplicationFocus(bool focus)
    {
        if(!focus && !paused && !starting)
        {            
            paused = !paused;
            Cursor.visible = paused;
            Time.timeScale = System.Convert.ToInt32(!paused);
            pauseMenu.SetActive(paused);
            player.SetWait(paused);
        }
    }

    //Function to restart the level
    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    //Function to open the main menu
    public void OpenMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    //Function to resume the game
    public void ResumeGame()
    {
        paused = !paused;
        Cursor.visible = paused;
        Time.timeScale = System.Convert.ToInt32(!paused);
        pauseMenu.SetActive(paused);
        player.SetWait(paused);
    }

    //A function to deal damage to the chests. When it reaches 0 the game ends.
    public void DealDamage(int d)
    {
        if (!gameEnded)
        {
            health -= d;
            if (health <= 0)
            {
                health = 0;
                EndGame();
            }
            healthNumb.text = health.ToString();
        }     
    }

    //Function to end the game
    public void EndGame()
    {
        Time.timeScale = 0.0f;
        player.SetWait(true);
        //We make the cursor visible, activate the game ended state and enable the pause menu
        Cursor.visible = true;
        gameEnded = true;
        pauseMenu.SetActive(true);
        //We change the texts to put the game over message and the score
        if (PlayerPrefs.GetInt("language") == 0)
        {
            pauseText.text = "GAME OVER";
            scoreText.text = "SCORE:";
        }
        else if (PlayerPrefs.GetInt("language") == 1)
        {
            pauseText.text = "HAS PERDIDO";
            scoreText.text = "PUNTUACIÓN:";
        }
        else if (PlayerPrefs.GetInt("language") == 2)
        {
            pauseText.text = "GALDU DUZU";
            scoreText.text = "PUNTUAZIOA:";
        }
        scoreNumb.text = ((int)(Time.fixedTime - startTime)).ToString();
        //We deactivate the resume button
        resumeButton.SetActive(false);
    }


}
