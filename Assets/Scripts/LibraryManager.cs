using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class LibraryManager : MonoBehaviour
    {
        public static LibraryManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        public CardLibrary CardLibrary;
    }
}