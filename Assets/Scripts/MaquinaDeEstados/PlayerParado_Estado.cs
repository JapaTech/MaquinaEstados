using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParado_Estado : PlayerBase_Estado
{
    public PlayerParado_Estado(PlayerMaquinaDeEstados _contextoAtual, Player_StateFactory _factory)
       : base(_contextoAtual, _factory) { }

    public override void InicializaEstado()
    {
        //Debug.Log("Inicializou Parar");
        ctx.Rb.velocity = Vector2.zero;
    }

    public override void AtualizaEstado()
    {
        Debug.Log("Parado");
        ChecaTrocaDeEstado();
    }

    public override void ChecaTrocaDeEstado()
    {
       if(ctx.InputsX != 0)
       {
            TrocaEstados(fabrica.Andando());
       }
       if (ctx.PodeDarDash && ctx.PediuDash)
       {
            //Debug.Log("Entrou no dash do parado");
            TrocaEstados(fabrica.Dash());
            ctx.PodeDarDash = false;
            ctx.PediuDash = false;
       }
    }

    public override void FinalizaEstado()
    {
        //Debug.Log("Finalizou Parado");
    }

    public override void InicializaSubestado()
    {
       
    }
}
