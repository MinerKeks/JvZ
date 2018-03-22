using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    private float Health;
    private float MaxHealth;
    private int Points;
    private Text scoreText, HPText, Score;
    private RectTransform GUIHPBar;
    Scene scene;

    private void OnLevelWasLoaded(int level)
    {
        scene = SceneManager.GetActiveScene();

        if (scene.name == "GameOver")
        {
            SceneManager.MoveGameObjectToScene(gameObject, scene);
            GameObject.Find("Score").GetComponent<Text>().text = ("Your score is: " + Points.ToString());
        }
        if(scene.name == "Menu")
            Points = 0; Health = 0;
    }
    void Awake()
    {
        scene = SceneManager.GetActiveScene();
        if(scene.name == "Game")
        {
            GUIHPBar = (GameObject.Find("HPBar").transform as RectTransform);
            HPText = GameObject.Find("HPBar-ChangableHealth").GetComponent<Text>();
            scoreText = GameObject.Find("Score").GetComponent<UnityEngine.UI.Text>();
        }
    }
    public void setStartValue(float value)
    {
        Health = value;
        HPText.text = Health.ToString();
    }
    public void ChangeHealthBar(float changed_health)
    {
        GUIHPBar.localScale = new Vector3(changed_health/ Health, GUIHPBar.localScale.y);
        HPText.text = changed_health.ToString();
    }
    public void ChangeScore( int newScoreValue)
    {
        Points += newScoreValue;
        scoreText.text = Points.ToString();
    }

}
