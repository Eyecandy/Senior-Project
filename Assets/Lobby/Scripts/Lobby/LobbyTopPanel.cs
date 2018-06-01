using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Prototype.NetworkLobby
{
    public class LobbyTopPanel : MonoBehaviour
    {
        public bool isInGame = false;

        protected bool isDisplayed = true;
        protected Image panelImage;

        void Start()
        {
            panelImage = GetComponent<Image>();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        void Update()
        {
            if (!isInGame)
                return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleVisibility(!isDisplayed);
                EnableNetworkManagerHud(!isDisplayed);
                
            }

        }
        
        private static void EnableNetworkManagerHud(bool isActive)
        {
            
            if (!isActive)
            {
			
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }

        public void ToggleVisibility(bool visible)
        {
            isDisplayed = visible;
            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(isDisplayed);
            }

            if (panelImage != null)
            {
                panelImage.enabled = isDisplayed;
            }
        }
    }
}