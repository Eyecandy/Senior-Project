
using Player_Scripts;
using UnityEngine;
using UnityEngine.UI;

public class NamePlate : MonoBehaviour
{
    [SerializeField] private Text _playerNameText;
    [SerializeField] private Player _player;
    [SerializeField] private RectTransform _rectTransformOfNamePlate;

    
    
    

    private void Update()
    {
        SetNamePlateAndSizeOfWalkingSpeedBar();
        SetCorrectRotationForClientLookingAtTheNamePlate();
    }
    /*
     * Sets the Size of the rectTransform Of the walkingSpeedBa and sets the name of player
     * 
     */
    private void SetNamePlateAndSizeOfWalkingSpeedBar()
    {
        _playerNameText.text = _player.PlayerName;
        _rectTransformOfNamePlate.localScale = new Vector3(_player.WalkingSpeedPercentage/100f, 1f,1f);
    }
    /*
     * From the perspective of the Main Camera (Only one main cam per player at any time)
     * Then show the name plate from the direction of the camera.
     */
    private void SetCorrectRotationForClientLookingAtTheNamePlate()
    {
        transform.LookAt(transform.position+Camera.main.transform.rotation*Vector3.forward,
                          Camera.main.transform.rotation*Vector3.up);
    }
}
