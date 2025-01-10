using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public List<GameObject> redTeam = new List<GameObject>();
    [SerializeField] public List<GameObject> blueTeam = new List<GameObject>();

}
