using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAndando_Estado : PlayerBase_Estado
{
    public PlayerAndando_Estado(PlayerMaquinaDeEstados _contextoAtual, Player_StateFactory _factory)
       : base(_contextoAtual, _factory) { }

    public override void AtualizaEstado()
    {
        ChecaTrocaDeEstado();

        //Faz o cálculo de movimentos do jogador
        ctx.CalculoMovimentosX = ctx.InputsX * ctx.VelMovimento;
    }

    public override void ChecaTrocaDeEstado()
    {
        if (ctx.InputsX == 0)
        {
            TrocaEstados(fabrica.Parado());
        }
        else if (ctx.PodeDarDash && ctx.PediuDash)
        {
            TrocaEstados(fabrica.Dash());
        }
    }

    public override void FinalizaEstado()
    {

    }

    public override void InicializaSubestado()
    {
        
    }

    public override void InicializaEstado()
    {
        MostraEstado.Instancia.MostraTextoSubestado("Andando");
    }
}
