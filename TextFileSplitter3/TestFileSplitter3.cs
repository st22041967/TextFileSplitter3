using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextFileSplitter3
{
    class TestFileSplitter3
    {
        static void Main(string[] args)
        {
            SplitFile(@"C:\Users\Stuart\Downloads\TEST1.TXT", @"C:\Users\Stuart\Downloads\", "OUT.txt", 22, "use rates; " + Environment.NewLine);
            //SplitFile(@"H:\WORK\(01) IN PROGRESS\WMBS-444 HNW - Latest Postcodes\Insert_FireInsight and PostcodeJBASubs_IN.sql", @"H:\WORK\(01) IN PROGRESS\WMBS-444 HNW - Latest Postcodes\", "Insert_FireInsight and PostcodeJBASubs_OUT.sql", 60000, "use LandscapeDataCollector;" + Environment.NewLine);
            Console.ReadKey();
        }

        private static void SplitFile(string fileToSplit, string outputPath, string outputFile, int splitSize, string header)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fileToSplit))
                {
                    int fileCounter = 0;
                    int lineCounter = 0;
                    string newOutFile = "";
                    StringBuilder sb = new StringBuilder();
                    sb.Clear();
                    // Add Header to each outfile?
                    if (header.Length > 0) sb.Append(header);
                    // Main loop
                    Console.WriteLine("STARTED");
                    while (sr.Peek() >= 0)
                    {
                        var fileLine = sr.ReadLine();
                        lineCounter++;
                        sb.Append(fileLine + Environment.NewLine);
                        // Write splitSize number of records
                        if (lineCounter == splitSize)
                        {
                            fileCounter++;
                            newOutFile = string.Format(outputPath + fileCounter.ToString() + "_" + outputFile);
                            File.WriteAllText(newOutFile, sb.ToString());
                            Console.WriteLine(string.Format(newOutFile + "Created"));
                            lineCounter = 0;
                            sb.Clear();
                            // Add Header to each outfile?
                            if (header.Length > 0) sb.Append(header);
                        }
                    }
                    // Write any remaining records
                    if (sb.Length > 0)
                    {
                        fileCounter++;
                        newOutFile = string.Format(outputPath + fileCounter.ToString() + "_" + outputFile);
                        File.WriteAllText(newOutFile, sb.ToString());
                        Console.WriteLine(string.Format(newOutFile + "Created"));
                    }
                    Console.WriteLine("COMPLETE");
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine(string.Format("The file (" + fileToSplit + ") could not be read:"));
                Console.WriteLine(e.Message);
            }
        }
    }
}
