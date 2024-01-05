using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    //Componentes
    Rigidbody2D rb;
    Transform tr;
    Animator anim;

    float gravityInicial;
    Vector2 inputs;
    
    //Movimento no chão
    Vector2 calculoMovimentos;
    Vector3 olhaDirecao;
    [SerializeField] float velMovimento;

    //Pulo
    [SerializeField] Transform[] pontosChecagemChao = new Transform[2];
    RaycastHit2D hitNoChao_rH;
    [SerializeField] LayerMask solo;
    bool pediuPular;
    bool estaNoChao;
    [SerializeField] float velPulo;

    bool agachado = false;

    //Dash
    bool pediuParaDarDash = false;
    bool podeDarDash = true;
    bool estaDandoDash = false;
    [SerializeField] float forcaDoDash = 24f;
    [SerializeField] float tempoDoDash = 0.2f;
    [SerializeField] float coolDownDoDash = 1.5f;

    //Escada
    [SerializeField] float velEscada;
    bool estaNaEscada;
    bool podeSubir;

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
        LeInputDash();

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

    void LeInputMovimento()
    {
        inputs.x = Input.GetAxis("Horizontal");
    }

    void LeInputPulo()
    {
        if (Input.GetButtonDown("Jump"))
            pediuPular = true;
    }

    void LeInputDash()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            pediuParaDarDash = true;
        }
    }

    void LeInputAgachado()
    {
        if (Input.GetButtonDown("Fire1"))
            agachado = true;
        if (Input.GetButtonUp("Fire1"))
            agachado = false;
    }

    void LeInputSubirDescer()
    {
        inputs.y = Input.GetAxis("Vertical");
    }

    void VerificaSePodeSubir()
    {
        if (estaNaEscada == true && Mathf.Abs(inputs.y) > 0)
            podeSubir = true;
    }

    void CalculaMovimento()
    {
        calculoMovimentos.x = inputs.x * velMovimento;
;       Pular();
    }

    void Pular()
    {
        if (pediuPular && estaNoChao)
        {
            calculoMovimentos.y = velPulo;
        }
        else
            calculoMovimentos.y = rb.velocity.y;

        pediuPular = false;
    }

    void VerificaEstaNoChao()
    {
        hitNoChao_rH = Physics2D.Linecast(pontosChecagemChao[0].position, pontosChecagemChao[1].position, solo);

        if (hitNoChao_rH.collider == null)
            estaNoChao = false;
        else
            estaNoChao = true;
    }


    void OlhaParaDirecao() 
    {
        if (inputs.x != 0)
            olhaDirecao.x = inputs.x;

        tr.right = olhaDirecao;
    }

    void Escada()
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

    void Dash()
    {
        if (estaDandoDash)
            return;

        if (podeDarDash && pediuParaDarDash)
            StartCoroutine(Dash_Crtn());
        pediuParaDarDash = false;
    }

    private IEnumerator Dash_Crtn()
    {
        rb.velocity = Vector2.zero;
        float time = 0;
        podeDarDash = false;
        estaDandoDash = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        //rb.MovePosition(rb.position + (Vector2)tr.right * forcaDoDash * Time.fixedDeltaTime);
        //rb.AddForce(tr.right * forcaDoDash, ForceMode2D.Impulse);
        //rb.AddForceAtPosition(tr.right * forcaDoDash, tr.right, ForceMode2D.Impulse);
        while(tempoDoDash > time)
        {
            time += Time.deltaTime;
            rb.velocity = tr.right * forcaDoDash;
            yield return null;
        }
        rb.gravityScale = originalGravity;
        estaDandoDash = false;
        yield return new WaitForSeconds(coolDownDoDash);
        podeDarDash = true;
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
        anim.SetBool("Agachado", agachado);
        anim.SetBool("Escada", podeSubir);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pontosChecagemChao[0].position, pontosChecagemChao[1].position);
    }
}
