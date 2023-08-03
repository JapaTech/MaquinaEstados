using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCaindo_Estado : PlayerBase_Estado
{
    public PlayerCaindo_Estado(PlayerMaquinaDeEstados _contextoAtual, Player_StateFactory _factory)
      : base(_contextoAtual, _factory)
    {
        eUmEstadoRaiz = true;
        InicializaSubestado();
    }

    public override void InicializaEstado()
    {
        ctx.CalculoMovimentosY = ctx.Rb.velocity.y;
    }

    public override void AtualizaEstado()
    {
        ctx.CalculoMovimentosY = ctx.Rb.velocity.y;
        ChecaTrocaDeEstado();
    }

    public override void ChecaTrocaDeEstado()
    {
        if (ctx.NoChao == true)
            TrocaEstados(fabrica.NoChao());
        if (ctx.PodeSubir)
            TrocaEstados(fabrica.Escada());
    }

    public override void FinalizaEstado()
    {
        ctx.PediuPular = false;
    }

    public override void InicializaSubestado()
    {
        if (ctx.PediuDash && !ctx.EstaDandoDash)
        {
            DefinaSubestado(fabrica.Dash());
        }

        else if (ctx.InputsX != 0)
        {
            DefinaSubestado(fabrica.Andando());
        }

        else if (ctx.InputsX == 0)
        {
            DefinaSubestado(fabrica.Parado());
        }
    }

    
}
