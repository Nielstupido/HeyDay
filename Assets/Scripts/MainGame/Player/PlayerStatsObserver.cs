using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsObserver : MonoBehaviour
{
    public delegate void OnPlayerStatChanged(PlayerStats statName, IDictionary<PlayerStats, float> playerStatsDict);
    public static OnPlayerStatChanged onPlayerStatChanged;
}
