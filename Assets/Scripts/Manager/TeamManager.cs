using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : Singleton<TeamManager>
{
    // 팀 색상별 챔피언 리스트를 관리하는 딕셔너리
    Dictionary<Define.TeamColor, List<BaseChampion>> TeamDir = new Dictionary<Define.TeamColor, List<BaseChampion>>();

    public void SetTeam( Define.TeamColor color, Define.ChampionName name )
    {
        // FindChampion을 통해 챔피언을 찾기
        BaseChampion champion = Manager.Game.FindChampion(name);

        if ( champion != null )
        {
            // 팀 색상에 해당하는 리스트가 없으면 새로 생성
            if ( !TeamDir.ContainsKey(color) )
            {
                TeamDir [color] = new List<BaseChampion>();
            }

            // 해당 팀에 챔피언 추가
            TeamDir [color].Add(champion);
        }
        else
        {
            Debug.LogWarning("챔피언을 찾을 수 없습니다: " + name);
        }
    }
    // 특정 팀에 속한 챔피언 리스트를 반환하는 메서드 (필요시 사용)
    public List<BaseChampion> GetChampionsByTeam( Define.TeamColor color )
    {
        if ( TeamDir.ContainsKey(color) )
        {
            return TeamDir [color];
        }
        return null;
    }
}
