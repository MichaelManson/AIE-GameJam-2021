using System.Threading.Tasks;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public GameObject playerTestPrefab;

    private void Start()
    {
        //SpawnPlayers();
    }

    private async void SpawnPlayers()
    {
        var p = Instantiate(playerTestPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        p.Wins = 12;

        await Task.Delay(500);
        
        Instantiate(playerTestPrefab, Vector3.zero, Quaternion.identity);
        
        await Task.Delay(500);

        Instantiate(playerTestPrefab, Vector3.zero, Quaternion.identity);
    }
}
