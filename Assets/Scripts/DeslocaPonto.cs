using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeslocaPonto : MonoBehaviour
{
    [SerializeField] Transform posInicial;
    Transform tr;
    [SerializeField] Vector3 margem;
    [SerializeField] Vector2 dimensoesRetangulo;
    [SerializeField] LayerMask paredeDash;

    private void Start()
    {
        tr = transform;
    }

    private void FixedUpdate()
    {
        MovePonto();
    }

    private void MovePonto()
    {
        RaycastHit2D ray;
        ray = Physics2D.BoxCast(posInicial.position + margem, dimensoesRetangulo, 0, tr.right, Vector2.Distance(tr.position + margem, posInicial.position + margem), paredeDash);
        Debug.Log(ray.transform.name);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((posInicial.position + margem) + transform.right * Vector2.Distance(posInicial.position + margem, transform.position + margem), dimensoesRetangulo);
        Gizmos.DrawRay(posInicial.position, transform.right * Vector2.Distance(posInicial.position, transform.position));
    }
}
