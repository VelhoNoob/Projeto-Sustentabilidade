using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    private Vector2 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var xInput = Input.GetAxisRaw("Horizontal");

        //Configurando direções
        if (rb.velocity.x > 0)
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        if (rb.velocity.x < 0)
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
    }

}
