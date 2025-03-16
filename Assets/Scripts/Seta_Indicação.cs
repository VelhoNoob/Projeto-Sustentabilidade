using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seta_Indicação : MonoBehaviour
{
    [Header("Referências")]
    [Tooltip("Objeto que queremos indicar (por exemplo, a lixeira).")]
    public Transform target;

    [Tooltip("Imagem da seta na UI (um RectTransform).")]
    public RectTransform arrowUI;

    [Tooltip("Câmera principal da cena.")]
    public Camera mainCamera;

    [Header("Configurações")]
    [Tooltip("Espaçamento em pixels da borda da tela para posicionar a seta.")]
    public float borderPadding = 50f;

    void Update()
    {
        // Verifica se todas as referências foram atribuídas
        if (target == null || arrowUI == null || mainCamera == null)
            return;

        // Converte a posição do alvo para coordenadas de viewport
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(target.position);

        // Se o alvo estiver atrás da câmera, inverte a direção
        if (viewportPos.z < 0)
        {
            viewportPos.x = 1 - viewportPos.x;
            viewportPos.y = 1 - viewportPos.y;
        }

        // Checa se o alvo está fora dos limites da tela (viewport fora do intervalo [0,1])
        bool isOffScreen = viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1;
        arrowUI.gameObject.SetActive(isOffScreen);

        if (isOffScreen)
        {
            // Converte a posição do alvo para coordenadas de tela
            Vector3 targetScreenPos = mainCamera.WorldToScreenPoint(target.position);

            // Pega o centro da tela
            Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) * 0.5f;

            // Calcula a direção do centro da tela até o alvo
            Vector3 dir = (targetScreenPos - screenCenter).normalized;

            // Calcula a posição da seta na borda da tela, levando em conta o borderPadding
            float xPos = screenCenter.x + dir.x * (screenCenter.x - borderPadding);
            float yPos = screenCenter.y + dir.y * (screenCenter.y - borderPadding);
            arrowUI.position = new Vector3(xPos, yPos, 0);

            // Rotaciona a seta para apontar na direção do alvo
            // Se o sprite da seta estiver apontando para cima por padrão, subtraia 90 graus.
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrowUI.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }

}



