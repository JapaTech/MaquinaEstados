using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Coleta informações que determinam qual estado deve ser acionado
public class PlayerMaquinaDeEstados : MonoBehaviour
{
    //Componentes
    Rigidbody2D rb;
    Transform tr;
    Animator anim;

    //Gravidade
    float gravityInicial;
    
    //Inputs
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
    [SerializeField] float velPulo = 11f;
    bool estaPulando;
    [SerializeField] float duracaoPulo;

    //Agachar
    bool agachado = false;

    //Dash
    bool pediuParaDarDash = false;
    bool podeDarDash = true;
    bool estaDandoDash = false;
    [SerializeField] float tempoDoDash = 0.2f;
    [SerializeField] float cooldownDoDash = 1.5f;
    [SerializeField] Transform dashPos;

    //Escada
    [SerializeField] float velEscada;
    bool estaNaEscada;
    bool podeSubir;

    //Estados
    PlayerBase_Estado estadoAtual;
    Player_StateFactory fabricaDeEstados;

    //Getters & Setters
    public PlayerBase_Estado EstadoAtual { get { return estadoAtual; } set { estadoAtual = value; } }
    public Rigidbody2D Rb { get { return rb; } set { rb = value; } }
    public Transform Tr { get { return tr; } set { tr = value; } }
    public bool PediuPular { get { return pediuPular; } set { pediuPular = value; } }
    public bool EstaPulando { get { return estaPulando; } }
    public float CalculoMovimentosX { get { return calculoMovimentos.x; } set { calculoMovimentos.x = value; } }
    public float CalculoMovimentosY { get { return calculoMovimentos.y; } set { calculoMovimentos.y = value; } }
    public float VelMovimento { get { return velMovimento; } }
    public float VelPulo { get { return velPulo; } set { velPulo = value; } }
    public bool NoChao { get { return estaNoChao; } }
    public bool Agachado { get { return agachado; } }
    public float VelEscada { get { return velEscada; } }
    public bool PodeSubir { get { return podeSubir; } }
    public float InputsX { get { return inputs.x; } }
    public float InputsY { get { return inputs.y; } set { inputs.y = value; } }
    public float GravityScaleIni { get { return gravityInicial; } set { gravityInicial = value; } }
    public bool PodeDarDash { get { return podeDarDash; } set { podeDarDash = value; } }
    public bool EstaDandoDash { get { return estaDandoDash; } set { estaDandoDash = value; } }
    public bool PediuDash { get { return pediuParaDarDash; } set { pediuParaDarDash = value; } }
    public bool DashPos { get { return dashPos; } }

    void Awake()
    {
        //Acessar componentes
        tr = transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        //Set valor da gravidade inicial
        gravityInicial = rb.gravityScale;

        //Cria fábrica de estado e define o estado inicial
        fabricaDeEstados = new Player_StateFactory(this);
        estadoAtual = fabricaDeEstados.NoChao();
        estadoAtual.InicializaEstado();
    }

    // Update is called once per frame
    void Update()
    {
        //Leitura dos inputs
        LeInputMovimento();
        LeInputSubirDescer();
        LeInputPulo();
        LeInputAgachado();
        LeInputDash();
        //Verifica se está em uma escada
        VerificaSePodeSubir();
    }

    private void FixedUpdate()
    {
        //Função que atualiza os estados
        estadoAtual.UpdateEstados();

        VerificaEstaNoChao();
        AplicaMovimento();
    }

    private void LateUpdate()
    {
        //Vira o personagem para esquerda ou direita
        OlhaParaDirecao();
        //Animaçies
        Anima();
    }

    #region Detecção de Inputs
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
        if (!podeDarDash)
            return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Pediu dash");
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

    #endregion

    //Corrotina para controlar o tempo que leva para o jogador chegar no ápice do pulo
    IEnumerator EstaNoPulo()
    {
        estaPulando = true;
        yield return new WaitForSeconds(duracaoPulo);
        estaPulando = false;
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

    void VerificaSePodeSubir()
    {
        if (estaNaEscada == true && Mathf.Abs(inputs.y) > 0)
            podeSubir = true;
    }

    void OlhaParaDirecao()
    {
        if (inputs.x != 0)
            olhaDirecao.x = inputs.x;

        tr.right = olhaDirecao;
    }

    void AplicaMovimento()
    {
        rb.velocity = calculoMovimentos;
    }

    #region Dash
    public void StardDash_Crtn()
    {
        Debug.Log(podeDarDash);
        if (podeDarDash && !estaDandoDash)
        {
            StartCoroutine(Dash_Crtn(dashPos.position, tempoDoDash));
        }
        else
        {
            pediuParaDarDash = false;
        }
    }

    //Corrotina que controla o Dash
    private IEnumerator Dash_Crtn(Vector2 dashPos, float duracao)
    {
        if (!estaDandoDash)
        {
            estaDandoDash = true;
            podeDarDash = false;
            float tempo = 0;
            Vector2 posInicial = tr.position;
            Vector2 posFinal = dashPos;
            Vector2 posAtual;
            
            while(tempo < duracao)
            {
                posAtual = Vector2.Lerp(posInicial, posFinal, tempo / duracao);
                rb.MovePosition(posAtual);
                tempo += Time.deltaTime;
                yield return null;
            }
            
            rb.MovePosition(posFinal);
        }
        estaDandoDash = false;
        StartCoroutine(CooldownDash(estaNoChao));
        //Debug.Log("Saiu da courotine dash");
    }

    //Cooldown para o Dash
    private IEnumerator CooldownDash(bool comecouDashDoChao)
    {
        estaDandoDash = false;
        WaitForSeconds cooldown = new WaitForSeconds(cooldownDoDash);

        podeDarDash = false;
        //Debug.Log("Entrou no cooldown do dash");
        if (comecouDashDoChao)
        {
            yield return cooldown;
            pediuParaDarDash = false;
            podeDarDash = true;
        }
        else
        {
            yield return cooldown;
            yield return new WaitUntil(() => estaNoChao == true);
            pediuParaDarDash = false;
            podeDarDash = true;
        }
        //Debug.Log("Saiu da cooldown dash");
    }
    #endregion

    //Seta as variáveis do animator
    void Anima()
    {
        anim.SetFloat("VelMovimento", Mathf.Abs(calculoMovimentos.x));
        anim.SetBool("NoChao", estaNoChao);
        anim.SetFloat("VelNoAr", calculoMovimentos.y);
        anim.SetBool("Agachado", agachado);
        anim.SetBool("Escada", podeSubir);
    }


    //Colisões que verificam se o jogador está numa escada
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
}
