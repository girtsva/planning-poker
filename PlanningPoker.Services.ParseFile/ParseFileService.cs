using System.ComponentModel.DataAnnotations;
using System.Globalization;
using CsvHelper;
using PlanningPoker.ApiModels.Response;
using PlanningPoker.Services.Interfaces;
using PlanningPoker.Services.ReadFile.Mapping;
using PlanningPoker.Validation;

namespace PlanningPoker.Services.ReadFile;

public class ParseFileService : IParseFileService
{
    private readonly IServiceProvider _provider;
    private const string FilePathTxt = @"./test.txt";
    private const string FilePath = @"./test.csv";
    //string fileName = Path.GetFileName(FilePath);
    
    // separator = Path.DirectorySeparatorChar;
    // path = $"{separator}users{separator}user1{separator}";
    
    // string[] paths = {@"d:\archives", "2001", "media", "images"};
    // string fullPath = Path.Combine(paths);

    public ParseFileService(IServiceProvider provider)
    {
        _provider = provider;
    }

    public int TestTxtFileRead()
    {
        var lines = new List<string>(File.ReadAllLines(FilePathTxt));
            
        Console.WriteLine($"Lines = {lines.Count}");

        return lines.Count;
    }

    public List<JiraIssueResponse> ParseIssuesLocalFile()
    {
        using var reader = new StreamReader(FilePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<JiraIssueResponseMapper>();
        var records = csv.GetRecords<JiraIssueResponse>();
        return records.ToList();
    }
    
    public List<JiraIssueResponse> ParseIssues(MemoryStream stream)
    {
        using var reader = new StreamReader(stream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<JiraIssueResponseMapper>();
        var records = csv.GetRecords<JiraIssueResponse>(); //.ToList();

        // foreach (var record in records)
        // {
        //     var context = new ValidationContext(record, serviceProvider: _provider, items: null);
        //     var validationResults = new List<ValidationResult>();
        //     
        //     bool isValid = Validator.TryValidateObject(record, context, validationResults, true);
        // }
        
        return records.ToList();
    }
}