using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
   /// <summary>
   /// Ускорение
   /// </summary>
   public float speed;
   /// <summary>
   /// Скорость поворота
   /// </summary>
   [Range(1, 10)] public float speed_rotate = 3.6f;
   private Rigidbody2D _rb;
   private SpriteRenderer _fireSprite;
   /// <summary>
   /// Вектор направления движения
   /// </summary>
   private Vector2 _moveVector;
   /// <summary>
   /// Нос корабля
   /// </summary>
   private Transform _nose;

   /// <summary>
   /// Префаб пули
   /// </summary>
   public GameObject bullet;
   /// <summary>
   /// Родительский объект для PM
   /// </summary>

   private void Awake()
   {
      _rb = GetComponent<Rigidbody2D>();
      _nose = GameObject.Find("Nose").GetComponent<Transform>();
      _fireSprite = GameObject.Find("Fire").GetComponent<SpriteRenderer>();
   }

   void Update()
   {
      Controller();
   }

   /// <summary>
   /// Метод управления кораблем
   /// </summary>
   private void Controller()
   {
      _moveVector = _nose.position - transform.position;

      if (Input.GetKeyDown(KeyCode.W))
      {
         _rb.AddForce(_moveVector * this.speed, ForceMode2D.Impulse);
         StopCoroutine(nameof(AnimFire));
         StartCoroutine(nameof(AnimFire));
      }
      // Ускорение в направлении вектора

      if (Input.GetKey(KeyCode.A))
         _rb.rotation += speed_rotate / 10;
      if (Input.GetKey(KeyCode.D))
         _rb.rotation -= speed_rotate / 10;
      // Повороты

      if (Input.GetKeyDown(KeyCode.Space))
      {
         Fire();
      }
   }

   private IEnumerator AnimFire()
   {
      for (int i = 0; i < 5; i++)
      {
         _fireSprite.enabled = true;
         yield return new WaitForSeconds(0.01f);
         _fireSprite.enabled = false;
      }
      yield return new WaitForSeconds(0.1f);
      _fireSprite.enabled = true;
      yield return new WaitForSeconds(0.5f);
      _fireSprite.enabled = false;
   }

   private void Fire()
   {
      PoolManager.GetGameObjectFromPool(bullet, _nose).GetComponent<Bullet>().StartForse(_moveVector);
   }
}
