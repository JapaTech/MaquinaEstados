using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentar : MonoBehaviour
{
    //Componentes
    private Rigidbody2D rb;
    private Transform tr;
    private Animator anim;

    private float gravityInicial;
    private Vector2 inputs;

    //Movimento no chão
    private Vector2 calculoMovimentos;
    private Vector3 olhaDirecao;
    [SerializeField] private float velMovimento;

    //Pulo
    [SerializeField] private Transform[] pontosChecagemChao = new Transform[2];
    private RaycastHit2D hitNoChao_rH;
    [SerializeField] private LayerMask solo;
    private bool pediuPular;
    private bool estaNoChao;
    [SerializeField] private float velPulo;

    //Agachar
    private bool agachado;
    [SerializeField] private Collider2D colAgachado;
    [SerializeField] private Collider2D colEmPe;
    
    //Escada
    [SerializeField] private float velEscada;
    private bool estaNaEscada;
    private bool podeSubir;

    void Awake()
    {
        tr = transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        gravityInicial = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        LeInputMovimento();
        LeInputSubirDescer();
        LeInputPulo();
        VerificaSePodeSubir();
        LeInputAgachado();
    }

    private void FixedUpdate()
    {
        VerificaEstaNoChao();
        CalculaMovimento();
        Escada();
        AplicaMovimento();
        //Dash();
    }

    private void LateUpdate()
    {
        OlhaParaDirecao();
        Anima();
    }

    private void LeInputMovimento()
    {
        inputs.x = Input.GetAxis("Horizontal");
    }

    private void LeInputPulo()
    {
        if (Input.GetButtonDown("Jump"))
            pediuPular = true;
    }

    private void LeInputSubirDescer()
    {
        inputs.y = Input.GetAxis("Vertical");
    }

    private void LeInputAgachado() 
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!estaNoChao || estaNaEscada)
                return;
            
            agachado = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            agachado = false;
        }
        Agachar();
    }

    private void VerificaSePodeSubir()
    {
        if (estaNaEscada == true && Mathf.Abs(inputs.y) > 0)
            podeSubir = true;
    }

    private void CalculaMovimento()
    {
        if (agachado)
        {
            calculoMovimentos = Vector2.zero;
            return;
        }

        calculoMovimentos.x = inputs.x * velMovimento;
        Pular();
    }

    private void Agachar()
    {
        if (agachado)
        {
            colAgachado.enabled = true;
            colEmPe.enabled = false;
        }
        else
        {
            colAgachado.enabled = false;
            colEmPe.enabled = true;
        }
    }

    private void Pular()
    {
        if (pediuPular && estaNoChao)
        {
            calculoMovimentos.y = velPulo;
        }
        else
            calculoMovimentos.y = rb.velocity.y;

        pediuPular = false;
    }

    private void VerificaEstaNoChao()
    {
        hitNoChao_rH = Physics2D.Linecast(pontosChecagemChao[0].position, pontosChecagemChao[1].position, solo);

        if (hitNoChao_rH.collider == null)
            estaNoChao = false;
        else
            estaNoChao = true;
    }


    private void OlhaParaDirecao()
    {
        if (inputs.x != 0)
            olhaDirecao.x = inputs.x;

        tr.right = olhaDirecao;
    }

    private void Escada()
    {
        if (podeSubir)
        {
            rb.gravityScale = 0;
            calculoMovimentos.y = inputs.y * velEscada;
        }
        else
        {
            rb.gravityScale = gravityInicial;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Escada"))
            estaNaEscada = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Escada"))
        {
            estaNaEscada = false;
            podeSubir = false;

        }
    }

    void AplicaMovimento()
    {
        rb.velocity = calculoMovimentos;
    }

    void Anima()
    {
        anim.SetFloat("VelMovimento", Mathf.Abs(calculoMovimentos.x));
        anim.SetBool("NoChao", estaNoChao);
        anim.SetFloat("VelNoAr", calculoMovimentos.y);
        anim.SetBool("Escada", podeSubir);
        anim.SetBool("Agachado", agachado);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pontosChecagemChao[0].position, pontosChecagemChao[1].position);
    }
}

//!!!!!!!!!