using Microsoft.VisualBasic.FileIO;
using System.Text;

var filePath = @"C:\Users\vitom\OneDrive\Pulpit\input.csv";
int columnNumber;
string[] columnNames;
List<string[]> rows = new();

using (TextFieldParser csvParser = new(filePath))
{
    // Initializing csvParser
    csvParser.CommentTokens = new string[] { "#" };
    csvParser.SetDelimiters(new string[] { "," });
    csvParser.HasFieldsEnclosedInQuotes = false;

    // Scan the first row for column names; set column number based on lenght of the first row.
    columnNames = csvParser.ReadFields();
    columnNumber = columnNames.Length;

    StringBuilder stringBuilder = new();
    StreamWriter streamWriter = new(@"C:\Users\vitom\OneDrive\Pulpit\output.html");

    // Begin writing of table into string
    stringBuilder.Append("<center><table border=\"1\">");

    // Write row with column names into string
    stringBuilder.Append("<tr>");
    for (int i = 0; i < columnNames.Length; i++)
    {
        stringBuilder.Append($"<th>{columnNames[i]}</th>");
    }
    stringBuilder.Append("</tr>");

    // Loop for reading fields row by row
    int rowIndex = 0;
    while (!csvParser.EndOfData)
    {
        rows.Add(csvParser.ReadFields());
        if(rowIndex%2==0)
            stringBuilder.Append("<tr bgcolor= #D3D3D3>");
        else
            stringBuilder.Append("<tr>");
        // Loop for appending rows of table into string
        for (int i = 0; i < columnNumber; i++)
        {
            stringBuilder.Append($"<td>{rows[rowIndex][i]}</td>");
        }
        stringBuilder.Append("</tr>");
        rowIndex++;
    }
    // End of writing table into string; disposing of the writer object
    stringBuilder.Append("</table></center>");
    streamWriter.Write(stringBuilder);
    streamWriter.Flush();
    streamWriter.Close();
    streamWriter.Dispose();
}