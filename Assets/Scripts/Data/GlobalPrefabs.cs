using UnityEngine;

[CreateAssetMenu(fileName = "Prefabs Data", menuName = "ScriptableObjects/Global Prefabs", order = 0)]
public sealed class GlobalPrefabs : ScriptableObject
{
  [field: SerializeField] public GameObject PlayerMallet { get; private set; }
  [field: SerializeField] public GameObject Puck { get; private set; }
  [field: SerializeField] public GameObject Powerup { get; private set; }
  [field: SerializeField] public GameObject PowerupUiItem { get; private set; }
}
