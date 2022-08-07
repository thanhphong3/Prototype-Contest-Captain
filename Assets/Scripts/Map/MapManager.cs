using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static MapManager Instance;
    [SerializeField] int m_level;
    [SerializeField] List<GameObject> ListEnemy;
    [SerializeField] List<LevelConfig> ListLevel;
    [SerializeField] Transform MapMatrix;
    [SerializeField] Transform ContainerEnemy_A;
    [SerializeField] Transform ContainerEnemy_B;
    [SerializeField] Transform PointStart_A;
    [SerializeField] Transform PointStart_B;

    private LevelConfig m_levelCurrentConfig;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }
    public void Init()
    {
        GetCurrentLevel();
        GetLevelCurrentConfig();
        StartCoroutine(SpawnLevel());
    }
    private void GetCurrentLevel()
    {

    }
    private void GetLevelCurrentConfig()
    {
        m_levelCurrentConfig = ListLevel[m_level];
    }

    IEnumerator SpawnLevel()
    {
        List<ItemPhase> ListPhase = m_levelCurrentConfig.ListPhase;

        foreach (var item in ListPhase)
        {
            // while (!CheckNullEnemy())
            // {
            //     yield return null;
            // }
            yield return SpawnPhase(item);
        }
    }
    IEnumerator SpawnPhase(ItemPhase itemPhase)
    {
        List<ItemTurn> ListItemTurn = itemPhase.ListItemTurn;

        foreach (var item in ListItemTurn)
        {
            yield return SpawnTurn(item);
        }
    }
    IEnumerator SpawnTurn(ItemTurn itemTurn)
    {
        yield return new WaitForSeconds(itemTurn.timeAppear);

        List<ItemEnemy> ListEnemy_A = itemTurn.ListEnemy_A;
        List<ItemEnemy> ListEnemy_B = itemTurn.ListEnemy_B;

        foreach (var item in ListEnemy_A)
        {
            SpawnEnemy(item, "A");
        }
        foreach (var item in ListEnemy_B)
        {
            SpawnEnemy(item, "B");
        }
    }
    public void SpawnEnemy(ItemEnemy itemEnemy, string name)
    {
        GameObject model = GetModelEnemy(((int)itemEnemy.enemyType));
        Vector3 posEnd = GetPositionCell(itemEnemy.indexAppear);
        Vector3 posStart;

        if (name == "A")
        {
            posStart = PointStart_A.position;
            posStart.z = posEnd.z;

            GameObject enemy = Instantiate(model, posStart, Quaternion.identity);
            enemy.transform.parent = ContainerEnemy_A;
            enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
            enemy.GetComponent<CombatVehicle>().Move(posEnd);
        }
        if (name == "B")
        {
            posStart = PointStart_B.position;
            posStart.z = posEnd.z;

            GameObject enemy = Instantiate(model, posStart, Quaternion.identity);
            enemy.transform.parent = ContainerEnemy_B;
            enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
            enemy.GetComponent<CombatVehicle>().Move(posEnd);
        }
    }
    private Vector3 GetPositionCell(int index)
    {
        return MapMatrix.GetChild(index).position;
    }
    private GameObject GetModelEnemy(int index)
    {
        return ListEnemy[index];
    }
    public bool CheckNullEnemy()
    {
        return ContainerEnemy_A.childCount == 0 && ContainerEnemy_B.childCount == 0;
    }
}
