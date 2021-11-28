// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Exit Games GmbH"/>
// <summary>Demo code for Photon Chat in Unity.</summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Photon.Chat.Demo
{
    public class ChannelSelector : MonoBehaviour, IPointerClickHandler
    {
        #region Public Fields

        public string Channel;

        #endregion Public Fields



        #region Public Methods

        public void OnPointerClick(PointerEventData eventData)
        {
            ChatGui handler = FindObjectOfType<ChatGui>();
            handler.ShowChannel(this.Channel);
        }

        public void SetChannel(string channel)
        {
            this.Channel = channel;
            Text t = this.GetComponentInChildren<Text>();
            t.text = this.Channel;
        }

        #endregion Public Methods
    }
}