using Unity.Netcode;
using UnityEngine;

public class PuckPhysics : NetworkBehaviour
{
  public static PuckPhysics Singleton;
  private ulong m_lastPlayerHit;
  [field: SerializeField] private AudioClip m_sfxPowerupHit;
  [field: SerializeField] private AudioClip m_sfxPuckHit;


  private void Awake()
  {
    if (Singleton == null)
      Singleton = this;
    else
      Destroy(gameObject);
  }

  void OnCollisionEnter(Collision collision)
  {
    if (!IsServer) return;

    if (collision.gameObject.CompareTag("Player"))
    {
      m_lastPlayerHit = collision.gameObject.GetComponent<NetworkObject>().NetworkObjectId;
      PlaySfxPuckHit_Rpc();
    }

    if (collision.gameObject.CompareTag("Powerup"))
    {
      PowerupManager.Singleton.NetworkPowerupHit_Rpc(
        m_lastPlayerHit,
        collision.transform.parent.GetComponent<NetworkObject>().NetworkObjectId
      );
      PlaySfxPowerupHit_Rpc();
    }

    if (collision.gameObject.CompareTag("Wall"))
    {
      PlaySfxPuckHit_Rpc();
    }
  }
  

  [Rpc(SendTo.Everyone)]
  private void PlaySfxPowerupHit_Rpc()
  {
    Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(m_sfxPowerupHit);
  }

  [Rpc(SendTo.Everyone)]
  private void PlaySfxPuckHit_Rpc()
  {
    Camera.main.gameObject.GetComponent<AudioSource>().PlayOneShot(m_sfxPuckHit);
  }
}