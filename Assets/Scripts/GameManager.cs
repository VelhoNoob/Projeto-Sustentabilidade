using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private Player player;

    [Header("Gerenciado de Tempo")]
    [SerializeField] private Text tempoTexto;
    [SerializeField] private float tempoContador;   
    [SerializeField] private bool fimDeJogo = false;

    [Header("Pontuação")]
    [SerializeField] private Text pontuacaoTexto;
    [SerializeField] private int pontuacao;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();        
    }


    void Update()
    {        
        ContadorDeTempo();        
        AtualizaçãoDisplay();
        pontuacao = player.pontos;
    }

    public void AtualizaçãoDisplay()
    {
        //tempoTexto.text = tempoContador.ToString("F0"); 
        TranformarTempoEmMinutos();
        pontuacaoTexto.text = pontuacao.ToString();
    }

    void ContadorDeTempo()
    {
        fimDeJogo = false;

        if(!fimDeJogo && tempoContador > 0)
        {
            tempoContador -= Time.deltaTime; // Contagem regressiva           

            if (tempoContador <= 0)
            {
                tempoContador = 0;
                fimDeJogo = true;
            }

        }
    }    

    void TranformarTempoEmMinutos()
    {
        // Calculate the number of minutes and seconds that have elapsed
        int minutos = Mathf.FloorToInt(tempoContador / 60);
        int segundos = Mathf.FloorToInt(tempoContador % 60);

        // Update the stopwatch text to display the elapsed time
        tempoTexto.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }


}
