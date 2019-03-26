using UnityEngine;

namespace audience
{

    public class Effects
    {
        float _rate;
        float _midScale;
        float _ratio;
        private Vector3 scale;

        public Effects(float rate, float midScale, float ratio)
        {
            _rate = rate;
            _midScale = midScale;
            _ratio = ratio;
        }

        public void GrowShrink(Transform transform, int touches = 0)
        {
            var factor = touches * 0.3f;
            float scaleComponent = _midScale * Mathf.Pow(_ratio, Mathf.Sin(Time.time * _rate)) + factor;
            for (int i = 0; i < 3; i++)
            {
                scale = scaleComponent * Vector3.one;
                transform.localScale = scale;
            }
        }

    }

}