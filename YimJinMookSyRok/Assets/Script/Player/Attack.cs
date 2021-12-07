using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Script.Player
{
    public class Attack : MonoBehaviour
    {
        private BoxCollider2D m_Bc;
        private const string ENEMY_TAG = "Enemy";

        private void Awake()
        {
            m_Bc = GetComponent<BoxCollider2D>();
        }

        private void OnEnable()
        {
            m_Bc.enabled = true;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag(ENEMY_TAG))
            {
                if (other.gameObject.TryGetComponent<PlayerController>(out var p))
                {
                    p.Hit(1);
                    gameObject.SetActive(false);
                }

                Debug.Log("Hit");
            }
        }
    }
}