using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UiManager : MonoBehaviour
{

    public static UiManager um;

    public int[] holePar;
    public GameObject victoryScreen;

    Text scoreIndicator;
    Text[] holeStats;
    Text victoryScreenText;
    Ball ball;
    GameObject gameSummary;
    Text[] gameSummaryScores;
    int[] holeHits;

    private void Awake()
    {
        if (um == null)
            um = this;
    }

    string GetScore(int par, int hits)
    {
        if (hits == 1)
            return "Ace";
        else if (hits == par - 3)
            return "Albatross";
        else if (hits == par - 2)
            return "Eagle";
        else if (hits == par - 1)
            return "Birdie";
        else if (hits == par)
            return "Par";
        else if (hits == par + 1)
            return "Bogey";
        else if (hits == par + 2)
            return "Double Bogey";
        else if (hits == par + 3)
            return "Triple Bogey";
        else
            return "+" + (hits - par);
    }

    public void ShowVictoryScreen()
    {
        string score;
        int par = holePar[ball.targetHoleIndex - 1];
        score = GetScore(par, ball.hits);
        gameSummaryScores[ball.targetHoleIndex - 1].text = "Hole " + ball.targetHoleIndex + ": " + ball.hits + " | Score: " + score;
        holeHits[ball.targetHoleIndex - 1] = ball.hits;
        victoryScreen.SetActive(true);
        if (ball.hits >= par + 3)
            victoryScreenText.text = "Absolute Horsecrap Job!\n" + score;
        else if (ball.hits >= par)
            victoryScreenText.text = "Good Job!\n" + score;
        else
            victoryScreenText.text = "Great Job!\n" + score;

    }

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindWithTag("Ball").GetComponent<Ball>();
        scoreIndicator = GameObject.FindWithTag("ScoreIndicator").GetComponent<Text>();
        gameSummary = GameObject.FindWithTag("GameSummary");
        gameSummaryScores = gameSummary.GetComponentsInChildren<Text>();
        holeStats = gameSummary.GetComponentsInChildren<Text>();
        holeHits = new int[holePar.Length];
        victoryScreenText = victoryScreen.GetComponentInChildren<Text>();
        gameSummary.SetActive(false);
        StartCoroutine(UpdateCourseStats());
    }

    public void FinalScreen()
    {
        int finalPar = 0;
        int finalHits = 0;
        string finalScore;
        string score;
        int par = holePar[2];
        score = GetScore(par, ball.hits);
        gameSummaryScores[2].text = "Hole " + ball.targetHoleIndex + ": " + ball.hits + " | Score: " + score;
        holeHits[2] = ball.hits;
        for (int i = 0; i < holeHits.Length; i++)
        {
            finalPar += holePar[i];
            finalHits += holeHits[i];
        }
        finalScore = GetScore(finalPar, finalHits);
        gameSummary.SetActive(true);
        gameSummaryScores[gameSummaryScores.Length - 1].text = "Final Score: " + finalScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            gameSummary.SetActive(true);
        else if (Input.GetKeyUp(KeyCode.Tab))
            gameSummary.SetActive(false);
        if (Input.GetKeyDown(KeyCode.Return) && victoryScreen.activeSelf)
        {
            ball.hits = 0;
            victoryScreen.SetActive(false);
            Time.timeScale = 1;
        }

    }

    IEnumerator UpdateCourseStats()
    {
        while (true)
        {
            scoreIndicator.text = "Hole " + ball.targetHoleIndex + " | Par " + holePar[ball.targetHoleIndex - 1] + "\n\nHits so far: " + ball.hits;
            gameSummaryScores[ball.targetHoleIndex - 1].text = "Hole " + ball.targetHoleIndex + ": " + ball.hits + " | Score: ";
            yield return new WaitForSeconds(0.2f);
        }
    }
}
