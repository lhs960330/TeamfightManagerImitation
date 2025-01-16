using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Define.TeamColor color;
    private void Start()
    {
        BaseSpawn();
    }
    // �� ���� ���� è�Ǿ��� ����
    void BaseSpawn()
    {
        Define.TeamColor teamColor = color;

        // ���� �ش��ϴ� è�Ǿ� ����Ʈ ��������
        List<BaseChampion> champions = Manager.Team.GetChampionsByTeam(teamColor);

        foreach ( BaseChampion champion in champions )
        {
            // è�Ǿ��� Prefab�� ���� ���� �ν��Ͻ�ȭ
            GameObject spawnedChampion = Instantiate(champion.gameObject, transform.position, Quaternion.identity);

            // Y ���� -1���� 1���� �������� ����
            float randomY = Random.Range(-1f, 1f);

            // ���� Y ���� �����Ͽ� è�Ǿ��� ��ġ ����
            spawnedChampion.transform.position = new Vector3(transform.position.x, randomY, transform.position.z);
        }
    }
}
