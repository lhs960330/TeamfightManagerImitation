using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Dictionary를 사용하여 챔피언을 찾을 수 있게 합니다.
    [SerializeField] private Dictionary<Define.ChampionName, GameObject> championDictionary = new Dictionary<Define.ChampionName, GameObject>();

    private void Start()
    {
        Init();
        Manager.Team.SetTeam(Define.TeamColor.Blue, Define.ChampionName.fighter);
        Manager.Team.SetTeam(Define.TeamColor.Red, Define.ChampionName.archer);
    }

    void Init()
    {
        // 각 챔피언을 Dictionary에 추가합니다.
        GameObject archer = Manager.Resource.Load<GameObject>("Champions/archer");
        GameObject fighter = Manager.Resource.Load<GameObject>("Champions/fighter");

        // BaseChampion 컴포넌트를 가져와서 Dictionary에 저장
        championDictionary.Add(archer.GetComponent<BaseChampion>().Data.name, archer);
        championDictionary.Add(fighter.GetComponent<BaseChampion>().Data.name, fighter);
    }

    public BaseChampion FindChampion( Define.ChampionName name )
    {
        // Dictionary에서 이름으로 GameObject를 찾고, 그 안에서 BaseChampion 컴포넌트를 가져옵니다.
        if ( championDictionary.TryGetValue(name, out GameObject championObject) )
        {
            return championObject.GetComponent<BaseChampion>();
        }
        Debug.Log("챔피언이 없음");
        return null; // 챔피언을 찾을 수 없으면 null 반환
    }
}
