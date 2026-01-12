using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int level = 0;
    public Transform MainCamera;
    public Rigidbody2D playerRb;
    public Transform Player;
    public Transform borderRight;
    public Transform borderLeft;
    
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

        
        InitializeLevel();
    }

    void InitializeLevel()
    {
        MainCamera.position = new Vector3(0, 0, -10);
        playerRb.velocity = Vector2.zero;
        Player.position = new Vector3(-8, 0, 0);
        borderRight.position = new Vector3(9, 9, 0);
        borderLeft.position = new Vector3(-9, 9,0 );
    }
    public void StartOver()
    {
        level = 0;
        InitializeLevel();
    }
    public void NextLevel()
    {
        level++;
        //int nextLevelLength = 40;

        switch (level)
        {
            case 1:
                SetPositions(new Vector3(40, 0, -10), new Vector2(32, 0), new Vector2(31,9),new Vector2(49,9));
                break;

            case 2:
                SetPositions(new Vector3(80,0 , -10), new Vector2(72, 0),new Vector2(71,9), new Vector2(89,9));
                break;
            case 3:
                SetPositions(new Vector3(120, 0, -10), new Vector2(112, 0), new Vector2(111, 9), new Vector2(129, 9));
                break;
        }
    }
    void SetPositions(Vector3 camPos, Vector2 playerPos,Vector2 borderLeftPos, Vector2 borderRightPos)
    {
        MainCamera.position = camPos;
        playerRb.velocity = Vector2.zero;
        playerRb.position = playerPos;
        borderRight.position = borderRightPos;
        borderLeft.position = borderLeftPos;
    }

    // Update is called once per frame

}
