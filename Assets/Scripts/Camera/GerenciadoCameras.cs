using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GerenciadoCameras : MonoBehaviour
{
    public static GerenciadoCameras instance;

    [SerializeField] private CinemachineVirtualCamera[] todasCamerasVirutais;

    [Header ("Controla o lerp Y durante a queda")]
    [SerializeField] private float quantidadePanQueda = 0.25f;
    [SerializeField] private float tempoPanQueda = 0.35f;

    public float velocidadeQuedaEmYTreshold = -15f;

    public bool EstaFazendoDamp { get; private set; }
    public bool LerpedDoPlayerCaindo { get; set; }

    private CinemachineVirtualCamera cameraAtual;
    private CinemachineFramingTransposer framingTransposer;

    private float quantidadePanYPadrao;

    private Coroutine lerpYCoroutine;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        for (int i = 0; i < todasCamerasVirutais.Length; i++)
        {
            if (todasCamerasVirutais[i].enabled)
            {
                cameraAtual = todasCamerasVirutais[i];

                framingTransposer = cameraAtual.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }
    }

    private void Start()
    {
        quantidadePanYPadrao = framingTransposer.m_YDamping;
    }

    public void LerpDamping(bool jogadorEstaCaindo)
    {
        lerpYCoroutine = StartCoroutine(LerpYCamera(jogadorEstaCaindo));
    }

    private IEnumerator LerpYCamera(bool jogadorEstaCaindo)
    {
        EstaFazendoDamp = true;

        float valorDampInicial = framingTransposer.m_YDamping;
        float valorDampFinal = 0;

        if (jogadorEstaCaindo)
        {
            valorDampFinal = quantidadePanQueda;
            LerpedDoPlayerCaindo = true;
        }
        else
        {
            valorDampFinal = quantidadePanYPadrao;
        }

        float tempo = 0;

        while(tempo < tempoPanQueda)
        {
            tempo += Time.deltaTime;

            float lerpedPanQuantidade = Mathf.Lerp(valorDampInicial, valorDampFinal, (tempo / tempoPanQueda));
            framingTransposer.m_YDamping = lerpedPanQuantidade;

            yield return null;
        }

        EstaFazendoDamp = false;
    }
}
