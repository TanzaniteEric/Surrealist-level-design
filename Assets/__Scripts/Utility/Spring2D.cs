using UnityEngine;

    public class Spring2D : MonoBehaviour
    {
        public Vector2 position = Vector2.zero;
        public Vector2 velocity = Vector2.zero;
        public Vector2 equilibriumPosition = Vector2.zero;

        public float angularFrequency;
        public float dampingRatio;

        void Update()
        {
            SpringMotion.CalcDampedSimpleHarmonicMotion(ref position, ref velocity, equilibriumPosition, Time.deltaTime, angularFrequency, dampingRatio);
        }

    }
