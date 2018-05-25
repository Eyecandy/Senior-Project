using UnityEngine;

public class Util : MonoBehaviour {

    //Set Layer of Gameobject and all of it's children
    public static void SetLayerRecursively(GameObject _obj, int _newLayer)
    {
        if (_obj == null)
            return;
		
        _obj.layer = _newLayer;
        foreach (Transform _child in _obj.transform )
        {
            if (_child == null)
                continue;
			
            SetLayerRecursively(_child.gameObject, _newLayer);
        }
    }
}
