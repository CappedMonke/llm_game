using UnityEngine;
using TWC;

public class MapHandler : MonoBehaviour {
public TileWorldCreator twc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        var _path = Application.streamingAssetsPath + "/Map_Island_BP.json";
        twc.LoadBlueprintStackAndExecute(_path);
    }

    void BuildMap(TileWorldCreator _twc) {
        _twc.ExecuteAllBuildLayers(false);
        Debug.Log("Build");
    }

    public void OnEnable() {
        twc.OnBlueprintLayersComplete += BuildMap;
    }
    public void OnDisable() {
        twc.OnBlueprintLayersComplete -= BuildMap;
    }
}
