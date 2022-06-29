using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace PlanningPoker.Services.ReadFile.Mapping;

public class DescriptionConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        //var formatted = Regex.Replace(text, @"[\n]{2,}", "\n");
        
        return text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        throw new NotImplementedException("This converter does not support converting Description to string");
    }
}