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
        
        //Verifica se o dash est� dispon�vel
        if (!ctx.PodeDarDash && !ctx.EstaDandoDash)
        {
            ctx.PediuDash = false;
            TrocaEstados(fabrica.Andando());
        }
        else
        {
            ctx.PediuDash = false;
            //Zera a velocidade para n�o haver interfer�ncia
            ctx.Rb.velocity = Vector2.zero;
            //Come�a o dash
            ctx.StardDash_Crtn();
        }
    }

    public override void AtualizaEstado()
    {
        //Debug.Log("Dando dash");
        //Impede mudan�as no eixo Y (como a gravidade)
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
