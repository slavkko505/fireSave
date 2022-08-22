using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
   [SerializeField] private GameObject axeInHande;
   [SerializeField] private GameObject axeInShulders;
   [SerializeField] private GameObject bowInShoulders;
   [SerializeField] private GameObject bowInHand;
   
   public static WeaponController instance;

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
      }
      else
      {
         Destroy(gameObject);
      }
   }

   private void Start()
   {
      bowInHand.SetActive(false);
      axeInShulders.SetActive(false);
   }

   public bool IsMeelAttack()
   {
      return  axeInHande.activeSelf;
   }
   
   public void EquipBow()
   { 
      StartCoroutine(EquipingBow());
   }

   IEnumerator EquipingBow()
   {
      yield return new WaitForSecondsRealtime(0.2f);
      
      axeInHande.SetActive(!axeInHande.activeSelf);
      axeInShulders.SetActive(!axeInShulders.activeSelf);
      
      bowInShoulders.SetActive(!bowInShoulders.activeSelf);
      bowInHand.SetActive(!bowInHand.activeSelf);

   }
   
}
