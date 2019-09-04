using System;
using System.Configuration;
using System.IO;
using System.Linq;

// Usage:
//
// string exe_file, output_dir;
// bool overwrite = false;
// try {
//   exe_file = AppConfig.PathFFMPEG;
//   output_dir = AppConfig.OutputDirectory;
//   overwrite = AppConfig.OverwriteOutput;
// }
// catch (Exception ex) {
//   MessageBox.Show(ex.Message, "Error");
//   return;
// }


namespace jumbo
{
  public class AppConfig
  {
    // The app configuration, and the 'appSettings' section
    protected static Configuration config = null;
    protected static KeyValueConfigurationCollection settings = null;


    // ctor
    // Note: our errors will be in the inner exception.
    static AppConfig()
    {
      // Get the settings
      try {
        config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        settings = config.AppSettings.Settings;
      }
      catch (Exception ex) {
        string s = $"Couldn't open app configuration: {ex.Message}";
        throw new Exception(s);
      }

      // Create config file if needed
      if (File.Exists(config.FilePath)) return;

      try {
        CreateConfigFile(config.FilePath);
        config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        settings = config.AppSettings.Settings;
      }
      catch (Exception ex) {
        string s = $"Couldn't create app config file: {ex.Message}";
        throw new Exception(s);
      }
    }


    // Create a new app config file.
    // Throw exception on error.
    protected static void CreateConfigFile(string path)
    {      
      using (StreamWriter sw = File.CreateText(path)) {
        sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        sw.WriteLine("<configuration>");
        sw.WriteLine("  <startup>");
        sw.WriteLine("    <supportedRuntime version=\"v4.0\" sku=\".NETFramework,Version=v4.7.1\" />");
        sw.WriteLine("  </startup>");
        sw.WriteLine("  <appSettings>");
        sw.WriteLine("    <add key=\"FFMPEG_PATH\" value=\"\"/>");
        sw.WriteLine("    <add key=\"OUTPUT_DIRECTORY\" value=\"\"/>");
        sw.WriteLine("    <add key=\"OVERWRITE_OUTPUT\" value=\"false\"/>");
        sw.WriteLine("  </appSettings>");
        sw.WriteLine("</configuration>");
      }
    }

    
    // Path to FFMPEG exe.
    // Return empty string if setting is missing.
    // Throw exception on error.
    public static string PathFFMPEG
    {
      get {
        if (!settings.AllKeys.Contains("FFMPEG_PATH")) return string.Empty;
        return settings["FFMPEG_PATH"].Value;
      }

      set {
        if (!File.Exists(value)) {
          string s = $"File {value} doesn't exist.";
          throw new Exception(s);
        }

        try {
          settings["FFMPEG_PATH"].Value = value;
          config.Save(ConfigurationSaveMode.Modified);
          ConfigurationManager.RefreshSection("appSettings");
        }
        catch (Exception ex) {
          string s = $"Couldn't save ffmpeg path: {ex.Message}";
          throw new Exception(s);
        }
      }
    }


    // Default output directory (optional).
    // Return empty string if setting is missing.
    // Throw exception on error.
    public static string OutputDirectory
    {
      get {
        if (!settings.AllKeys.Contains("OUTPUT_DIRECTORY")) return string.Empty;
        return settings["OUTPUT_DIRECTORY"].Value;
      }

      set {
        if (value.Trim() != String.Empty && !Directory.Exists(value)) {
          string s = $"Output folder {value} doesn't exist.";
          throw new Exception(s);
        }

        try {
          settings["OUTPUT_DIRECTORY"].Value = value;
          config.Save(ConfigurationSaveMode.Modified);
          ConfigurationManager.RefreshSection("appSettings");
        }
        catch (Exception ex) {
          string s = $"Couldn't save output directory: {ex.Message}";
          throw new Exception(s);
        }
      }
    }


    // Overwrite output file?
    // Return false if setting is missing or invalid.
    // Throw exception on error.
    public static bool OverwriteOutput
    {
      get {
        if (!settings.AllKeys.Contains("OVERWRITE_OUTPUT")) return false;
        if (!bool.TryParse(settings["OVERWRITE_OUTPUT"].Value, out bool overwrite)) return false;
        return overwrite;
      }

      set {
        try {
          settings["OVERWRITE_OUTPUT"].Value = value ? "true" : "false";
          config.Save(ConfigurationSaveMode.Modified);
          ConfigurationManager.RefreshSection("appSettings");
        }
        catch (Exception ex) {
          string s = $"Couldn't save overwrite flag: {ex.Message}";
          throw new Exception(s);
        }
      }
    }


    // Valid?
    public static bool Valid
    {
      get {
        return PathFFMPEG != String.Empty;
      }
    }


  }  // class
}    // namespace

