using UnityEngine;

public class Asteroids_Manager : MonoBehaviour
{
   public GameObject asteroidPrefab;
   public float trajectoryVariance = 15f;

   /// <summary>
   /// Дистанция спауна
   /// </summary>
   public float spawnDistance = 15f;

   /// <summary>
   /// Кол-во астероидов
   /// </summary>
   public int astCount = 2;
   private int _astSmallCount;

   void Start()
   {
      Spawn();
      Asteroid.singelton.destroySmallAst += AstDestroy;
   }

   /// <summary>
   /// Появление астероида
   /// </summary>
   private void Spawn()
   {
      _astSmallCount = astCount * 4;
      for (int i = 0; i < astCount; i++)
      {
         //TODO Задать для медленных астероидов дистанцию появления ближе
         Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
         Vector3 spawnPoint = this.transform.position + spawnDirection;

         float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
         Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);


         Asteroid asteroid = PoolManager.GetGameObjectFromPool(asteroidPrefab, spawnPoint, rotation).GetComponent<Asteroid>();
         asteroid.SetTrajectory(rotation * -spawnDirection);
      }
      astCount++;
   }

   public void AstDestroy()
   {
      Debug.Log($"astSmallCount = {_astSmallCount}");
      _astSmallCount--;
      if (_astSmallCount <= 0) Spawn();
   }
}
