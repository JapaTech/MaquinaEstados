using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEscada_Estado : PlayerBase_Estado
{
    public PlayerEscada_Estado(PlayerMaquinaDeEstados _contextoAtual, Player_StateFactory _factory)
       : base(_contextoAtual, _factory) {
        eUmEstadoRaiz = true;
        InicializaSubestado();
            }

    public override void InicializaEstado()
    {
        
        ctx.Rb.gravityScale = 0;
    }

    public override void AtualizaEstado()
    {
        ctx.CalculoMovimentosY = ctx.InputsY * ctx.VelEscada;
        ChecaTrocaDeEstado();
    }

    public override void ChecaTrocaDeEstado()
    {
        if (!ctx.PodeSubir && ctx.NoChao)
        {
            TrocaEstados(fabrica.NoChao());
        }
        if (!ctx.NoChao && !ctx.PodeSubir)
        {
            Debug.Log("Caindo");
            TrocaEstados(fabrica.Caindo());
        }
    }

    public override void FinalizaEstado()
    {
        ctx.Rb.gravityScale = ctx.GravityScaleIni;
        ctx.PediuPular = false;
        Debug.Log("Finalizou Escada");
    }

    public override void InicializaSubestado()
    {
        if (ctx.InputsX != 0)
        {
            DefinaSubestado(fabrica.Andando());
        }
        else if (ctx.InputsX == 0)
        {
            DefinaSubestado(fabrica.Parado());
        }
    }


    
}
