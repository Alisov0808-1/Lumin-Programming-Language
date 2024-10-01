using System.Text.RegularExpressions;

namespace Lumin
{
    static public class Parser
    {
        static bool HasS(string input)
        {
            bool inSingleQuote = false; 
            bool inDoubleQuote = false; 

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

               
                if (c == '"')
                {
                    if (!inSingleQuote) 
                    {
                        inDoubleQuote = !inDoubleQuote;
                    }
                }
                
                else if (c == '\'')
                {
                    if (!inDoubleQuote)
                    {
                        inSingleQuote = !inSingleQuote;
                    }
                }
               
                else if (!inSingleQuote && !inDoubleQuote)
                {
                   
                    if (c == '*' ||
                        (c == '+' && i < input.Length - 1 && input[i + 1] == '=') ||
                        (c == '=' && i > 0 && input[i - 1] == '+') ||
                        (c == '-' && i < input.Length - 1 && input[i + 1] == '=') ||
                        (c == '/'))
                    {
                        return true; 
                    }
                }
            }

            return false;
        }
        static public bool Pars(string command, string file, List<string> func)
        {
            bool tes = false;
            string pattern = @"\d+";
        //z  if (HasS(command))
           // {
                Console.WriteLine(command);
                
                if (command.Contains("=") &&
              !command.Contains('-') &&

              !command.Contains(">=") &&
              !command.Contains("+=") &&
              !command.Contains("-=") &&
              !command.Contains("<=") &&
              !command.Contains('!') &&
              !command.Contains('*'))
                {
                    string[] a = command.TrimStart().Split("=", 2);
                    bool containsNumbers = Regex.IsMatch(a[1], pattern); bool sas = false;
                    for (int ia = 0; ia < func.Count; ia++)
                    {
                        string com = command.Replace(" ", null);
                        if (a[1].TrimStart().StartsWith(func[ia]))
                        {
                            sas = true;
                            File.AppendAllText(file, "\n" + $"call {a[1]}\n\nmov dword [{a[0]}],eax"); break;
                        }
                    }
                    if (char.IsLetter(a[1].Trim()[0]) && a[1].TrimStart().Contains("'") == false)
                    {
                        if (sas == false)
                        {
                            File.AppendAllText(file, "\n" + $"\nmov [{a[0]}],[{a[1]}]");
                        }
                    }
                    else { File.AppendAllText(file, "\n" + $"\nmov [{a[0]}],{a[1]}"); }
                    return true;




                }
            
                else if (command.Contains("+="))
                {

                    string[] a = command.TrimStart().Split("+=", 2);
                    bool containsNumbers = Regex.IsMatch(a[1], pattern);
                    bool sas = false;
                    for (int ia = 0; ia < func.Count; ia++)
                    {
                        string com = command.Replace(" ", null);
                        if (a[1].TrimStart().StartsWith(func[ia]))
                        {
                            sas = true;
                            File.AppendAllText(file, "\n" + $"call {a[1]}\nadd dword [{a[0]}],eax");
                            break;
                        }
                    }
                    if (char.IsLetter(a[1].Trim()[0]) && a[1].TrimStart().Contains("'") == false)
                    {
                        if (sas == false)
                        {
                            File.AppendAllText(file, "\n" + $"add [{a[0]}],[{a[1]}]");
                        }
                    }
                    else { File.AppendAllText(file, "\n" + $"add [{a[0]}],{a[1]}"); }
                    return true;
                }

                else if (command.Contains("-="))
                {
                    string[] a = command.TrimStart().Split("-=", 2);
                    bool containsNumbers = Regex.IsMatch(a[1], pattern); bool sas = false;
                    for (int ia = 0; ia < func.Count; ia++)
                    {
                        string com = command.Replace(" ", null);
                        if (a[1].TrimStart().StartsWith(func[ia]))
                        {
                            sas = true;
                            File.AppendAllText(file, "\n" + $"call {a[1]}\nsub dword  [{a[0]}],eax"); break;
                        }
                    }
                    if (char.IsLetter(a[1].Trim()[0]) && a[1].TrimStart().Contains("'") == false)
                    {
                        if (sas == false)
                        {
                            File.AppendAllText(file, "\n" + $"sub [{a[0]}],[{a[1]}]");
                        }
                    }
                    else { File.AppendAllText(file, "\n" + $"sub [{a[0]}],{a[1]}"); }
                    return true;
                }
                else if (command.Contains('*') && !command.Contains('-') && !command.Contains('!') && !command.Contains('=') && !command.Contains('/') && !command.Contains('+'))
                {
                    string[] a = command.TrimStart().Split('*');
                    bool containsNumbers = Regex.IsMatch(a[1], pattern);
                    bool sas = false;
                    for (int ia = 0; ia < func.Count; ia++)
                    {
                        string com = command.Replace(" ", null);
                        if (a[1].TrimStart().StartsWith(func[ia]))
                        {
                            sas = true;
                            File.AppendAllText(file, "\n" + $" call {a[1]}\n  imul eax,eax,[{a[0]}]\r\n mov [{a[0]}], eax");
                            break;
                        }
                    }
                    if (char.IsLetter(a[1].Trim()[0]) && a[1].TrimStart().Contains("'") == false)
                    {
                        if (sas == false)
                        {
                            File.AppendAllText(file, "\n" + $" \n mov eax,dword [{a[0]}] \r\n    imul eax,eax,[{a[1]}]\r\n    mov [{a[0]}], eax \n ");
                        }
                    }
                    else { File.AppendAllText(file, "\n" + $" \n  mov eax,dword [{a[0]}] \r\n    imul eax,eax,{a[1]}\r\n    mov [{a[0]}], eax \n"); }
                    return true;
                }
                else if (command.Contains('/') && !command.TrimStart().StartsWith("import"))
                {
                    string[] parts = command.TrimStart().Split(',');
                    string[] operands = parts[1].Split('/');
                    bool containsNumbers = Regex.IsMatch(operands[1], pattern);
                    bool sas = false;
                    for (int ia = 0; ia < func.Count; ia++)
                    {
                        string com = command.Replace(" ", null);
                        if (operands[0].TrimStart().StartsWith(func[ia]))
                        {
                            sas = true;
                            File.AppendAllText(file, "\n" + $"call {operands[1]}\n \r\n    xor edx, edx\r\n    mov ebx,eax\r\n    div ebx \r\n    mov dword  [{parts[0]}], eax \r\n    mov [{operands[0]}],edx");
                            break;
                        }
                    }
                    if (char.IsLetter(operands[1].Trim()[0]) && operands[1].TrimStart().Contains("'") == false)
                    {
                        if (sas == false)
                        {
                            File.AppendAllText(file, "\n" + $"mov eax, [{operands[0]}] \r\n    xor edx, edx\r\n    mov ebx, [{operands[1]}]\r\n    div ebx \r\n    mov dword  [{parts[0]}], eax \r\n    mov [{operands[0]}], edx\n ");
                        }
                    }
                    else
                    {
                        File.AppendAllText(file, "\n" + $" mov eax, [{operands[0]}] \r\n    xor edx, edx\r\n    mov ebx, {operands[1]}\r\n    div ebx \r\n    mov dword  [{parts[0]}], eax \r\n    mov [{operands[0]}], edx");
                    }
                    return true;
                }

                return default;
            //}
            //else { return false; }
        }
    }
}
