using UnityEngine;
using UnityEngine.Serialization;


namespace TMPro.Examples
{
    public class TMPTextEventCheck : MonoBehaviour
    {

        [FormerlySerializedAs("TextEventHandler")] public TMPTextEventHandler textEventHandler;

        private TMP_Text _mTextComponent;

        void OnEnable()
        {
            if (textEventHandler != null)
            {
                // Get a reference to the text component
                _mTextComponent = textEventHandler.GetComponent<TMP_Text>();
                
                textEventHandler.ONCharacterSelection.AddListener(OnCharacterSelection);
                textEventHandler.ONSpriteSelection.AddListener(OnSpriteSelection);
                textEventHandler.ONWordSelection.AddListener(OnWordSelection);
                textEventHandler.ONLineSelection.AddListener(OnLineSelection);
                textEventHandler.ONLinkSelection.AddListener(OnLinkSelection);
            }
        }


        void OnDisable()
        {
            if (textEventHandler != null)
            {
                textEventHandler.ONCharacterSelection.RemoveListener(OnCharacterSelection);
                textEventHandler.ONSpriteSelection.RemoveListener(OnSpriteSelection);
                textEventHandler.ONWordSelection.RemoveListener(OnWordSelection);
                textEventHandler.ONLineSelection.RemoveListener(OnLineSelection);
                textEventHandler.ONLinkSelection.RemoveListener(OnLinkSelection);
            }
        }


        void OnCharacterSelection(char c, int index)
        {
            Debug.Log("Character [" + c + "] at Index: " + index + " has been selected.");
        }

        void OnSpriteSelection(char c, int index)
        {
            Debug.Log("Sprite [" + c + "] at Index: " + index + " has been selected.");
        }

        void OnWordSelection(string word, int firstCharacterIndex, int length)
        {
            Debug.Log("Word [" + word + "] with first character index of " + firstCharacterIndex + " and length of " + length + " has been selected.");
        }

        void OnLineSelection(string lineText, int firstCharacterIndex, int length)
        {
            Debug.Log("Line [" + lineText + "] with first character index of " + firstCharacterIndex + " and length of " + length + " has been selected.");
        }

        void OnLinkSelection(string linkID, string linkText, int linkIndex)
        {
            if (_mTextComponent != null)
            {
                TMP_LinkInfo linkInfo = _mTextComponent.textInfo.linkInfo[linkIndex];
            }
            
            Debug.Log("Link Index: " + linkIndex + " with ID [" + linkID + "] and Text \"" + linkText + "\" has been selected.");
        }

    }
}
