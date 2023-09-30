using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParado_Estado : PlayerBase_Estado
{
    public PlayerParado_Estado(PlayerMaquinaDeEstados _contextoAtual, Player_StateFactory _factory)
       : base(_contextoAtual, _factory) { }

    public override void InicializaEstado()
    {
        MostraEstado.Instancia.MostraTextoSubestado("Parado");
        ctx.CalculoMovimentosX = 0;
    }

    public override void AtualizaEstado()
    {
        ChecaTrocaDeEstado();
    }

    public override void ChecaTrocaDeEstado()
    {
       if(ctx.InputsX != 0)
       {
            TrocaEstados(fabrica.Andando());
       }
       else if (ctx.PodeDarDash && ctx.PediuDash)
       {
            TrocaEstados(fabrica.Dash());
            ctx.PediuDash = false;
       }
    }

    public override void FinalizaEstado()
    {
  
    }

    public override void InicializaSubestado()
    {
       
    }
}
