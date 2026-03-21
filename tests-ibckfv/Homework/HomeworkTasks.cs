namespace Homework;

public class HomeworkTasks
{
    static void Main()
    {
    }

    public static string LongestCommonPrefix(string[] words)
    {
        string prefix = words[0];

        for (int i = 1; i < words.Length; i++)
        {
            if (words[i].Length < 2)
            {
                throw new InvalidOperationException("Word cannot be shorter than 2 symbols");
            }

            while (words[i].IndexOf(prefix) != 0)
            {
                prefix = prefix.Substring(0, prefix.Length - 1);

                if (prefix.Length < 2)
                {
                    return "";
                }
            }
        }

        return prefix;
    }

    public static int[,] UpscaleImage(bool[,] image, int k)
    {
        int n = image.GetLength(0);
        int m = image.GetLength(1);

        if (n == m)
        {
            throw new Exception("Image cannot be square");
        }

        int newN = n * k;
        int newM = m * k;

        int[,] upscaledImage = new int[newN, newM];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                int pixelValue = image[i, j] ? 1 : 0;
                
                for (int row = i * k; row < (i + 1) * k; row++)
                {
                    for (int col = j * k; col < (j + 1) * k; col++)
                    {
                        upscaledImage[row, col] = pixelValue;
                    }
                }
            }
        }
        return upscaledImage;
    }
    
    public static bool WordPattern(string pattern, string s)
    {
        string[] words = s.Split(' ');
        
        if (pattern.Length != words.Length) 
        {
            return false;
        }

        if (pattern.Length > 0)
        {
            bool same = true;
            for (int i = 1; i < pattern.Length; i++)
            {
                if (pattern[i] != pattern[0])
                {
                    same = false;
                    break;
                }
            }
            if (same)
            {
                throw new Exception("Pattern cannot consist only one repeating character");
            }
        }

        var charToWord = new (char, string)[pattern.Length];
        var wordToChar = new (string, char)[pattern.Length];
        int count = 0;
        
        for (int i = 0; i < pattern.Length; i++)
        {
            char c = pattern[i];
            string word = words[i];
            
            bool charExists = false;
            bool wordExists = false;
            
            for (int j = 0; j < count; j++) 
            {
                if (charToWord[j].Item1 == c)
                {
                    charExists = true;
                    if (charToWord[j].Item2 != word)
                    {
                        return false;
                    }
                }
                
                if (wordToChar[j].Item1 == word)
                {
                    wordExists = true;
                    if (wordToChar[j].Item2 != c) 
                    {
                        return false;
                    }
                }
            }
            
            if (!charExists && !wordExists) 
            {
                charToWord[count] = (c, word);
                wordToChar[count] = (word, c);
                count++;
            } else if (!charExists || !wordExists) 
            {
                return false;
            }
        }
        
        return true;
    }
}