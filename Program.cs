using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace WordCount
{
    public class Program
    {
        //返回文件字符数
        public static int CodeCount(string FileName)
        {
            if (!File.Exists(@"..\Debug\" + FileName))
            {
                Console.WriteLine("文件不存在");
                return 0;
            }

            int count = 0;
            char[] Code = { ' ', ',', '%', '~', '!', '#', '^', '*', '(', ')', '\t' };

            //读取文件
            string[] s = File.ReadAllLines(@"..\Debug\" + FileName, Encoding.Default);
            foreach (string line in s)     //行数
            {
                foreach (Char a in line)   //每行字符数
                {
                    foreach (char b in Code)
                    {
                        if (a == b)
                            count++;
                    }
                }

            }

            return count;
        }

        //返回文件单词数
        public static int WordCount(string FileName)
        {
            if (!File.Exists(@"..\Debug\" + FileName))
            {
                Console.WriteLine("文件不存在");
                return 0;
            }

            int sum = 0;

            //读取文件
            string[] s = File.ReadAllLines(@"..\Debug\" + FileName, Encoding.Default);
            foreach (string line in s)
            {
                if (line != null)
                {
                    foreach (Char a in line)
                    {
                        if (a == ' ' || a == ',')
                            sum++;
                    }
                    sum++;
                }
            }
            return sum;
        }

        //返回文件总行数
        public static int LineCount(string FileName)
        {
            if (!File.Exists(@"..\Debug\" + FileName))
            {
                Console.WriteLine("文件不存在");
                return 0;
            }

            //读取文件
            string[] s = File.ReadAllLines(@"..\Debug\" + FileName, Encoding.Default);

            return s.Length;

        }

        //将结果输出到指定文件
        public static void OutPut(string ParmeterResult, string FileName)
        {
            if (!File.Exists(@"..\Debug\" + FileName))
            {
                //如果文件不存在，则先新建文件
                FileStream fs = new FileStream(@"..\Debug\" + FileName, FileMode.Create, FileAccess.Write);
                string[] parResult = { ParmeterResult };
                StreamWriter sw = new StreamWriter(fs);
                foreach (string str in parResult)
                {
                    sw.WriteLine(str);
                }
                sw.Close();
            }
            else
            {
                FileStream fs = new FileStream(@"..\Debug\" + FileName, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(ParmeterResult);
                sw.Close();
                fs.Close();
            }

        }

        static void Main(string[] args)
        {
            string FileName;      //文件名称
            string UserWrite;     //用户输入内容
            string[] ParName = new string[5];       //操作名称
            int[] ParmeterResult = new int[5];   //文件操作的返回结果

            UserWrite = Console.ReadLine();

            string[] AllFileName = new string[50]; //存储所有输入过的文件名
            string[] AllParName = new string[50];  //存储所有操作名
            int i = 0;

            while (UserWrite != "###")
            {
                string[] Parameter = UserWrite.Split(' ');   //文件执行操作类型

                FileName = Parameter[Parameter.Length - 1];
                AllFileName[i] = FileName;

                for (int j = 1; j < Parameter.Length - 1; j++)
                {

                    if (Parameter[j] == "-c")       //得到字符总数
                    {
                        ParmeterResult[j] = CodeCount(FileName);
                        ParName[j] = "字符数";
                    }
                    else if (Parameter[j] == "-w")     //得到单词总数
                    {
                        ParmeterResult[j] = WordCount(FileName);
                        ParName[j] = "单词数";
                    }
                    else if (Parameter[j] == "-l")     //得到总行数
                    {
                        ParmeterResult[j] = LineCount(FileName);
                        ParName[j] = "行数";
                    }
                    else if (Parameter[j] == "-o")     //将结果写入文件
                    {
                        for (int k = 1; k <= Parameter.Length - 1; k++)
                        {
                            OutPut(AllFileName[i - 1] + "," + ParName[k] + ":" + ParmeterResult[k], AllFileName[i]);
                            Console.WriteLine(AllFileName[i - 1] + "," + ParName[k] + ":" + ParmeterResult[k]);
                        }
                        continue;
                    }
                    Console.WriteLine(FileName + "," + ParName[j] + ":" + ParmeterResult[j]);
                }
                UserWrite = Console.ReadLine();
                i++;
            }

        }
    }
}