using UnityEngine;
using TWC;

public class MapHandler : MonoBehaviour {
public TileWorldCreator twc;
    void Start() {
        Debug.Log("Start");
        twc.LoadBlueprintStack(Application.dataPath + "/Resources/Maps/Map_Island_TW");
        Debug.Log("Loaded");
        twc.ExecuteAllBlueprintLayers();
        Debug.Log("Executed");
    }

    void BuildMap(TileWorldCreator _twc) {
        _twc.ExecuteAllBuildLayers(false);
        Debug.Log("Map Build Complete");
    }

    public void OnEnable() {
        twc.OnBlueprintLayersComplete += BuildMap;
    }
}
