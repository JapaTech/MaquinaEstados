using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPulo_Estado : PlayerBase_Estado
{
    public PlayerPulo_Estado(PlayerMaquinaDeEstados _contextoAtual, Player_StateFactory _factory)
       : base(_contextoAtual, _factory) {
        eUmEstadoRaiz = true;
        InicializaSubestado();
    }

    public override void InicializaEstado()
    {
        //Debug.Log("Pular");
        
        ctx.StartCoroutine("EstaNoPulo");
        ctx.CalculoMovimentosY = ctx.VelPulo;   

    }

    public override void AtualizaEstado()
    {
        //Debug.Log("Esta no pulo");
        ChecaTrocaDeEstado();
    }

    public override void ChecaTrocaDeEstado()
    {
        if (ctx.NoChao)
            TrocaEstados(fabrica.NoChao());
        else if (ctx.PodeSubir)
            TrocaEstados(fabrica.Escada());
        else if (ctx.EstaPulando == false && ctx.NoChao == false)
        {
            //ctx.CalculoMovimentosY = ctx.Rb.velocity.y;
            //Debug.Log("No Ar");
            TrocaEstados(fabrica.Caindo());
        }
    }

    public override void FinalizaEstado()
    {
    
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
