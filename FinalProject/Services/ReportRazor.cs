using FinalProject.Models.Entities;
using RazorEngine;
using RazorEngine.Templating;

namespace FinalProject.Services;

public class ReportRazor : IReport
{
    private const string _TemplateText = 
        @"Report for Manager
Report Number: @Model.ReportNumber
Creation Time: @Model.ReportCreationTime.ToString(""dd.MM.yyyy HH:mm:ss"")
Subject: @Model.ReportDescription";
    public int ReportNumber { get; }
    public DateTime CreationDate { get; set; }
    public string? Description { get; set; }
    public ReportRazor()
    {
        ReportNumber++;
    }

    public string Create()
    {        
        var template_text = _TemplateText;
                
        var result = Engine.Razor.RunCompile(template_text, "ReportTemplate", null, new
        {
            ReportNumber,
            ReportCreationTime = CreationDate,
            ReportDescription = Description
        });

        return result;
    }
}
