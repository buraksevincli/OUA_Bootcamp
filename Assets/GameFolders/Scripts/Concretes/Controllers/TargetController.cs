using UnityEngine;

namespace GameFolders.Scripts.Concretes.Controllers
{
    public class TargetController : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private GameObject explodeEffect;

        public void TakeDamage(float amount)
        {
            health -= amount;

            if (health <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            Instantiate(explodeEffect, transform.position, transform.rotation);

            Destroy(this.gameObject);
        }
    }
}
