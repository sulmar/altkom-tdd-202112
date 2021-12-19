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
    public class LegalPersonality : Report
    {

    }

    public class ReportFactory
    {
        public static Report Create(string type)
        {
            // P, LP, LF -> Osobowość prawna
            // F -> Działalność fizyczna
            // nieznany -> NotSupportedException

            throw new NotImplementedException();
        }
    }

 
}
