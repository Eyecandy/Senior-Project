using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Prototype.NetworkLobby
{
    public class LobbyTopPanel : MonoBehaviour
    {
        public bool isInGame = false;
        public Button quitButton;
        public GameObject Title;

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
            {
                if (isDisplayed)
                {
                    isDisplayed = false;
                    Title.SetActive(true);
                    EnableNetworkManagerHud(isDisplayed);
                }
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleVisibility(!isDisplayed);
                EnableNetworkManagerHud(!isDisplayed);
            }
            quitButton.gameObject.SetActive(false);
            Title.SetActive(false);

        }
        
        public void DisableQuitButton()
        {
            quitButton.gameObject.SetActive(false);
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