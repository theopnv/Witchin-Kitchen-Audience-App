using UnityEngine;
using UnityEngine.UI;

namespace audience.game
{
    public class PlayerStateManager : MonoBehaviour
    {
        [SerializeField] private Text _NameText;
        private string _Name;

        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                _NameText.text = _Name;
            }
        }

        [SerializeField] private Text _ScoreText;
        private int _Score;

        public int Score
        {
            get => _Score;
            set
            {
                _Score = value;
                _ScoreText.text = "Score: " + Score;
            }
        }

        public Image PlusOneImage;

        [SerializeField] private Image _Character;

        public void SetCharacter(string pathToSprite)
        {
            var sprite = Resources.Load<Sprite>(pathToSprite);
            if (sprite)
            {
                _Character.sprite = sprite;
            }
        }
    }
}