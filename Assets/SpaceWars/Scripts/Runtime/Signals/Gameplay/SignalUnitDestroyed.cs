using UnityEngine;

namespace SpaceWars.Runtime.Signals.Gameplay {
    public readonly struct SignalUnitDestroyed {
        public readonly GameObject Unit;

        public SignalUnitDestroyed(GameObject unit) {
            Unit = unit;
        }
    }
}