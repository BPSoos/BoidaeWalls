using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    
    private bool _hasEnded = false;

    [FormerlySerializedAs("dead_players")] public int deadPlayers = 0;

    public void EndGame (){
        if(_hasEnded)
            return;
        _hasEnded = true;
        StartCoroutine(PlayEndGameAnimation ());
    }

    IEnumerator PlayEndGameAnimation ()
    {
        Debug.Log("GG");

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
    }
}
