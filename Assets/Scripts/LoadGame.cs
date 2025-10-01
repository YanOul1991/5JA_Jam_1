using UnityEngine;
using UnityEngine.SceneManagement;

/* 
    Script pour charger la scène du jeu ce qui permettra d'initialiser le serveur
*/
public class LoadGame : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("JeuMultiBrickJam");
    }
}
