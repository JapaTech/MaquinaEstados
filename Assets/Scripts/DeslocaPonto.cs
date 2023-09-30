using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeslocaPonto : MonoBehaviour
{
    [SerializeField] Transform posPlayer;
    [SerializeField] Transform posMax;
    Transform tr;
    [SerializeField] Vector3 margem;
    [SerializeField] Vector2 dimensoesRetangulo;
    [SerializeField] LayerMask paredeDash;

    Vector3 posinicial;
    bool podeMover;

    private void Start()
    {
        tr = transform;
        posinicial = tr.localPosition;
    }

    private void FixedUpdate()
    {
        AfastaPonto();
 
    }

    private void AfastaPonto()
    {
        RaycastHit2D proximoParede = Physics2D.Raycast(tr.position, Vector3.right, 0.25f, paredeDash);
        if (proximoParede)
        {
            return; 
        }

        RaycastHit2D atingiuParede;
        atingiuParede = Physics2D.Raycast(posPlayer.position, tr.right, Vector2.Distance(tr.position, posPlayer.position), paredeDash);
        if (atingiuParede)
        {
            tr.position =  atingiuParede.point;
        }
        else
        {
            tr.localPosition = posinicial;
        }
    }

    /*private void MovePonto()
    {
        RaycastHit2D ray;
        ray = Physics2D.BoxCast(posPonto.position + margem, dimensoesRetangulo, 0, tr.right, Vector2.Distance(tr.position + margem, posPonto.position + margem), paredeDash);
        if(ray)
        {
            Debug.Log("parede");
            tr.localPosition = ray.point;
        }
        else
        {
            tr.localPosition = posInicial;
        }
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(posPlayer.position, transform.right * Vector2.Distance(transform.position, posPlayer.position));
        //Gizmos.DrawWireCube((posPonto.position + margem) + transform.right * Vector2.Distance(posPonto.position + margem, transform.position + margem), dimensoesRetangulo);
        //Gizmos.DrawRay(posPonto.position, transform.right * Vector2.Distance(posPonto.position, transform.position));
    }
}
