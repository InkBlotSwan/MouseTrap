using System;
using System.Xml.Serialization;
using System.IO;
class SettingsClass
{
    //Settings Variabe, used as an instance of the trapsettings class.
    public TrapSettings _settings = new TrapSettings();

    /// <summary>
    /// Updates the Variables in the settings file.
    /// </summary>
    /// <param name="X1"></param>
    /// <param name="Y1"></param>
    /// <param name="X2"></param>
    /// <param name="Y2"></param>
    public void Update(int X1, int Y1, int X2, int Y2, string[] oldList)
    {
        _settings.topLeftX = X1;
        _settings.topLeftY = Y1;
        _settings.bottomRightX = X2;
        _settings.bottomRightY = Y2;
        _settings.listOfPrograms = oldList;
    }
    public void ListUpdate(string programToAdd)
    {
        Array.Resize(ref _settings.listOfPrograms, _settings.listOfPrograms.Length + 1);
        _settings.listOfPrograms[_settings.listOfPrograms.Length - 1] = programToAdd;
    }

    /// <summary>
    /// Saves the current _settings file to the xml file.
    /// </summary>
    public void Save()
    {
        XmlSerializer set_serialized = new XmlSerializer(typeof(TrapSettings));

        

        using (StreamWriter xmlfile = new StreamWriter("TrapWindow.xml"))
        {
            set_serialized.Serialize(xmlfile, _settings);
            xmlfile.Dispose(); //TODO! try and close the xml file properly.
        }
    }

    /// <summary>
    /// Checks that the file trying to be loaded exists.
    /// </summary>
    public bool exists()
    {
        TrapSettings _dummy;
        bool exists = false;
        try
        {
            XmlSerializer set_serialized = new XmlSerializer(typeof(TrapSettings));
            StreamReader xml_settings_file = new StreamReader("TrapWindow.xml");
            _dummy = (TrapSettings)set_serialized.Deserialize(xml_settings_file);
            xml_settings_file.Close();
            exists = true;
            return exists;
        }
        catch (FileNotFoundException)
        {
            return exists;
        }
    }

    /// <summary>
    /// Loads the settings file from the xml in program directory.
    /// </summary>
    public void load()
    {
        XmlSerializer set_serialized = new XmlSerializer(typeof(TrapSettings));
        StreamReader xml_settings_file = new StreamReader("TrapWindow.xml");

        //Set variable to old saved settings file.
        _settings = (TrapSettings)set_serialized.Deserialize(xml_settings_file);
        xml_settings_file.Close();
    }
}

/// <summary>
/// Class object to be serialised, containing the settings
/// for the mouse trap to be laid down. This is for object serialisation method.
/// </summary>
[Serializable]
public class TrapSettings
{
    public int topLeftX;
    public int topLeftY;

    public int bottomRightX;
    public int bottomRightY;

    //Array Variable for the list.
    public string[] listOfPrograms;
    public TrapSettings()
    {
        topLeftX = 0;
        topLeftY = 0;
        bottomRightX = 0;
        bottomRightY = 0;
        listOfPrograms = new string[1];
    }

    public TrapSettings(int X1, int Y1, int X2, int Y2)
    {
        topLeftX = X1;
        topLeftY = Y1;
        bottomRightX = X2;
        bottomRightY = Y2;
    }
}

