using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTeste : MonoBehaviour
{
    Rigidbody2D rb;
    Transform tr;
    bool pediuDash;
    bool executandoDash;
    [SerializeField] Transform dashPos;
    [SerializeField] float duracao;

    private void Start()
    {
        tr = transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        LeInput();
    }

    private void FixedUpdate()
    {
        if (!executandoDash)
        {
            ChamaDash();

        }
    }

    private void LeInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            pediuDash = true;
        }
    }

    private void ChamaDash()
    {
        if (pediuDash)
        {
            StartCoroutine(Dash(dashPos.position, duracao));
            pediuDash = false;
        }
    }

    IEnumerator Dash(Vector2 dashPos, float duracao)
    {
        if (!executandoDash)
        {
            executandoDash = true;
            float tempo = 0;
            Vector2 posInicial = tr.position;
            Vector2 posFinal = dashPos;
            Vector2 dashAtual;

            while (tempo < duracao)
            {
                dashAtual = Vector2.Lerp(posInicial, posFinal, tempo / duracao);
                rb.MovePosition(dashAtual);
                tempo += Time.deltaTime;
                Debug.Log(dashAtual);
                yield return null;
            }

            Debug.Log("Saiu do while do dash");
            rb.MovePosition(posFinal);
        }
        executandoDash = false;
    }
}
