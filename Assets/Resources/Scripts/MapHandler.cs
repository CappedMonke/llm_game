using UnityEngine;
using TWC;

public class MapHandler : MonoBehaviour {
public TileWorldCreator twc;
    void Start() {
        //
        //twc.LoadBlueprintStack(Application.dataPath + "/Resources/Maps/Map_Island_TW");
        //twc.ExecuteAllBlueprintLayers();
    }

    void BuildMap(TileWorldCreator _twc) {
        _twc.ExecuteAllBuildLayers(false);
    }

    public void OnEnable() {
        twc.OnBlueprintLayersComplete += BuildMap;
    }
}
