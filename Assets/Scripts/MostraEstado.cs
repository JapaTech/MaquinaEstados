using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MostraEstado: MonoBehaviour
{
    public static MostraEstado Instancia;

    [field: SerializeField] public TMP_Text estado { get; set; }
    [field: SerializeField] public  TMP_Text subEstado { get; set; }

    private void Awake()
    {
        if(Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MostraTextoEstado(string nome)
    {
       estado.text = $"Estado: {nome}";
    }

    public void MostraTextoSubestado(string nome)
    {
       subEstado.text = $"Subestado: {nome}";
    }
}
;