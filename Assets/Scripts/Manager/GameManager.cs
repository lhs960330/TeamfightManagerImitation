using System.Collections.Generic;
using UnityEngine;
// 순서대로
public enum ChampionName { archer, fighter, }
public enum TeamColor { Red, Blue }
public class GameManager : Singleton<GameManager>
{
    Dictionary<TeamColor, GameData> TeamDir = new Dictionary<TeamColor, GameData>();
    [SerializeField] List<GameObject> Champions = new List<GameObject>();

    void Init()
    {

    }
    public void SetTeam( ChampionName name, TeamColor color )
    {

    }
}
