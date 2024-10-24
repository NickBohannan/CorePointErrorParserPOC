using System.Text;
using System.Text.RegularExpressions;

string errorPattern = @"ERROR: (.*)";

string filePath = "mainframe_ebcdic_file.dat";
string outputFilePath = "ParsedErrors.txt";

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

Encoding ebcdicEncoding = Encoding.GetEncoding("IBM037");

var errorMessages = new List<string>();

try
{
    string[] fileLines = File.ReadAllLines(filePath, ebcdicEncoding);

    foreach (var line in fileLines)
    {
        // Check if the line matches the error pattern
        Match match = Regex.Match(line, errorPattern);
        if (match.Success)
        {
            Console.WriteLine("match found...");
            // Add the captured error message to the list
            errorMessages.Add(match.Groups[1].Value);
        }
    }

    // Write errors to the output file
    File.WriteAllLines(outputFilePath, errorMessages);

    Console.WriteLine($"Found {errorMessages.Count} error(s). Errors saved to {outputFilePath}");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
