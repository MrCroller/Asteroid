using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
   enum Size
   {
      SMALL = 1,
      MEDIUM = 4,
      BIG = 8
   }
   [SerializeField] private Size _size = Size.BIG;
   public GameObject asteroidPrefab;

   /// <summary>
   /// Минимальная скорость
   /// </summary>
   [Range(0.1f, 26f)] public float minSpeed = 1f;

   /// <summary>
   /// Максимальная скорость
   /// </summary>
   [Range(0.1f, 26f)] public float maxSpeed = 2f;

   /// <summary>
   /// Скорость
   /// </summary>
   [SerializeField] private float speed;

   /// <summary>
   /// Делитель размера астероида
   /// </summary>
   [Tooltip("Делитель размера астероида. Чем больше => тем меньше астероид в размерах")] public float attech = 10f;
   private Rigidbody2D _rb;

   /// <summary>
   /// Спрайты астероидов
   /// </summary>
   public Sprite[] sprites;
   private SpriteRenderer _spriteRender;

   private void Awake()
   {
      _rb = GetComponent<Rigidbody2D>();
      _spriteRender = GetComponent<SpriteRenderer>();
   }

   private void Start()
   {
      // Выбор случайного спрайта
      _spriteRender.sprite = sprites[Random.Range(0, sprites.Length)];
      this.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360);

      // Выбор цвета в градациях серого
      float color_value = Random.Range(0.1f, 0.9f);
      _spriteRender.color = new Color(color_value, color_value, color_value);
   }

   /// <summary>
   /// Траектория падения
   /// </summary>
   /// <param name="direction"></param>
   public void SetTrajectory(Vector2 direction)
   {
      // Изменение размера
      _rb.mass = (int)_size;
      this.transform.localScale = (Vector3.one * (int)_size) / attech;

      if (minSpeed > maxSpeed) Debug.LogError("Минимальное значение скорости больше максимального");
      speed = Random.Range(minSpeed, maxSpeed);
      _rb.AddForce(direction * speed);
   }


   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Bullet"))
      {
         PoolManager.PutGameobjectToPool(other.gameObject);
         Destroy();
      }
   }

   /// <summary>
   /// Уничтожение астероида
   /// </summary>
   private void Destroy()
   {
      if (_size != Size.SMALL)
      {
         for (int i = 0; i < 2; i++)
         {
            Vector3 newDirection = this.transform.position;
            Quaternion rndRot = Quaternion.AngleAxis(Random.Range(-45f, 45f), Vector3.forward);
            Asteroid ast = PoolManager.GetGameObjectFromPool(asteroidPrefab, this.transform.position, rndRot).GetComponent<Asteroid>();
            Debug.Log("Create");

            // ast._size--; //Уменьшение размера на 1
            SetParam(ref ast._size, ref ast.speed); //FIXME после Destroy() BIG Size появляющиеся астероиды разных размеров (MEDIUM и SMALL)
            ast.SetTrajectory(rndRot * -newDirection);
         }
      }
      PoolManager.PutGameobjectToPool(this.gameObject);
      Debug.Log("Exploution!");
   }

   /// <summary>
   /// Изменение размера и траектории
   /// </summary>
   /// <param name="size">Размер объекта</param>
   /// <param name="speed">Скорость объекта</param>
   private void SetParam(ref Size size, ref float speed)
   {
      if (size == Size.BIG)
      {
         size = Size.MEDIUM;
         speed *= 2;
      }
      else if (size == Size.MEDIUM)
      {
         size = Size.SMALL;
         speed *= 2;
      }
   }
}
