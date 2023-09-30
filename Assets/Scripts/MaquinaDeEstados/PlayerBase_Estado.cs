//Classe base de todos os estados

public abstract class PlayerBase_Estado
{
    //Se for um estado, true; se não, false
    protected bool eUmEstadoRaiz = false;

    //Referência da máquina de estados
    protected PlayerMaquinaDeEstados ctx;

    //Referência da fábrica (construtor) de estados
    protected Player_StateFactory fabrica;

    //Referência para estado e subestado
    protected PlayerBase_Estado superEstadoAtual;
    protected PlayerBase_Estado subEstadoAtual;


    //Construtor
    public PlayerBase_Estado(PlayerMaquinaDeEstados _contextoAtual, Player_StateFactory _factory)
    {
        ctx = _contextoAtual;
        fabrica = _factory;
    }

    public abstract void InicializaEstado();
    public abstract void AtualizaEstado();
    public abstract void FinalizaEstado();
    public abstract void ChecaTrocaDeEstado();
    public abstract void InicializaSubestado();


    public void UpdateEstados() 
    {
        AtualizaEstado();
        if(subEstadoAtual != null)
        {
            subEstadoAtual.UpdateEstados();
        }
    }

    protected void TrocaEstados(PlayerBase_Estado novoEstado) 
    {
        FinalizaEstado();

        novoEstado.InicializaEstado();

        if (eUmEstadoRaiz)
            ctx.EstadoAtual = novoEstado;
        else if (superEstadoAtual != null)
            superEstadoAtual.DefinaSubestado(novoEstado);   

    }
    
    protected void DefinaSuperestado(PlayerBase_Estado novoSuperestado) {
        superEstadoAtual = novoSuperestado;
    }

    protected void DefinaSubestado(PlayerBase_Estado novoSubestado) {
        subEstadoAtual = novoSubestado;
        novoSubestado.DefinaSuperestado(this);
    }

}
