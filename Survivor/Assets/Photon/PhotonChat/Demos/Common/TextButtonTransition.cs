// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextButtonTransition.cs" company="Exit Games GmbH">
// </copyright>
// <summary>
//  Use this on Button texts to have some color transition on the text as well without corrupting button's behaviour.
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Photon.Chat.UtilityScripts
{
    /// <summary>
    /// Use this on Button texts to have some color transition on the text as well without corrupting button's behaviour.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class TextButtonTransition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region Public Fields

        /// <summary>
        /// The color of the hover of the transition state.
        /// </summary>
        public Color HoverColor = Color.black;

        /// <summary>
        /// The color of the normal of the transition state.
        /// </summary>
        public Color NormalColor = Color.white;

        /// <summary>
        /// The selectable Component.
        /// </summary>
        public Selectable Selectable;

        #endregion Public Fields



        #region Private Fields

        private Text _text;

        #endregion Private Fields



        #region Public Methods

        public void Awake()
        {
            _text = GetComponent<Text>();
        }

        public void OnDisable()
        {
            _text.color = NormalColor;
        }

        public void OnEnable()
        {
            _text.color = NormalColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Selectable == null || Selectable.IsInteractable())
            {
                _text.color = HoverColor;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Selectable == null || Selectable.IsInteractable())
            {
                _text.color = NormalColor;
            }
        }

        #endregion Public Methods
    }
}