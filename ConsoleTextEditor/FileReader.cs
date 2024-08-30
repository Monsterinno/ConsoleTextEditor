using System;
using System.IO;
using System.Text;

class FileReader
{
    private void WriteInFile(string filePath, bool overwrite)
    {
        try 
        {
            StreamWriter sw = new StreamWriter(filePath, overwrite, Encoding.ASCII);

        }catch(Exception exp)
        {
            Console.WriteLine($"Failed to edit file contents! ({exp.Message})");
        }finally
        {

        }
    }
    private void ReadFile(string input, bool edit)
    {
        Console.WriteLine("--=============================================--");
        try
        {
            using (StreamReader sr = new StreamReader(input))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }catch(Exception exp)
        {
            Console.WriteLine($"Failed to display file content! ({exp.Message})");
        }
    }
    public void GetDirPath()
    {
        string dirPath;
        Console.WriteLine("Please provide the path of the folder with the files you wish to read/edit; ");
        dirPath = @"" + Console.ReadLine();

        if (Directory.Exists(dirPath))
        {
            Console.WriteLine("--=============================================--");
            foreach (var file in Directory.GetFiles(dirPath))
            {
                Console.WriteLine(Path.GetFileName(file));
            }
            GetFilePath(dirPath);
        }else
        {
            Console.WriteLine("Directory location does not exist.. Would you want to make one instead? (Y/N)");
            string answer = "" + Console.ReadLine();
            if (answer.ToLower() == "y" || answer.ToLower() == "yes")
            { 
                Directory.CreateDirectory(dirPath);
            }else
            {
                GetDirPath();
            }
        }
    }
    private void GetFilePath(string dirPath)
    {
        string filePath;

        Console.WriteLine("--=============================================--");
        Console.WriteLine("Please state the file you wish to read/edit; ");
        filePath = dirPath + @"\" + Console.ReadLine();

        if (File.Exists(filePath))
        {
            Console.WriteLine("Do you wish to enable editting? (Y/N)");
            string answer = "" + Console.ReadLine();
            if (answer.ToLower() == "y" || answer.ToLower() == "yes")
            {
                //TODO: make this!
                Console.WriteLine("WIP!");

                ReadFile(filePath, true);
            }else
            {
                ReadFile(filePath, false);
            }

        }else
        {
            Console.WriteLine("Given file does not exist!");
            GetFilePath(dirPath);
        }
    }
}