using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeslocaPonto : MonoBehaviour
{
    [SerializeField] Transform posPlayer;
    [SerializeField] Transform posMax;
    Transform tr;

    [SerializeField] LayerMask parede;
    private RaycastHit2D atingiuParede;
    private RaycastHit2D proximoParede;
    [SerializeField] private float margemParede = 0.25f;


    [SerializeField] LayerMask chao;
    private RaycastHit2D atingiuChao;
    private RaycastHit2D proximoChao;
    [SerializeField] private float altura = 1.64f;
    [SerializeField] private float margemChao = 0.25f;

    Vector3 posinicial;
    bool podeMover;

    [SerializeField] Vector2 dimensoesRetangulo;

    private void Start()
    {
        tr = transform;
        posinicial = tr.localPosition;
    }

    private void FixedUpdate()
    {
        AfastaPontoChao();
        AfastaPontoFrente();
    }

    private void AfastaPontoFrente()
    {
        proximoParede = Physics2D.Raycast(tr.position, Vector3.right, margemParede, parede);

        if (proximoParede)
        {
            return; 
        }

        atingiuParede = Physics2D.Raycast(posPlayer.position, tr.right, Vector2.Distance(tr.position, posPlayer.position), parede);
        if (atingiuParede)
        {
            tr.position =  atingiuParede.point;
        }
        else
        {
            tr.localPosition = posinicial;
        }
    }

    private void AfastaPontoChao()
    {
        Vector3 teste = new Vector3();
        float f = altura + margemChao;
        

        atingiuChao = Physics2D.Linecast(tr.position, tr.position + Vector3.down * altura, chao);
        proximoChao = Physics2D.Raycast(tr.position, Vector3.down, altura + margemChao, chao);
        

        if (atingiuChao)
        {

            teste.y = Vector3.Distance(tr.position + Vector3.down * altura, atingiuChao.point);
            tr.position += teste;
        }
        else if (proximoChao)
        {
            return;
        }
        else
        {
            tr.localPosition = posinicial;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(posPlayer.position, transform.right * Vector2.Distance(transform.position, posPlayer.position));
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * altura);
    }
}
