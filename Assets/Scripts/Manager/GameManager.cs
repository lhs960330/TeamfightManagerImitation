using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Dictionary�� ����Ͽ� è�Ǿ��� ã�� �� �ְ� �մϴ�.
    [SerializeField] private Dictionary<Define.ChampionName, GameObject> championDictionary = new Dictionary<Define.ChampionName, GameObject>();

    private void Start()
    {
        Init();
        Manager.Team.SetTeam(Define.TeamColor.Blue, Define.ChampionName.fighter);
        Manager.Team.SetTeam(Define.TeamColor.Red, Define.ChampionName.archer);
    }

    void Init()
    {
        // �� è�Ǿ��� Dictionary�� �߰��մϴ�.
        GameObject archer = Manager.Resource.Load<GameObject>("Champions/archer");
        GameObject fighter = Manager.Resource.Load<GameObject>("Champions/fighter");

        // BaseChampion ������Ʈ�� �����ͼ� Dictionary�� ����
        championDictionary.Add(archer.GetComponent<BaseChampion>().Data.name, archer);
        championDictionary.Add(fighter.GetComponent<BaseChampion>().Data.name, fighter);
    }

    public BaseChampion FindChampion( Define.ChampionName name )
    {
        // Dictionary���� �̸����� GameObject�� ã��, �� �ȿ��� BaseChampion ������Ʈ�� �����ɴϴ�.
        if ( championDictionary.TryGetValue(name, out GameObject championObject) )
        {
            return championObject.GetComponent<BaseChampion>();
        }
        Debug.Log("è�Ǿ��� ����");
        return null; // è�Ǿ��� ã�� �� ������ null ��ȯ
    }
}
