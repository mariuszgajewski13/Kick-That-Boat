using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WaveManager : MonoBehaviour
{
   public static WaveManager instance;

   private float amplitude;
   private float length;
   private float speed;
   private float offset;

   public Renderer rend;

   private void Awake()
   {
      if (instance == null)
         instance = this;
      else if (instance != this)
         Destroy(this);

      rend = GetComponent<Renderer>();
   }

   private void Update()
   {
      offset += Time.deltaTime * speed;
      amplitude = rend.material.GetFloat("_Wave_Amplitude");
      length = rend.material.GetFloat("_Wave_Length");
      speed = rend.material.GetFloat("_Wave_Speed");
   }

   public float GetWaveHeight(float _x) => amplitude * Mathf.Sin(_x / length + offset);
}
