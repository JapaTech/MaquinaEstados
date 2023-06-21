
public class Player_StateFactory
{
    PlayerMaquinaDeEstados contexto;

    public Player_StateFactory(PlayerMaquinaDeEstados _contextoAtual)
    {
        contexto = _contextoAtual;
    }

    public PlayerBase_Estado NoChao()
    {
        return new PlayerNoChao_Estado(contexto, this);
    }
    public PlayerBase_Estado Andando()
    {                                   
        return new PlayerAndando_Estado(contexto, this);
    }
    public PlayerBase_Estado Dash()
    {
        return new PlayerDarDash_Estado(contexto, this);
    }
    public PlayerBase_Estado Agachado()
    {
        return new PlayerAgacha_Estado(contexto, this);
    }
    public PlayerBase_Estado Escada()
    {
        return new PlayerEscada_Estado(contexto, this);
    }

    public PlayerPulo_Estado Pulo()
    {
        return new PlayerPulo_Estado(contexto, this);
    }

    public PlayerParado_Estado Parado()
    {
        return new PlayerParado_Estado(contexto, this);
    }

    public PlayerCaindo_Estado Caindo()
    {
        return new PlayerCaindo_Estado(contexto, this);
    }
}
