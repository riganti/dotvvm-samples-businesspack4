using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BPSamples.GridViewExporting.Data;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Vml.Office;
using DotVVM.BusinessPack.Controls;
using DotVVM.BusinessPack.Export;
using DotVVM.BusinessPack.Export.ColumnValueProvider;
using DotVVM.BusinessPack.Export.Csv;
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
                SortDescending = false,
                SortExpression = nameof(TimeTrackingEntry.Id)
            },
            PagingOptions =
            {
                PageSize = 15
            },
            RowEditOptions =
            {
                PrimaryKeyPropertyName = nameof(TimeTrackingEntry.Id), EditRowId = -1
            }
        };

        public void EditEntry(TimeTrackingEntry entry)
        {
            Entries.RowEditOptions.EditRowId = entry.Id;
        }

        public void UpdateEntry(TimeTrackingEntry entry)
        {
            var context = new AppDbContext();
            context.Update(entry);
            context.SaveChanges();
            CancelEdit();
        }

        private void CancelEdit()
        {
            Entries.RowEditOptions.EditRowId = -1;
        }

        public void CancelEditEntry()
        {
            CancelEdit();
            Entries.RequestRefresh();
        }



        public DefaultViewModel()
        {
            this.fakeDataService = new FakeDataService();
        }

        public override Task PreRender()
        {
            if (Entries.IsRefreshRequired)
            {
                Entries.LoadFromQueryable(fakeDataService.GetQueryable());
            }

            return base.PreRender();
        }

        public async Task ExportExcel()
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

        public async Task ExportCsv()
        {
            // get grid view
            var gridView = (GridView)Context.View.FindControlByClientId("grid");

            // preare data set
            var dataSet = new BusinessPackDataSet<TimeTrackingEntry>();
            dataSet.SortingOptions = Entries.SortingOptions;
            dataSet.LoadFromQueryable(fakeDataService.GetQueryable());

            // configure export
            var settings = new GridViewExportCsvSettings<TimeTrackingEntry>()
            {
                CreateHeader = true,
                QuotedValue = QuotedValue.Always,
                Separator = ","
            };
            settings.ColumnValueProviderHandlers.Register(new AnonymousColumnValueProvider<TimeTrackingEntry>(
                canGetColumnValueFunc: (control, column, entry) => column.ColumnName == "Hours",
                getColumnValueFunc: (control, column, entry) => column switch
                {
                    { ColumnName: "Hours" } => new ColumnValue() { Text = Math.Round(entry.Hours, 1).ToString("n1") },
                    _ => throw new NotSupportedException()
                }
            ));

            var export = new GridViewExportCsv<TimeTrackingEntry>(settings);
            using var file = export.Export(gridView, dataSet);
            await Context.ReturnFileAsync(file, "timetracking.csv", "text/csv");
        }
    }
}
