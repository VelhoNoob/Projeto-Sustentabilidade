using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Adicione este namespace

public class GameManager : MonoBehaviour
{
    // Refer�ncia ao componente TextMeshProUGUI
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI timerText;
    public GameObject TelaFinal;
    // Vari�vel para armazenar a pontua��o
    private int score = 0;

    // Vari�vel para armazenar o tempo inicial do cron�metro
    public float startTime = 60f;

    // Vari�vel para armazenar o tempo restante
    private float timer;

    // Refer�ncia ao GameObject que cont�m o script Player
    public GameObject playerGameObject;

    private Player p;

    // Refer�ncia ao script ChangeScene
    public ChangeScene changeScene;

    // Vari�vel para verificar se o jogo est� pausado
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializa o texto da pontua��o
        TelaFinal.SetActive(false);
        UpdateScoreText();

        // Inicializa o cron�metro com o tempo inicial
        timer = startTime;
        UpdateTimerText();

        // Obt�m a vari�vel pontos do script Player
        p = playerGameObject.GetComponent<Player>();
        if (p != null)
        {
            score = p.pontos;
            UpdateScoreText();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Atualiza a pontua��o
        score = p.pontos;
        UpdateScoreText();

        // Atualiza o cron�metro
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 0;
                // A��o a ser executada quando o cron�metro atinge zero
                PauseGame();
                Debug.Log("Tempo esgotado!");
            }
            UpdateTimerText();
        }

        // Verifica se o jogo est� pausado e se a tecla "Esc" foi pressionada
        if (isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            changeScene.ChangeToScene("Menu"); // Substitua "NomeDaCena" pelo nome da cena desejada
        }
    }

    // M�todo para atualizar o texto da pontua��o
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
        scoreText2.text = score + " PONTOS";
    }

    // M�todo para atualizar o texto do cron�metro
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // M�todo para pausar o jogo
    void PauseGame()
    {
        TelaFinal.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        // Desativar a entrada do jogador, se necess�rio
        if (p != null)
        {
            p.enabled = false;
        }
    }
}