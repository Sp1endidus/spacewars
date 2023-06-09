using UnityEngine;

namespace SpaceWars.Runtime.Signals.Gameplay {
    public readonly struct SignalPlayerBinding {
        public readonly GameObject Player;

        public SignalPlayerBinding(GameObject player) {
            Player = player;
        }
    }
}