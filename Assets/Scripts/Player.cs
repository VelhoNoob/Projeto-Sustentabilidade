using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //VARIAVEIS DE MOVIMENTO
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 movement;

    //COLETA DE LIXO
    [SerializeField] private GameObject lixoCarregado = null;
    [SerializeField] private int pontos = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Captura a entrada do jogador
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //COLETA DE LIXO
        // Se o jogador estiver segurando um lixo, ele segue a posição do jogador
        if (lixoCarregado != null)
        {
            lixoCarregado.transform.position = transform.position;
        }
    }

    void FixedUpdate()
    {
        // Move o personagem
        rb.velocity = movement.normalized * moveSpeed;

        //Configurando direções
        if (rb.velocity.x > 0)
        {            
            sr.flipX = false;
        }
        if (rb.velocity.x < 0)
        {            
            sr.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Pegando o lixo
        if (lixoCarregado == null &&
            (collision.CompareTag("Lixo_Verde") ||
             collision.CompareTag("Lixo_Azul") ||
             collision.CompareTag("Lixo_Vermelho") ||
             collision.CompareTag("Lixo_Amarelo")))
        {
            lixoCarregado = collision.gameObject; // O jogador pega o lixo
            lixoCarregado.GetComponent<Collider2D>().enabled = false; // Desabilita o collider do lixo para evitar múltiplas coletas
            lixoCarregado.transform.SetParent(transform); // Faz o lixo "grudar" no jogadorx
        }

        // Descartando o lixo na lixeira
        if (lixoCarregado != null &&
            (collision.CompareTag("Lixeira_Verde") ||
             collision.CompareTag("Lixeira_Azul") ||
             collision.CompareTag("Lixeira_Vermelho") ||
             collision.CompareTag("Lixeira_Amarelo")))
        {
            string corLixeira = collision.tag.Replace("Lixeira_", "");
            string corLixo = lixoCarregado.tag.Replace("Lixo_", "");

            if (corLixo == corLixeira)
            {
                pontos += 10; // Ganha pontos
                Debug.Log("Lixo descartado corretamente! Pontos: " + pontos);
            }
            else
            {
                pontos -= 5; // Perde pontos
                Debug.Log("Lixo descartado errado! Pontos: " + pontos);
            }

            Destroy(lixoCarregado); // Remove o lixo descartado
            lixoCarregado = null; // Libera o jogador para pegar outro lixo
        }
    }

}
