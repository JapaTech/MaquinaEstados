using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDarDash_Estado : PlayerBase_Estado
{
    public PlayerDarDash_Estado(PlayerMaquinaDeEstados _contextoAtual, Player_StateFactory _factory)
      : base(_contextoAtual, _factory) { }

    public override void InicializaEstado()
    {
        if (!ctx.PodeDarDash)
        {
            TrocaEstados(fabrica.Parado());
        }
        else
        {
            ctx.PediuDash = false;
            ctx.Rb.velocity = Vector2.zero;
            ctx.StardDash_Crtn();
        }
    }

    public override void AtualizaEstado()
    {
        Debug.Log("Dando dash");
        ctx.CalculoMovimentosY = 0;
        ChecaTrocaDeEstado();
    }

    public override void ChecaTrocaDeEstado()
    {
        if(ctx.EstaDandoDash == false)
        {
            TrocaEstados(fabrica.Parado());
        }
    }

    public override void FinalizaEstado()
    {
        ctx.Rb.velocity = Vector2.zero;
        Debug.Log("Saiu Dash");
    }

    public override void InicializaSubestado()
    {
        //Debug.Log("Sub Dash");
    }

}
