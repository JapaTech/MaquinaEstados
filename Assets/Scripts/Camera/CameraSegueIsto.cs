using System.Collections;
using UnityEngine;

public class CameraSegueIsto : MonoBehaviour
{
    [Header("Referência de Objeto")]
    [SerializeField] private Transform playerTransform;

    [Header("Flip Rotacao")]
    [SerializeField] private float tempoFlipRotacaoY = 0.5f;

    private Transform tr;

    private Coroutine corritinaDeVirar;

    private PlayerMaquinaDeEstados player;

    private bool estaViradoParaDireita;

    private void Start()
    {
        tr = transform;

        player = playerTransform.gameObject.GetComponent<PlayerMaquinaDeEstados>();

        estaViradoParaDireita = player.EstaViradoDireita;
    }

    private void Update()
    {
        tr.position = playerTransform.position;
    }

    public void ComecaVirar()
    {
        corritinaDeVirar = StartCoroutine(FlipLerp());
    }

    private IEnumerator FlipLerp()
    {
        Debug.Log("Chamou");
        float rotInicial = tr.localEulerAngles.y;
        float rotFinalQuantidade = DeterminarRotacaoFinal();
        float rotY = 0f;

        float tempo = 0;

        while(tempo < tempoFlipRotacaoY)
        {
            tempo += Time.deltaTime;

            rotY = Mathf.Lerp(rotInicial, rotFinalQuantidade, (tempo / tempoFlipRotacaoY));

            tr.rotation = Quaternion.Euler(0, rotY, 0f);

            yield return null;
        }
    } 

    private float DeterminarRotacaoFinal()
    {
        estaViradoParaDireita = !estaViradoParaDireita;

        if (estaViradoParaDireita)
        {
            return 0;
        }
        else
        {
            return 180;
        }
    }

}
