using System;
using System.Linq;
using Bogus;

namespace BPSamples.GridViewExporting.Data;

public class FakeDataService
{




    private static TimeTrackingEntry[] allEntries = GenerateData();

    private static TimeTrackingEntry[] GenerateData()
    {
        var faker = new Faker();
        var names = Enumerable.Range(0, 20).Select(n => faker.Name.FullName()).ToArray();
        
        return Enumerable.Range(0, faker.Random.Int(100, 150))
            .Select(x =>
            {
                var beginDate = faker.Date.Recent(30);

                return new TimeTrackingEntry()
                {
                    BeginDate = beginDate,
                    EndDate = beginDate.AddMinutes(faker.Random.Int(5, 8 * 60)),
                    Description = faker.Lorem.Sentence(),
                    EmployeeName = faker.PickRandom(names)
                };
            })
            .ToArray();
    }

    public IQueryable<TimeTrackingEntry> GetQueryable()
    {
        return allEntries.AsQueryable();
    }
}

public class TimeTrackingEntry
{
    public DateTime BeginDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public double Hours => (EndDate - BeginDate).TotalHours;

    public string EmployeeName { get; set; }

    public string Description { get; set; }
}