using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int level = 0;
    public Transform MainCamera;
    public Rigidbody2D playerRb;
    public Transform Player;
    public Transform borderRight;
    public Transform borderLeft;
    public TextMeshProUGUI infoText;
    Coroutine textRoutine;
    public bool inputLocked = false;

    //public Rigidbody2D playerRb;
    // Start is called before the first frame update
    void Awake()
    {
        
        if (instance == null)
            instance = this;
        else
        {

            Destroy(gameObject);
        return;
        }

        NextLevel();
    }
    private void Start()
    {
        StartCoroutine(GameStartTextFlow());
    }

    //void InitializeLevel()
    //{
    //    MainCamera.position = new Vector3(0, 2, -10);
    //    playerRb.velocity = Vector2.zero;
    //    Player.position = new Vector3(-8, 0, 0);
    //    borderRight.position = new Vector3(9, 9, 0);
    //    borderLeft.position = new Vector3(-9, 9,0 );


    //}
    public void NextLevel()
    {
        level++;
        ShowLevelText("Level " + level, 1f);
        //int nextLevelLength = 40;
        playerRb.velocity = Vector2.zero;

        switch (level)
        {
            case 1:
                SetPositions(new Vector3(0, 2, -10), new Vector2(-8, 0), new Vector2(-9, 9), new Vector2(9, 9));
                break;
            case 2:
                SetPositions(new Vector3(40, 2, -10), new Vector2(32, 0), new Vector2(31,9),new Vector2(49,9));
                break;

            case 3:
                SetPositions(new Vector3(80,2 , -10), new Vector2(72, 0),new Vector2(71,9), new Vector2(89,9));
                break;
            case 4:
                SetPositions(new Vector3(120, 2, -10), new Vector2(112, 0), new Vector2(111, 9), new Vector2(129, 9));
                break;
            case 5:
                SetPositions(new Vector3(160, 2, -10), new Vector2(152, 0), new Vector2(151, 9), new Vector2(169, 9));
                break;
        }
    }
    public void StartOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void SetPositions(Vector3 camPos, Vector2 playerPos,Vector2 borderLeftPos, Vector2 borderRightPos)
    {
        MainCamera.position = camPos;
        playerRb.velocity = Vector2.zero;
        playerRb.position = playerPos;
        borderRight.position = borderRightPos;
        borderLeft.position = borderLeftPos;
        Player.GetComponent<Player>()
         .StartCoroutine(Player.GetComponent<Player>().ResetPlayerState());
    }

    public void ShowLevelText(string text,float duration)
    {
        if (textRoutine != null)
            StopCoroutine(textRoutine);

        textRoutine = StartCoroutine(ShowTextRoutine(text, duration));
    }
    IEnumerator ShowTextRoutine(string text, float duration)
    {
        inputLocked = false;
        infoText.text = text;
        infoText.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        infoText.gameObject.SetActive(false);
        inputLocked = false;
    }
    IEnumerator GameStartTextFlow()
    {
        ShowLevelText("Sanity Killer", 2f);
        yield return new WaitForSeconds(2f);

        ShowLevelText("Level "+level, 2f);
        yield return new WaitForSeconds(2f);

        infoText.gameObject.SetActive(false);
    }

}
