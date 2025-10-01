using UnityEngine;
using TMPro;
using Unity.Netcode;

public class ScoreManager : NetworkBehaviour
{
    public static ScoreManager instance; // singleton
    [SerializeField] private TMP_Text scoreTxtPlayer1;
    [SerializeField] private TMP_Text scoreTxtPlayer2;
    [SerializeField] private int pointageCible;
    private NetworkVariable<int> scoreHote = new NetworkVariable<int>();
    private NetworkVariable<int> scoreClient = new NetworkVariable<int>();
    public GameObject pannelVictoire; 
    public GameObject pannelDefaite; 
    public GameObject scoreUI; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsServer)
        {
            scoreHote.Value = 0;
            scoreClient.Value = 0;
        }

        scoreHote.OnValueChanged += OnChangementPointageHote;
        scoreClient.OnValueChanged += OnChangementPointageClient;
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        scoreHote.OnValueChanged -= OnChangementPointageHote;
        scoreClient.OnValueChanged -= OnChangementPointageClient;
    }

    public void AugmenteHoteScore()
    {
        scoreHote.Value++;
        VerifieFinPartie();
    }

    public void AugmenteScoreClient()
    {
        scoreClient.Value++;
        VerifieFinPartie();
    }

    private void OnChangementPointageHote(int ancienScoreHote, int nouveauScoreHote)
    {
        if (ancienScoreHote == nouveauScoreHote) return; 

        scoreTxtPlayer1.text = scoreHote.Value + "";
    }

    private void OnChangementPointageClient(int ancienScoreClient, int nouveauScoreClient)
    {
        if (ancienScoreClient == nouveauScoreClient) return; 

        scoreTxtPlayer2.text = scoreClient.Value + "";
    }

    void VerifieFinPartie()
    {
        if (scoreHote.Value >= pointageCible)
        {
            GagnantHote_ClientRpc();
        }
        else if (scoreClient.Value >= pointageCible)
        {
            GagnantClient_ClientRpc();
        }
    }

    [Rpc(SendTo.Everyone)]
    private void GagnantHote_ClientRpc()
    {

        if (IsServer)
        {
            ResetScoreRpc();
            pannelVictoire.SetActive(true);
        }
        else
        {
            pannelDefaite.SetActive(true);
        }
    }

    [Rpc(SendTo.Everyone)]
    private void ResetScoreRpc()
    {
        if (IsServer)
        {
            scoreHote.Value = 0;
            scoreClient.Value = 0;
        }

        scoreTxtPlayer1.text = "0";
        scoreTxtPlayer2.text = "0";

        scoreUI.SetActive(false);
        Cursor.visible = true;
    }

    [Rpc(SendTo.Everyone)]
    private void GagnantClient_ClientRpc()
    {

        if (IsServer)
        {
            ResetScoreRpc();
            pannelDefaite.SetActive(true);
        }
        else
        {
            pannelVictoire.SetActive(true);
        }
    }
}
