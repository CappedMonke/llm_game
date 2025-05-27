using UnityEngine;
using TWC;
using TWC.Actions;
using Unity.AI.Navigation;

public class MapHandler : MonoBehaviour {
public TileWorldCreator twc;
    void Start() {
        // Comment in to load map on load
        twc.LoadBlueprintStack(Application.dataPath + "/Resources/Maps/Map_Island_TW");
        twc.ExecuteAllBlueprintLayers();
    }

    void BuildMap(TileWorldCreator _twc) {
        _twc.ExecuteAllBuildLayers(false);        
    }

    void PrepareMap(TileWorldCreator _twc) {
        GameObject map = GameObject.Find("TW_Map");
        SetGameLayerRecursive(map, 7);
        UnitSelectionManager.Instance.ActivateUnits();
        map.AddComponent<NavMeshSurface>().BuildNavMesh();

        GameController.Instance.isMapReady = true;
    }

    public void OnEnable() {
        twc.OnBlueprintLayersComplete += BuildMap;
        twc.OnBuildLayersComplete += PrepareMap;
    }

    public void OnDisable() {
        twc.OnBlueprintLayersComplete -= BuildMap;
        twc.OnBuildLayersComplete -= PrepareMap;
    }

    private void SetGameLayerRecursive(GameObject mapObject, int layer) {
        mapObject.layer = layer;
        foreach (Transform mapObjectChild in mapObject.transform) {
            mapObjectChild.gameObject.layer = layer;

            if (mapObjectChild.GetComponentInChildren<Transform>() != null) {
                SetGameLayerRecursive(mapObjectChild.gameObject, layer);
            }
        }
    }
}
