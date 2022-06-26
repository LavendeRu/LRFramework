
using UnityEngine;
using UnityEditor;

namespace Tool.AssetBundle
{

    public class BuildAssetBundle : MonoBehaviour
    {

        // 打包输出目录,通常为StreamingAssets（若不存在该目录需要新建一个）,这是Unity的一个特殊目录，打包时该目录下所有资源会被打进包体中
        //StreamingAssets与Resources的区别在于，StreamingAssets不会被压缩打进包体，而Resources会被压缩
        public static readonly string RES_OUTPUT_PATH = "Assets/StreamingAssets";

        //MenuItem会在unity菜单栏添加自定义新项
        [MenuItem("CustomEditor/Build AssetBundle")]
        private static void Build()
        {
            //打包，第一个参数是AB的输出目录，第二个参数是打包选项,第三个参数是打包的平台,IOS,Android,Win要区分开，不然AB使用的时候会有问题。
            BuildPipeline.BuildAssetBundles(RES_OUTPUT_PATH, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
        }
    }
}
