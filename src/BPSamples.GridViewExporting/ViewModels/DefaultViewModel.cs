using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BPSamples.GridViewExporting.Data;
using ClosedXML.Excel;
using DotVVM.BusinessPack.Controls;
using DotVVM.BusinessPack.Export.Excel;
using DotVVM.Framework.ViewModel;

namespace BPSamples.GridViewExporting.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {
        private readonly FakeDataService fakeDataService;

        public BusinessPackDataSet<TimeTrackingEntry> Entries { get; set; } = new()
        {
            SortingOptions =
            {
                SortDescending = true,
                SortExpression = nameof(TimeTrackingEntry.BeginDate)
            },
            PagingOptions =
            {
                PageSize = 15
            }
        };


        public DefaultViewModel(FakeDataService fakeDataService)
        {
            this.fakeDataService = fakeDataService;
        }

        public override Task PreRender()
        {
            if (Entries.IsRefreshRequired)
            {
                Entries.LoadFromQueryable(fakeDataService.GetQueryable());
            }

            return base.PreRender();
        }

        public async Task Export()
        {
            // get grid view
            var gridView = (GridView)Context.View.FindControlByClientId("grid");
            
            // preare data set
            var dataSet = new BusinessPackDataSet<TimeTrackingEntry>();
            dataSet.SortingOptions = Entries.SortingOptions;
            dataSet.LoadFromQueryable(fakeDataService.GetQueryable());

            // configure export
            var settings = new GridViewExportExcelSettings<TimeTrackingEntry>()
                .WithWorksheetName("Time Tracking")
                .WithStartAddress(3, 1)
                .WithAdjustToContents()
                .ForColumn("Date",
                    c => c.WithValueTransform<DateTime>(v => v.Date))
                .ForColumn("BeginTime",
                    c => c.WithValueTransform<DateTime>(v => v.TimeOfDay))
                .ForColumn(c => c.EndDate,
                    c => c.WithValueTransform<DateTime>(v => v.TimeOfDay))
                .WithColumnValueProvider("Hours", (control, column, entry) => Math.Round(entry.Hours, 1))
                .ForColumn("EditButton",
                    c => c.WithIgnore())
                .ForColumn(c => c.Hours, c => c.WithRule(onCellExporting: (cell, value) =>
                {
                    // highlight cells with less than 1 hours
                    if ((double)value.Value < 1)
                    {
                        cell.Style.Fill.BackgroundColor = XLColor.Yellow;
                    }
                }))
                .WithRule(onDataExported: (worksheet, _) =>
                {
                    var cell = worksheet.Cell(1, 1);
                    cell.SetValue("Time Tracking report");
                    cell.Style.Font.SetFontSize(20);
                })
                .WithTableHeader(true)
                .WithRule(onDataExported: (worksheet, _) =>
                {
                    var table = worksheet.Tables.Single();
                    table.SetShowTotalsRow(true);
                    table.Field(0).TotalsRowLabel = "TOTAL HOURS";
                    table.Field(3).TotalsRowFunction = XLTotalsRowFunction.Sum;
                });

            var export = new GridViewExportExcel<TimeTrackingEntry>(settings);
            using var file = export.Export(gridView, dataSet);
            await Context.ReturnFileAsync(file, "timetracking.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
