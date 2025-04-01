using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //VARIAVEIS DE MOVIMENTO
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    private Vector2 movement;

    //COLETA DE LIXO
    [SerializeField] private GameObject lixoCarregado = null;
    public int pontos = 0;

    // Prefab a ser instanciado
    public List<GameObject> prefabs;


    // Intervalo de posições aleatórias
    public Vector2 randomPositionMin;
    public Vector2 randomPositionMax;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
        animator.SetFloat("speed", rb.velocity.magnitude);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Pegando a tag do objeto colidido
        string collisionTag = collision.collider.tag;

        // Pegando o lixo
        if (lixoCarregado == null &&
            (collisionTag == "Lixo_Verde" ||
             collisionTag == "Lixo_Azul" ||
             collisionTag == "Lixo_Vermelho" ||
             collisionTag == "Lixo_Amarelo"))
        {
            lixoCarregado = collision.gameObject; // O jogador pega o lixo
            lixoCarregado.GetComponent<Collider2D>().enabled = false; // Desabilita o collider do lixo para evitar múltiplas coletas
            lixoCarregado.transform.SetParent(transform); // Faz o lixo "grudar" no jogador
        }

        // Descartando o lixo na lixeira
        if (lixoCarregado != null &&
            (collisionTag == "Lixeira_Verde" ||
             collisionTag == "Lixeira_Azul" ||
             collisionTag == "Lixeira_Vermelho" ||
             collisionTag == "Lixeira_Amarelo"))
        {
            //
            string corLixeira = collisionTag.Replace("Lixeira_", "");
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

            Vector2 randomPosition = new Vector2(
                Random.Range(randomPositionMin.x, randomPositionMax.x),
                Random.Range(randomPositionMin.y, randomPositionMax.y)
            );

            // Instancia o prefab na posição aleatória
            GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Count)];

            // Instancia o prefab na posição aleatória
            Instantiate(randomPrefab, randomPosition, Quaternion.identity);


            Destroy(lixoCarregado); // Remove o lixo descartado
            lixoCarregado = null; // Libera o jogador para pegar outro lixo
        }
    }

}
