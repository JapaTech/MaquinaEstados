using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgacha_Estado : PlayerBase_Estado
{
    public PlayerAgacha_Estado(PlayerMaquinaDeEstados _contextoAtual, Player_StateFactory _factory)
      : base(_contextoAtual, _factory)
    {
        eUmEstadoRaiz = true;
    }

    public override void AtualizaEstado()
    {
        //Previne o jogador de se mover enquanto agachado
        ctx.CalculoMovimentosX = 0f;
        ChecaTrocaDeEstado();

    }

    public override void ChecaTrocaDeEstado()
    {
        if (ctx.Agachado == false)
            TrocaEstados(fabrica.NoChao());
    }

    public override void FinalizaEstado()
    {
        //Previve trocas de estados caso o jogador tente pular ou dar dash agachado
        ctx.PediuDash = false;
        ctx.PediuPular = false;    
    }

    public override void InicializaEstado()
    {
        MostraEstado.Instancia.MostraTextoEstado("Agachado");
    }

    public override void InicializaSubestado()
    {
        
    }

    
}
