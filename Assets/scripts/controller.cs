using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public Vector3 offset;
    public float smoothTime = 0.5f;
    public float distanceScale = 1.0f; // Fator de escala para controlar a elevação com base na distância
    public float minElevation = 5.0f; // Elevação mínima para a câmera
    private Vector3 velocity = Vector3.zero;
    private Vector3 startPosition = new Vector3(0, 0.5f, 0);

    void Start()
    {
        // Define o offset inicial com base na posição inicial da câmera.
        offset = transform.position - (player1.position + player2.position) / 2;
    }

    void LateUpdate()
    {
        // Verifica e reposiciona os jogadores se eles caírem abaixo do eixo Y
        ResetPlayerPositionIfBelowY(player1);
        ResetPlayerPositionIfBelowY(player2);

        // Determina qual jogador está mais atrás no eixo Z.
        Transform targetPlayer = player1.position.z < player2.position.z ? player1 : player2;

        // Calcula a distância entre os jogadores
        float distanceBetweenPlayers = Vector3.Distance(player1.position, player2.position);

        // Calcula a elevação baseada na distância entre os jogadores, aplicando o limite mínimo
        float elevation = Mathf.Max(offset.y + distanceBetweenPlayers * distanceScale, minElevation);

        // Calcula a posição alvo da câmera com base no jogador selecionado, o offset e a distância entre os jogadores
        Vector3 targetPosition = new Vector3(
            (player1.position.x + player2.position.x) / 2, // Mantém a câmera centrada entre os jogadores no eixo X.
            elevation, // Usa a elevação calculada, respeitando o limite mínimo.
            targetPlayer.position.z + offset.z // Posiciona a câmera atrás do jogador mais atrás no eixo Z.
        );

        // Suaviza o movimento da câmera para seguir o jogador mais atrás no eixo Z e ajustar a altura baseado na distância entre os jogadores
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    // Método para resetar a posição do jogador se estiver abaixo de Y = -3
    private void ResetPlayerPositionIfBelowY(Transform player)
    {
        if (player.position.y < -3)
        {
            player.position = startPosition; 
    
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            GameManager.Instance.decrementLife(); 
        }
    }
}
