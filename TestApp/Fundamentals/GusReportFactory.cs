using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Fundamentals.Gus
{

    public abstract class Report
    {
        public string Name { get; set; }
    }

    // Działalność fizyczna
    public class SoleTraderReport : Report
    {

    }

    // Osobowość prawna
    public class LegalPersonalityReport : Report
    {

    }

    public class ReportFactory
    {
        public static Report Create(string type) => type switch
        {
            "P" or "LP" or "LF" => new LegalPersonalityReport(),
            "F" => new SoleTraderReport(),
            _ => throw new NotSupportedException(),
        };

        // P, LP, LF -> Osobowość prawna// F -> Działalność fizyczna// nieznany -> NotSupportedException
    }   
}
