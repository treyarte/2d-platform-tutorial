using System.Collections;
using UnityEngine;

namespace Manager_Scrips
{
   public class SpawnFallingPlatform : MonoBehaviour
   {
      [Range(0f, 5f)]
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
}
