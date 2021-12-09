using UnityEngine;

public class Testing : MonoBehaviour
{
    public GameObject playerTestPrefab;

    private void Start()
    {
        Instantiate(playerTestPrefab);
        Instantiate(playerTestPrefab);
        Instantiate(playerTestPrefab);
        var i = Instantiate(playerTestPrefab);
        Instantiate(playerTestPrefab);
        Instantiate(playerTestPrefab);

        PlayerManager.Instance.RemovePlayer(i.GetComponent<Player>());
        
        print("BREAK ************");

        foreach (var p in PlayerManager.Instance.players)
        {
            print(p.PlayerNumber);
        }
    }
}
