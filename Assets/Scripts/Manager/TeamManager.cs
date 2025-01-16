using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : Singleton<TeamManager>
{
    // �� ���� è�Ǿ� ����Ʈ�� �����ϴ� ��ųʸ�
    Dictionary<Define.TeamColor, List<BaseChampion>> TeamDir = new Dictionary<Define.TeamColor, List<BaseChampion>>();

    public void SetTeam( Define.TeamColor color, Define.ChampionName name )
    {
        // FindChampion�� ���� è�Ǿ��� ã��
        BaseChampion champion = Manager.Game.FindChampion(name);

        if ( champion != null )
        {
            // �� ���� �ش��ϴ� ����Ʈ�� ������ ���� ����
            if ( !TeamDir.ContainsKey(color) )
            {
                TeamDir [color] = new List<BaseChampion>();
            }

            // �ش� ���� è�Ǿ� �߰�
            TeamDir [color].Add(champion);
        }
        else
        {
            Debug.LogWarning("è�Ǿ��� ã�� �� �����ϴ�: " + name);
        }
    }
    // Ư�� ���� ���� è�Ǿ� ����Ʈ�� ��ȯ�ϴ� �޼��� (�ʿ�� ���)
    public List<BaseChampion> GetChampionsByTeam( Define.TeamColor color )
    {
        if ( TeamDir.ContainsKey(color) )
        {
            return TeamDir [color];
        }
        return null;
    }
}
