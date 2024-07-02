using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Storage
{
   public static void Save(string key, float value)
   {
      PlayerPrefs.SetFloat(key,value);
      PlayerPrefs.Save();
   } 
   
   public static float Load(string key, float defualtValue)
   {
       if (PlayerPrefs.HasKey(key))
       {
           return PlayerPrefs.GetFloat(key);
       }

       return defualtValue;

   }
   
   
}
