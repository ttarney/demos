using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using TestingKofaxIssues.neikofaxccmdev;

namespace TestingKofaxIssues
{
    class Program
    {
        static void Main(string[] args)
        {
            ITPCloudService proxy = new ITPCloudService();
            proxy.Url = @"http://neikofaxccmdev:8081/ccm";
            
            try
            {

                CreateDocumentNEIV1ResponseRequestinfo response = proxy.CreateDocumentNEIV1(
                                            "CCM",                          //  partner
                                            "LOCAL",                        //  customer
                                            "ContractNEI",                  //  contracttypename
                                            "V1",                           //  contracttypeversion
                                            "TTT",                          //  jobid
                                            "888-1842",                     //  tid
                                            "Welcome_Packet",               //  project
                                            "Welcome Packet DT",            //  docutemplate
                                            "Welcome Packet Email DT",      //  docutemplateemail
                                            "vCard DT",                     //  docutemplatevcard
                                            "current");                     //  publishstatus
            }
            catch(SoapException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
