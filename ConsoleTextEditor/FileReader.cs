using System;
using System.IO;
using System.Text;

class FileReader
{
    private void WriteInFile(string filePath, bool overwrite)
    {
        Console.WriteLine("Now editing file..");
        Console.WriteLine("Anything you type will be saved in the specified file. Type [exit editmode] to exist editing mode.");
        try 
        {
            StreamWriter sw = new StreamWriter(filePath, overwrite, Encoding.ASCII);

            string line;
            while ((line = Console.ReadLine()) != "exit editmode")
            {
                sw.WriteLine(line);
            }

            Console.WriteLine("Closing editing mode...");
            sw.Close();

        }catch(Exception exp)
        {
            Console.WriteLine($"Failed to edit file contents! ({exp.Message})");
        }finally
        {
            ReadFile(filePath);
        }
    }
    private void ReadFile(string input)
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
    private void GetFilePath(string dirPath)
    {
        string filePath;

        Console.WriteLine("--=============================================--");
        Console.WriteLine("Please state the file you wish to read/edit/add; ");
        filePath = dirPath + @"\" + Console.ReadLine();

        if (File.Exists(filePath))
        {
            ReadFile(filePath);
            Console.WriteLine("--=============================================--");
            Console.WriteLine("Do you wish to enable editting? (Y/N)");
            string answer = "" + Console.ReadLine();
            if (answer.ToLower() == "y" || answer.ToLower() == "yes")
            {
                Console.WriteLine("Do you want to overwrite the current content in the file? (Y/N)");
                answer = "" + Console.ReadLine();
                if (answer.ToLower() == "y" || answer.ToLower() == "yes")
                {
                    WriteInFile(filePath, false);
                }else
                {
                    WriteInFile(filePath, true);
                }
                
            }else
            {
                ReadFile(filePath);
            }

        }else
        {
            Console.WriteLine("Given file does not exist.. Would you want to make one instead? (Y/N)");
            string answer = "" + Console.ReadLine();
            if (answer.ToLower() == "y" || answer.ToLower() == "yes")
            {
                File.Create(filePath).Close();
                Console.WriteLine("Created new file for path; " + filePath);
                ReadFile(filePath);
                Console.WriteLine("Do you wish to enable editting? (Y/N)");
                answer = "" + Console.ReadLine();
                if (answer.ToLower() == "y" || answer.ToLower() == "yes")
                {
                    Console.WriteLine("Do you want to overwrite the current content in the file? (Y/N)");
                    answer = "" + Console.ReadLine();
                    if (answer.ToLower() == "y" || answer.ToLower() == "yes")
                    {
                        WriteInFile(filePath, false);
                    }else
                    {
                        WriteInFile(filePath, true);
                    }
                    
                }else
                {
                    ReadFile(filePath);
                }
            }
            GetFilePath(dirPath);
        }
    }
    private void GetFileNamesFromDirectory(string dirPath)
    {
        Console.WriteLine("--=============================================--");
        Console.WriteLine("Contents; \n");
        Console.WriteLine("-------------------");
        if (Directory.GetFiles(dirPath).Length > 0)
        {
            foreach (var file in Directory.GetFiles(dirPath))
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }else
        {
            Console.WriteLine("<Empty>");
        }
    }
    public void GetDirPath()
    {
        string dirPath;
        Console.WriteLine("Please provide the path of the folder with the files you wish to read/edit/add; ");
        dirPath = @"" + Console.ReadLine();

        if (Directory.Exists(dirPath))
        {
            GetFileNamesFromDirectory(dirPath);
            GetFilePath(dirPath);
        }else
        {
            Console.WriteLine("Directory location does not exist.. Would you want to make one instead? (Y/N)");
            string answer = "" + Console.ReadLine();
            if (answer.ToLower() == "y" || answer.ToLower() == "yes")
            { 
                Directory.CreateDirectory(dirPath);
                Console.WriteLine("Created new directory for path; " + dirPath);
                GetFileNamesFromDirectory(dirPath);
                GetFilePath(dirPath);
            }else
            {
                GetDirPath();
            }
        }
    }
}