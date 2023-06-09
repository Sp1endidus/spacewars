using UnityEngine;

namespace SpaceWars.Runtime.Gameplay.Model.Shooting.Ammo {
    public abstract class AmmoBase : MonoBehaviour {
        public abstract AmmoType AmmoType { get; }
    }
}