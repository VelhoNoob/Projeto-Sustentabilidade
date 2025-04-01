using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Adicione este namespace

public class GameManager : MonoBehaviour
{
    // Referência ao componente TextMeshProUGUI
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI timerText;
    public GameObject TelaFinal;
    // Variável para armazenar a pontuação
    private int score = 0;

    // Variável para armazenar o tempo inicial do cronômetro
    public float startTime = 60f;

    // Variável para armazenar o tempo restante
    private float timer;

    // Referência ao GameObject que contém o script Player
    public GameObject playerGameObject;

    private Player p;

    // Referência ao script ChangeScene
    public ChangeScene changeScene;

    // Variável para verificar se o jogo está pausado
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializa o texto da pontuação
        TelaFinal.SetActive(false);
        UpdateScoreText();

        // Inicializa o cronômetro com o tempo inicial
        timer = startTime;
        UpdateTimerText();

        // Obtém a variável pontos do script Player
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
        // Atualiza a pontuação
        score = p.pontos;
        UpdateScoreText();

        // Atualiza o cronômetro
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 0;
                // Ação a ser executada quando o cronômetro atinge zero
                PauseGame();
                Debug.Log("Tempo esgotado!");
            }
            UpdateTimerText();
        }

        // Verifica se o jogo está pausado e se a tecla "Esc" foi pressionada
        if (isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            changeScene.ChangeToScene("Menu"); // Substitua "NomeDaCena" pelo nome da cena desejada
        }
    }

    // Método para atualizar o texto da pontuação
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
        scoreText2.text = score + " PONTOS";
    }

    // Método para atualizar o texto do cronômetro
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Método para pausar o jogo
    void PauseGame()
    {
        TelaFinal.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        // Desativar a entrada do jogador, se necessário
        if (p != null)
        {
            p.enabled = false;
        }
    }
}