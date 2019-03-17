using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace audience.game
{

    public class PlayerScore : MonoBehaviour
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

        [SerializeField] private Text _PotionsText;
        private int _Potions;

        public int Potions
        {
            get => _Potions;
            set
            {
                _Potions = value;
                _PotionsText.text = _Potions.ToString() + " potions";
            }
        }

        [SerializeField] private Text _IngredientsText;
        private int _Ingredients;

        public int Ingredients
        {
            get => _Ingredients;
            set
            {
                _Ingredients = value;
                _IngredientsText.text = _Ingredients.ToString() + " ingredients";
            }
        }

    }

}
