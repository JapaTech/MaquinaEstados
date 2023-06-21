using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndando_Estado : PlayerBase_Estado
{
    public PlayerAndando_Estado(PlayerMaquinaDeEstados _contextoAtual, Player_StateFactory _factory)
       : base(_contextoAtual, _factory) { }

    public override void AtualizaEstado()
    {
        Debug.Log("Andando");
        ChecaTrocaDeEstado();
        ctx.CalculoMovimentosX = ctx.InputsX * ctx.VelMovimento;
    }

    public override void ChecaTrocaDeEstado()
    {
        if (ctx.InputsX == 0)
            TrocaEstados(fabrica.Parado());
        if (ctx.PediuDash)
        {
            TrocaEstados(fabrica.Dash());
        }
    }

    public override void FinalizaEstado()
    {
        //Debug.Log("Finalizou Andar");
        ctx.Rb.velocity = Vector2.zero;
    }

    public override void InicializaSubestado()
    {
        //Debug.Log("Inicializou Andar");
    }

    public override void InicializaEstado()
    {
        Debug.Log("Entrou no andar");
    }
}
