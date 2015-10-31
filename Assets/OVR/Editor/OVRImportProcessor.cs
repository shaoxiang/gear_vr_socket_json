using UnityEngine;
using UnityEditor;

public class OVRImportProcessor : AssetPostprocessor 
{
	static void OnPostprocessAllAssets ( string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths ) 
	{
#if false //UNITY_5_0
		foreach ( string asset in importedAssets )
		{
			if ( asset.ToLower() == "assets/plugins/x86/oculusplugin.dll" || asset.ToLower() == "assets/plugins/x86_64/oculusplugin.dll" ) 
			{
				Debug.Log( "[OVRImportProcessor] Updating plugin compatibility: "  + asset );
				PluginImporter pluginImporter =  PluginImporter.GetAtPath( asset ) as PluginImporter;
				if ( pluginImporter != null ) 
				{
					bool x86_64 = asset.Contains( "x86_64" ); 
					pluginImporter.SetCompatibleWithEditor( true );
					pluginImporter.SetCompatibleWithAnyPlatform( false );
					pluginImporter.SetEditorData( "OS", "Windows" );
					if ( x86_64 ) 
					{
						pluginImporter.SetCompatibleWithPlatform( BuildTarget.StandaloneWindows, false );
						pluginImporter.SetCompatibleWithPlatform( BuildTarget.StandaloneWindows64, true );
					} 
					else 
					{
						pluginImporter.SetCompatibleWithPlatform( BuildTarget.StandaloneWindows64, false );
						pluginImporter.SetCompatibleWithPlatform( BuildTarget.StandaloneWindows, true );
					}
				}
				AssetDatabase.WriteImportSettingsIfDirty( asset );
			}
		}
#endif
	}
}