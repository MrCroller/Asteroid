using UnityEngine;

public class Asteroids_Manager : MonoBehaviour
{
   public GameObject asteroidPrefab;

   /// <summary>
   /// Частота появления астероидов
   /// </summary>
   public float spawnRate = 2f;
   public float trajectoryVariance = 15f;

   /// <summary>
   /// Дистанция спауна
   /// </summary>
   public float spawnDistance = 15f;

   /// <summary>
   /// Кол-во астероидов
   /// </summary>
   public int _astCount = 2;

   void Start()
   {
      Spawn();
   }

   /// <summary>
   /// Появление астероида
   /// </summary>
   private void Spawn()
   {
      for (int i = 0; i < _astCount; i++)
      {

         Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
         Vector3 spawnPoint = this.transform.position + spawnDirection;

         float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
         Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);


         Asteroid asteroid = PoolManager.GetGameObjectFromPool(asteroidPrefab, spawnPoint, rotation).GetComponent<Asteroid>();
         asteroid.SetTrajectory(rotation * -spawnDirection);
      }
      _astCount++;
   }
}
