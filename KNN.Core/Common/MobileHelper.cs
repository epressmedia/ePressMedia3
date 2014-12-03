using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;


namespace EPM.Core
{
    public class MobileHelper
    {

        ///
        /// Get data about a particular capability or property of a device.
        ///
        /// The name of the Device to query.
        /// The name of the Capability of Property to retrieve data about.
        ///
        public static string AboutDevice(string Device, string Capability)
        {
            string DataFilePath = HostingEnvironment.MapPath("/App_Data/MobileDevices.dat");
            if (System.IO.File.Exists(DataFilePath))
            {
                //Creates provider
                var Provider = FiftyOne.Foundation.Mobile.Detection.Binary.Reader.Create(DataFilePath);

                //If you would prefer to use the xml format then use this code
                //var xmlList = new List();
                //xmlList.Add(@"App_Data/51Degrees.mobi-Lite.xml.gz");
                //var Provider = FiftyOne.Foundation.Mobile.Detection.Xml.Reader.Create(xmlList);

                //Creates DeviceInfo objects to obtain data from.
                BaseDeviceInfo deviceInfo = Provider.GetDeviceInfo(Device);

                //Will be an empty string if either a capability or device wasn't identified.
                return deviceInfo.GetFirstPropertyValue(Capability);
            }
            throw new System.IO.FileNotFoundException("A data file was not found. You can download a Lite data file from http://51degrees.codeplex.com", DataFilePath);
        }
    }
}
