using System;
using System.Linq;
using Bogus;

namespace BPSamples.GridViewExporting.Data;

public class FakeDataService
{

    private readonly AppDbContext _context;
    public FakeDataService()
    {
        _context = new AppDbContext();

        if(_context.TimeTrackingEntries.Count() == 0)
        {
            GenerateData();
        }
    }


    private void GenerateData()
    {
        
        var faker = new Faker();
        var names = Enumerable.Range(0, 20).Select(n => faker.Name.FullName()).ToArray();
        
        var entries =  Enumerable.Range(1, 1000)
            .Select(x =>
            {
                var beginDate = faker.Date.Recent(30);

                return new TimeTrackingEntry()
                {
                    Id = x,
                    BeginDate = beginDate,
                    EndDate = beginDate.AddMinutes(faker.Random.Int(5, 8 * 60)),
                    Description = faker.Lorem.Sentence(),
                    EmployeeName = faker.PickRandom(names)
                };
            })
            .ToList();
        foreach (var entry in entries)
        {
            _context.TimeTrackingEntries.Add(entry);
        }
        _context.SaveChanges();
    }

    public IQueryable<TimeTrackingEntry> GetQueryable()
    {
        return _context.TimeTrackingEntries.AsQueryable();
    }
}