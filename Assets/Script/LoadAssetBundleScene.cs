using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using UnityEngine.SceneManagement;

public class LoadAssetBundleScene : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene()
    {
        StartCoroutine( StartAssetBundle() );
    }


    IEnumerator StartAssetBundle()
    {
        var path = Path.Combine( Application.streamingAssetsPath, "samplescene" );

        // path == "C:/work/OPIcWork/AssetBundleTest/Assets/StreamingAssets\\samplescene"

        var fileStream = new FileStream( path, FileMode.Open, FileAccess.Read );
        var bundleLoadRequest = AssetBundle.LoadFromStreamAsync( fileStream );
        yield return bundleLoadRequest;

        var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        if ( myLoadedAssetBundle == null )
        {
            Debug.Log( "Failed to load AssetBundle!" );
            yield break;
        }
        /*
        var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>( "testtext" );
        yield return assetLoadRequest;

        GameObject prefab = assetLoadRequest.asset as GameObject;
        GameObject go = Instantiate( prefab );

        go.transform.parent = this.transform;

        myLoadedAssetBundle.Unload( false );
        */

        string [] scenes = myLoadedAssetBundle.GetAllScenePaths();

        //myLoadedAssetBundle.

        string loadScenePath = null;

        foreach ( string sname in scenes )
        {
            if ( sname.Contains( "SampleScene" ) )
            {

                loadScenePath = sname;
            }
        }



        if ( loadScenePath == null )
        {
            //return;
            yield break;
        }

        LoadSceneMode loadMode;

        //if ( isAdditive ) loadMode = LoadSceneMode.Additive;
        //else loadMode = LoadSceneMode.Single;

        loadMode = LoadSceneMode.Single;
        SceneManager.LoadScene( loadScenePath, loadMode );



    
    }
}
