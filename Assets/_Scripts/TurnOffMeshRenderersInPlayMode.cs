using System.Collections.Generic;
using UnityEngine;

public class TurnOffMeshRenderersInPlayMode : MonoBehaviour
{
   public List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

   void OnEnable()
   {
      foreach (MeshRenderer meshRenderer in meshRenderers)
      {
         meshRenderer.enabled = false;
      }
   }
}
