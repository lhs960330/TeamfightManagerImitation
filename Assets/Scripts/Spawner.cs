using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Define.TeamColor color;
    private void Start()
    {
        BaseSpawn();
    }
    // 팀 색상에 따라 챔피언을 스폰
    void BaseSpawn()
    {
        Define.TeamColor teamColor = color;

        // 팀에 해당하는 챔피언 리스트 가져오기
        List<BaseChampion> champions = Manager.Team.GetChampionsByTeam(teamColor);

        foreach ( BaseChampion champion in champions )
        {
            // 챔피언의 Prefab을 게임 씬에 인스턴스화
            GameObject spawnedChampion = Instantiate(champion.gameObject, transform.position, Quaternion.identity);

            // Y 값을 -1에서 1까지 랜덤으로 설정
            float randomY = Random.Range(-1f, 1f);

            // 랜덤 Y 값을 적용하여 챔피언의 위치 설정
            spawnedChampion.transform.position = new Vector3(transform.position.x, randomY, transform.position.z);
        }
    }
}
