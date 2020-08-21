using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    private bool hasEnded = false;

    public void EndGame (){
        if(hasEnded)
            return;
        hasEnded = true;
        StartCoroutine(PlayEndGameAnimation ());
    }

    IEnumerator PlayEndGameAnimation ()
    {
        Debug.Log("GG");

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
    }
}
