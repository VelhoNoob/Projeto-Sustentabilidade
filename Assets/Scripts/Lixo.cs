using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lixo : MonoBehaviour
{

    [SerializeField] private string tipoLixo = null;

    [SerializeField] private List<Sprite> sprites;

    public SpriteRenderer sr;
    void Start()
    {   
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[Random.Range(0, sprites.Count)];
        sr.sortingOrder = 5; // garante que o sprite fique na frente do player
    }
    
    //private void OnCollisionEnter2D(Collision2D colisao){
    //    if (colisao.gameObject.CompareTag("Player"))
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    
    public string GetTipoLixo()
    {
        return tipoLixo;
    }
}
