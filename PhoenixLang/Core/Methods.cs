using System.Drawing;
using System.Globalization;
using System.Xml;
using Pastel;
using PhoenixLang.Core.Math;
using static PhoenixLang.Core.Parameters;
using static PhoenixLang.Core.Typing;
using static PhoenixLang.Core.Statements;

namespace PhoenixLang.Core;

public static class Methods
{
    // ReSharper disable UnusedMember.Global
    public static Dictionary<string, Action<XmlNode>> MethodsDict = new();
    
    [Method("OutputNoLine")]
    public static void OutputNoLine(XmlNode methodNode)
    {
        var textType = InterpretType(GetParameterValue(methodNode, "text_type"));
        var textRaw = GetParameterValue(methodNode, "text");
        
        if (textRaw == null)
            ParameterNullLog("text");
        
        textRaw ??= "";
            
        switch (textType)
        {
            case Type.String:
            {
                Console.Write(textRaw.Replace(@"\n", "\n"));
                break;
            }

            case Type.FString:
            {
                Console.Write(Variables.Replace(textRaw.Replace(@"\n", "\n")));
                break;
            }

            case Type.Number:
            {
                if (Defined.GetValueOrDefault("numColor") != "true")
                {
                    Console.Write(MathEngine.EvaluateDouble(textRaw));
                }
                else
                {
                    Console.Write(MathEngine.EvaluateDouble(textRaw)
                        .ToString(CultureInfo.InvariantCulture)
                        .Pastel(Color.Green)
                    );
                }
                break;
            }
            
            case Type.FNumber:
            {
                if (Defined.GetValueOrDefault("numColor") != "true")
                {
                    Console.Write(MathEngine.EvaluateDouble(
                        Variables.Replace(textRaw))
                    );
                }
                else
                {
                    Console.Write(MathEngine.EvaluateDouble(
                            Variables.Replace(textRaw))
                        .ToString(CultureInfo.InvariantCulture)
                        .Pastel(Color.Green)
                    );
                }
                break;
            }

            case Type.NotFound:
            {
                ParameterNullLog("text_type");
                break;
            }

            case Type.Unidentified:
            default:
            {
                Exception.ThrowException("Could not identify the type of the text.");
                Environment.Exit(1);
                break;
            }
            
        }
    }
    
    [Method("OutputConsole")]
    public static void OutputConsole(XmlNode methodNode)
    {
        var textType = InterpretType(GetParameterValue(methodNode, "text_type"));
        var textRaw = GetParameterValue(methodNode, "text");
        
        if (textRaw == null)
            ParameterNullLog("text");
        
        textRaw ??= "";
            
        switch (textType)
        {
            case Type.String:
            {
                Console.WriteLine(textRaw.Replace(@"\n", "\n"));
                break;
            }

            case Type.FString:
            {
                Console.WriteLine(Variables.Replace(textRaw.Replace(@"\n", "\n")));
                break;
            }

            case Type.Number:
            {
                if (Defined.GetValueOrDefault("numColor") != "true")
                {
                    Console.WriteLine(MathEngine.EvaluateDouble(textRaw));
                }
                else
                {
                    Console.WriteLine(MathEngine.EvaluateDouble(textRaw)
                        .ToString(CultureInfo.InvariantCulture)
                        .Pastel(Color.Green)
                    );
                }
                break;
            }
            
            case Type.FNumber:
            {
                if (Defined.GetValueOrDefault("numColor") != "true")
                {
                    Console.WriteLine(MathEngine.EvaluateDouble(
                        Variables.Replace(textRaw))
                    );
                }
                else
                {
                    Console.WriteLine(MathEngine.EvaluateDouble(
                            Variables.Replace(textRaw))
                        .ToString(CultureInfo.InvariantCulture)
                        .Pastel(Color.Green)
                    );
                }
                break;
            }

            case Type.NotFound:
            {
                ParameterNullLog("text_type");
                break;
            }

            case Type.Unidentified:
            default:
            {
                Exception.ThrowException("Could not identify the type of the text.");
                Environment.Exit(1);
                break;
            }
            
        }
    }
    
    [Method("ReadConsole")]
    public static void ReadConsole(XmlNode methodNode)
    {
        var output = Console.ReadLine();
        var toVar = GetParameterValue(methodNode, "to") ?? 
                    GetParameterValue(methodNode, "pipe") ?? 
                    GetParameterValue(methodNode, "pipeTo");
        output ??= "";
        
        if (toVar != null)
        {
            Variables.SetVariable(new VariableProps
            {
                Name = toVar,
                Type = Type.String,
                Value = output
            });
        }
    }
    
    [Method("OutputNewLine")]
    public static void OutputNewLine(XmlNode methodNode)
    {
        Console.WriteLine();
    }

    [Method("CreateVariable")]
    public static void CreateVariable(XmlNode methodNode)
    {
        var variableName = GetParameterValue(methodNode, "name");
        var variableType = GetParameterValue(methodNode, "value_type");
        var variableValue = GetParameterValue(methodNode, "value");
        
        if (variableName == null)
            ParameterNullLog("name");
        
        if (variableType == null)
            ParameterNullLog("value_type");
        
        if (variableValue == null)
            ParameterNullLog("value");

            
        variableName ??= "";
        variableType ??= "";
        variableValue ??= "";

        switch (InterpretType(variableType))
        {
            case Type.Number:
            {
                Variables.SetVariable(new VariableProps
                {
                    Name = variableName,
                    Type = Type.String,
                    Value = MathEngine.EvaluateDouble(variableValue).ToString(CultureInfo.InvariantCulture)
                });
                break;
            }
            case Type.FNumber:
            {
                Variables.SetVariable(new VariableProps
                {
                    Name = variableName,
                    Type = Type.String,
                    Value = MathEngine.EvaluateDouble(Variables.Replace(variableValue)).ToString(CultureInfo.InvariantCulture)
                });
                break;
            }

            case Type.String:
            {
                Variables.SetVariable(new VariableProps
                {
                    Name = variableName,
                    Type = Type.String,
                    Value = variableValue
                });
                break;
            }
            
            case Type.FString:
            {
                Variables.SetVariable(new VariableProps
                {
                    Name = variableName,
                    Type = Type.String,
                    Value = Variables.Replace(variableValue)
                });
                break;
            }
            
            case Type.NotFound:
            {
                ParameterNullLog("text_type");
                break;
            }
            
            case Type.Unidentified:
            default:
            {
                Exception.ThrowException("Could not identify the type of the variable.");
                Environment.Exit(1);
                break;
            }
        }

    }
}