public abstract class PlayerBase_Estado
{
    protected bool eUmEstadoRaiz = false;
    protected PlayerMaquinaDeEstados ctx;
    protected Player_StateFactory fabrica;
    protected PlayerBase_Estado superEstadoAtual;
    protected PlayerBase_Estado subEstadoAtual;

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
