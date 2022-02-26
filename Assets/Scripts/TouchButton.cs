using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    /// <summary>
    /// Tento skript predstavuje jednoduchou logiku tlacitka pro dotykove obrazovky
    /// </summary>
    public class TouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool Touched { get; private set; }
        private int pointerID;
        void Awake()
        {
            Touched = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // Set our start point
            if (!Touched)
            {
                Touched = true;
                pointerID = eventData.pointerId;
            }
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // Reset Every thing
            if (eventData.pointerId == pointerID)
            {
                Touched = false;
            }
        }
    }
}