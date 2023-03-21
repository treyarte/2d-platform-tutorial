using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFallingPlatform : MonoBehaviour
{
   [SerializeField] private float spawnDelay = 2f;
   [SerializeField] private GameObject fallingPlatform;

   private IEnumerator Spawn(Vector3 pos, Quaternion rot)
   {
      yield return new WaitForSeconds(spawnDelay);
      Instantiate(fallingPlatform, pos, rot);
   }
   
   public void RestoreFallingPlatform(Vector3 pos, Quaternion rot)
   {
      StartCoroutine(Spawn(pos, rot));
   }
}
