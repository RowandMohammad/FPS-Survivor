using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{

        public float TimeToLive = 1f;
        private void Start()
        {
            Destroy(gameObject, TimeToLive);
        }

    }
