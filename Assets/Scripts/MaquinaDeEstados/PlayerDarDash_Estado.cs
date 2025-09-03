using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDarDash_Estado : PlayerBase_Estado
{
    public PlayerDarDash_Estado(PlayerMaquinaDeEstados _contextoAtual, Player_StateFactory _factory)
      : base(_contextoAtual, _factory) { }

    public override void InicializaEstado()
    {
        MostraEstado.Instancia.MostraTextoSubestado("Dando dash");
        
        //Verifica se o dash está disponível
        if (!ctx.PodeDarDash && !ctx.EstaDandoDash)
        {
            ctx.PediuDash = false;
            TrocaEstados(fabrica.Andando());
        }
        else
        {
            ctx.PediuDash = false;
            //Zera a velocidade para não haver interferência
            ctx.Rb.velocity = Vector2.zero;
            //Começa o dash
            ctx.StardDash_Crtn();
        }
    }

    public override void AtualizaEstado()
    {
        //Debug.Log("Dando dash");
        //Impede mudanças no eixo Y (como a gravidade)
        ctx.CalculoMovimentosY = 0;
        ChecaTrocaDeEstado();
    }

    public override void ChecaTrocaDeEstado()
    {
        //Verifica se acabou de dar dash
        if(ctx.EstaDandoDash == false)
        {
            TrocaEstados(fabrica.Parado());
        }
    }

    public override void FinalizaEstado()
    {
  
    }

    public override void InicializaSubestado()
    {

    }

}
