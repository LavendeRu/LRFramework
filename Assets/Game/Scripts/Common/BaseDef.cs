/// <summary>
/// 基础定义文件
/// </summary>
using UnityEngine;

public class BaseDef
{
    public const string MUSIC_SUFFIX = "ogg";                   // 音乐后缀
    public const string SOUND_SUFFIX = "ogg";                   // 音乐后缀
    public const string FONT_SUFFIX = "ttf";                    // 字体后缀
}

public class ABFileInfo
{
    public string filename;
    public string md5;
    public int rawSize;         // 压缩前的文件大小
    public int compressSize;		// 压缩后的文件大小
}

public class GameNewVersionInfo
{
    public int appID;
    public string version;
    public string ip;
    public int port;
}
