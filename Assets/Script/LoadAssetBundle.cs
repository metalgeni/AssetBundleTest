using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class LoadAssetBundle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine( StartAssetBundle() );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartAssetBundle()
    {
        var path = Path.Combine( Application.streamingAssetsPath, "xsprite.AssetBundle" );

        var fileStream = new FileStream( path, FileMode.Open, FileAccess.Read );
        var bundleLoadRequest = AssetBundle.LoadFromStreamAsync( fileStream );
        yield return bundleLoadRequest;

        var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        if ( myLoadedAssetBundle == null )
        {
            Debug.Log( "Failed to load AssetBundle!" );
            yield break;
        }

        var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>( "xsprite" );
        yield return assetLoadRequest;

        GameObject prefab = assetLoadRequest.asset as GameObject;
        Instantiate( prefab );

        myLoadedAssetBundle.Unload( false );
    }

}
