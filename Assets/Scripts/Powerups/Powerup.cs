using System;
using UnityEngine;

public enum PowerupEffects
{
  grow,
  shrink,
  slow,
  bigPuck,
  smallPuck,
  reverseControls,
  Count
}

[Serializable]
public struct Powerup
{
  public ulong obj;
  public PowerupEffects effect;
}