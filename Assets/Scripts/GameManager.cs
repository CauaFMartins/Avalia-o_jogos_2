using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI timerText;
    public GameObject gameOverText;

    [Header("Game")]
    public int score = 0;
    public int ammo = 30;

    public float tempo = 60f;

    bool acabou = false;

    void Update()
    {
        if (acabou)
            return;

        tempo -= Time.deltaTime;

        if (tempo <= 0)
        {
            tempo = 0;
            FimDeJogo();
        }

        AtualizarUI();
    }

    void AtualizarUI()
    {
        scoreText.text = "Score: " + score;

        ammoText.text = "Ammo: " + ammo + "/30";

        timerText.text = "Time: " + Mathf.Ceil(tempo);
    }

    void FimDeJogo()
    {
        acabou = true;

        gameOverText.SetActive(true);

        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0f;
    }

    public void AdicionarScore(int valor)
    {
        score += valor;
    }

    public bool PodeAtirar()
    {
        return ammo > 0;
    }

    public void GastarMunicao()
    {
        ammo--;
    }
}