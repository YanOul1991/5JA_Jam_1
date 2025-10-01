using UnityEngine;
using Unity.Netcode; 
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
public class GameManager : NetworkBehaviour
{
    public static GameManager instance;
    public bool partieEnCours { private set; get; } 
    public bool partieTerminee { private set; get; } 

    void Awake()
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

    public void NouvellePartie()
    {
        partieEnCours = true;
        Puck.instance.PlacementPuck(new Vector3(0, 0, -5));
    }

    // Fonction appelée par le bouton Recommencer pour recommencer une partie
    public void Recommencer()
    {
        partieTerminee = true;
        NetworkPlayer.Singleton.Disconnect_Rpc();
    }
}
