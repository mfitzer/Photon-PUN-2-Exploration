using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public static class ScriptGenerator
{
    /// <summary>
    /// Generates a script from the given template file using the given placeholder values.
    /// </summary>
    /// <param name="templatePath">Path to the template file in the Assets folder.
    /// Example: "Scripts/Templates/MyTemplate.txt"</param>
    /// <param name="scriptFileName">Name of the generated script file. Example: MyTemplate.cs</param>
    /// <param name="placeholderValues">The values to replace the given placeholders with in the template file.</param>
    /// <param name="generatedScriptDirectory">Path to the directory in the Assets folder to generate the script.
    /// Example: "Scripts/Generated"</param>
    public static void generateScriptFromTemplate(string templatePath, string scriptFileName,
        Dictionary<string, string> placeholderValues, string generatedScriptDirectory)
    {
        //Read template code
        StreamReader reader = new StreamReader("Assets/" + templatePath);
        string templateCode = reader.ReadToEnd();
        reader.Close();

        string generatedCode = templateCode;

        //Replace placeholders with their values
        foreach (KeyValuePair<string, string> placeholderValue in placeholderValues)
        {
            generatedCode = generatedCode.Replace(placeholderValue.Key, placeholderValue.Value);
        }

        string fullDirectoryPath = Application.dataPath + "/" + generatedScriptDirectory;
        string scriptFilePath = Path.Combine(fullDirectoryPath, scriptFileName);

        //Create the root path directory if it doesn't exist        
        if (!Directory.Exists(fullDirectoryPath))
            Directory.CreateDirectory(fullDirectoryPath);

        //Create the file if it doesn't already exist
        if (!File.Exists(scriptFilePath))
            File.Create(scriptFilePath).Close();

        //Write generated code to the file
        StreamWriter writer = new StreamWriter(scriptFilePath);
        writer.Write(generatedCode);
        writer.Close();

        //Force project folder to refresh and compile scripts
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// Generates a script from the given template file using the given placeholder values.
    /// </summary>
    /// <param name="scriptPath">Path to the generated script file in the Assets folder.
    /// Example: "Scripts/MyGeneratedScript.cs"</param>
    /// <param name="generatedContentBegin">The placeholder marking the beginning of generated content.</param>
    /// <param name="generatedContentEnd">The placeholder marking the ending of generated content.</param>
    /// <param name="updatedContents">The contents to replace old generated contents with.</param>
    public static void updateGeneratedScript(string scriptPath, string generatedContentBegin,
        string generatedContentEnd, string updatedContents)
    {
        //Read script code
        StreamReader reader = new StreamReader("Assets/" + scriptPath);
        string scriptCode = reader.ReadToEnd();
        reader.Close();

        string updatedCode = scriptCode;

        int contentBeginIndex = scriptCode.IndexOf(generatedContentBegin) + generatedContentBegin.Length;
        int contentEndIndex = scriptCode.IndexOf(generatedContentEnd);

        //Remove old generated content
        updatedCode = updatedCode.Remove(contentBeginIndex, contentEndIndex - contentBeginIndex);

        //Insert new generated content
        updatedCode = updatedCode.Insert(contentBeginIndex, "\n" + updatedContents + "\n");

        string scriptFullFilePath = Application.dataPath + "/" + scriptPath;

        //Write updated code to the file
        StreamWriter writer = new StreamWriter(scriptFullFilePath);
        writer.Write(updatedCode);
        writer.Close();

        //Force project folder to refresh and compile scripts
        AssetDatabase.Refresh();
    }
}
