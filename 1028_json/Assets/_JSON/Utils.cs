using UnityEngine;
using System.Collections;
using System.IO;
using System.Diagnostics;

public class Utils {

	public static string GetDataPath()
	{
		string path;
		
		switch(Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
			path = Application.persistentDataPath + "/";
			//path = Application.dataPath + "/";
			break;
			
		case RuntimePlatform.Android:
			path = Application.persistentDataPath + "/";
			//path = Application.dataPath + "/";
			break;
			
		default:
			path = "./";
			break;
		}
		
		return path;
	}
	
	public static string GetSavePath()
	{
		string path;
		
		if (Application.platform == RuntimePlatform.IPhonePlayer ||
		    Application.platform == RuntimePlatform.Android)
			path = GetDataPath();
		else
			path = GetDataPath() + "Save/";
		
		return path;
	}
	
	public static void WriteTextToFile(string text, string fileName)
	{
		#if !WEB_BUILD
		string path = GetDataPath() + fileName;

		File.WriteAllText(path, text);
		#endif 
	}
	
	
	public static string ReadTextFromFile(string fileName)
	{
		#if !WEB_BUILD
		string path = GetDataPath() + fileName;
		
		if (File.Exists(path))
		{
			FileStream fs = new FileStream (path, FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(fs);
			
			string text = sr.ReadToEnd();
			
			sr.Close();
			fs.Close();
			
			return text;
		}
		
		return null;
		#else
		return null;
		#endif 
	}
	
	public static byte[] ReadBytesFromFile(string fileName)
	{
		byte[] bytes = null;

		#if !WEB_BUILD
		string path = GetDataPath() + fileName;
		
		if (File.Exists(path))
		{
			FileStream fs = new FileStream (path, FileMode.Open, FileAccess.Read);

			bytes = new byte[fs.Length];
			fs.Read(bytes, 0, (int)fs.Length);
			
			fs.Close();
		}
		#endif 

		return bytes;
	}

	public static Process _lastProcess = null;

	private static bool isProcessRunning(Process proc) {
		if (proc == null)
			return false;
		else if (proc.HasExited)
			return false;
		
		try {
			Process.GetProcessById(proc.Id);
		} catch (System.ArgumentException) {
			return false;
		}
		
		return true;
	}

	private static void killProcess(Process proc) {
		if (isProcessRunning (proc))
			proc.Kill ();
	}

	public static void ExecuteProcess(string fileName, string arguments) {
		UnityEngine.Debug.Log ("ExecuteProcess test" + Application.platform);
		KillLastProcess ();

		if (Application.platform == RuntimePlatform.WindowsEditor ||
		    Application.platform == RuntimePlatform.WindowsPlayer) {
			try {
				Process proc = new Process();
				proc.StartInfo.FileName = fileName;
				proc.StartInfo.Arguments = arguments;
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
				//foo.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				proc.Start();
				_lastProcess = proc;

                //UnityEngine.Debug.Log(proc.MainWindowHandle);

			} catch(System.Exception e) {
				UnityEngine.Debug.Log ("ExecuteProcess failed: " + fileName + ", " + arguments);
			}
		}
	}

	public static bool IsLastProcessRunning() {
		if (_lastProcess == null)
			return false;

		bool res = isProcessRunning (_lastProcess);
		if (!res)
			_lastProcess = null;

		return res;
	}

	public static void KillLastProcess() {
		if (_lastProcess != null) {
			killProcess(_lastProcess);
			_lastProcess = null;
		}
	}

}
