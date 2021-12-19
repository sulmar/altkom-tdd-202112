using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Fundamentals
{

    // Raport wiekowania płatności
    // na podst. https://inforfk.pl/cykle-tematyczne/tresc,inforfk,,INF0000000000000696389,Jak-wiekowac-naleznosci-handlowe-w-ksiegach-rachunkowych.html
    public class VendorAgingReport
    {
        public DateTime CreateDate { get; private set; }
        public string Title { get; private set; }
        public IDictionary<AgingGroup, decimal> Details { get; private set; }

        public VendorAgingReport()
        {
            Details = new Dictionary<AgingGroup, decimal>
            {
                { AgingGroup.Timely, 0 },
                { AgingGroup.ExpiredTo30Days, 0 },
                { AgingGroup.ExpiredFrom30To90Days, 0 },
                { AgingGroup.ExpiredFrom90To180Days, 0 },
                { AgingGroup.ExpiredFrom180To360Days, 0 },
                { AgingGroup.ExpiredOver360Days, 0 },
            };
        }

        public class VendorAgingCalculator
        {
            public VendorAgingReport Calculate(params Invoice[] invoices)
            {
                VendorAgingReport vendorAgingReport = new VendorAgingReport();
                vendorAgingReport.CreateDate = DateTime.Now;
                vendorAgingReport.Title = $"Raport wiekowania płatności na dzień {DateTime.Today:dd.MM.yyyy}";

                foreach (var invoice in invoices)
                {
                    var intervalDays = (invoice.DueDate - DateTime.Now).TotalDays;

                    if (intervalDays < 1)
                    {
                        vendorAgingReport.Details[AgingGroup.Timely] += invoice.TotalAmount;
                    }
                    if (intervalDays > 1 && intervalDays < 30)
                    {
                        vendorAgingReport.Details[AgingGroup.ExpiredTo30Days] += invoice.TotalAmount;
                    }
                    else if (intervalDays >= 30 && intervalDays < 90)
                    {
                        vendorAgingReport.Details[AgingGroup.ExpiredFrom30To90Days] += invoice.TotalAmount;
                    }
                    else if (intervalDays >= 90 && intervalDays < 180)
                    {
                        vendorAgingReport.Details[AgingGroup.ExpiredFrom90To180Days] += invoice.TotalAmount;
                    }
                    else
                    {
                        vendorAgingReport.Details[AgingGroup.ExpiredOver360Days] -= invoice.TotalAmount;
                    }
                }


                return vendorAgingReport;

            }


        }

        public abstract class Base
        {

        }

        public class Vendor : Base
        {
            public string Name { get; set; }
        }

        public class Invoice : Base
        {
            public DateTime DueDate { get; set; }
            public Vendor Vendor { get; set; }
            public decimal TotalAmount { get; set; }
        }

        /// <summary>
        /// Struktura wiekowa należności
        /// </summary>
        public enum AgingGroup
        {
            Timely,                     // <1
            ExpiredTo30Days,            // 1-30
            ExpiredFrom30To90Days,      // 31-90
            ExpiredFrom90To180Days,     // 91-180
            ExpiredFrom180To360Days,    // 181-360
            ExpiredOver360Days,         // >360
        }

    }
}
