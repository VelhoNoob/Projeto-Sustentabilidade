using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 movement;

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
    }

    void FixedUpdate()
    {
        // Move o personagem
        rb.velocity = movement.normalized * moveSpeed;

        //Configurando dire��es
        if (rb.velocity.x > 0)
        {
            //transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
            sr.flipX = false;
        }
        if (rb.velocity.x < 0)
        {
            //transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
            sr.flipX = true;
        }
    }

}
