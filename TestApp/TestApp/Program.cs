
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml.Linq;
//using Reimers.Google.Analytics.Reports;
using System.Globalization;
 
namespace Reimers.Google.Analytics
{
    public class ReportRequestor
    {
        public static void Main()
        {
            System.Console.WriteLine(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
        }
        //#region Fields

        //private static readonly string requestUrlFormat = "https://www.google.com/analytics/feeds/data?ids={0}&dimensions={1}&metrics={2}&start-date={3}&end-date={4}";
        //private static readonly string authUrlFormat = "accountType=GOOGLE&Email={0}&Passwd={1}&source=reimers.dk-analyticsreader-0.1&service=analytics";
        //private static CultureInfo ci = CultureInfo.GetCultureInfo("en-US");
        //private string _token = null;
        //private string _username = null;
        //private string _password = null;

        //#endregion

        //#region Constructor

        //public ReportRequestor() { }

        //public ReportRequestor(string email, string password)
        //{
        //    _username = email;
        //    _password = password;
        //}

        //#endregion

        //#region Properties

        //public string Email
        //{
        //    get { return _username; }

        //    set
        //    {
        //        if (!string.Equals(_username, value))
        //        {
        //            _username = value;
        //            _token = null;
        //        }
        //    }
        //}

        //public string Password
        //{
        //    get { return _password; }
        //    set
        //    {
        //        if (!string.Equals(_password, value))
        //        {
        //            _password = value;
        //            _token = null;
        //        }
        //    }
        //}

        //#endregion

        //#region Methods

        //private string GetToken(string username, string password)
        //{
        //    if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password))
        //    {
        //        throw new ArgumentNullException("Username, Password", "Username and/or password not set");
        //    }

        //    string authBody = string.Format(authUrlFormat, username, password);
        //    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://www.google.com/accounts/ClientLogin");
        //    req.Method = "POST";
        //    req.ContentType = "application/x-www-form-urlencoded";
        //    req.UserAgent = "Reimers.dk req";

        //    Stream stream = req.GetRequestStream();
        //    StreamWriter sw = new StreamWriter(stream);
        //    sw.Write(authBody);
        //    sw.Close();
        //    sw.Dispose();

        //    HttpWebResponse response = (HttpWebResponse)req.GetResponse();
        //    StreamReader sr = new StreamReader(response.GetResponseStream());
        //    string token = sr.ReadToEnd();
        //    string[] tokens = token.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

        //    foreach (string item in tokens)
        //    {
        //        if (item.StartsWith("Auth="))
        //        {
        //            return item.Replace("Auth=", "");
        //        }
        //    }

        //    return string.Empty;
        //}

        //public IEnumerable GetAccounts()
        //{
        //    if (string.IsNullOrEmpty(_token))
        //    {
        //        _token = GetToken(_username, _password);
        //    }

        //    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://www.google.com/analytics/feeds/accounts/default");
        //    req.Headers.Add("Authorization: GoogleLogin auth=" + _token);
        //    HttpWebResponse response = (HttpWebResponse)req.GetResponse();

        //    Stream responseStream = response.GetResponseStream();
        //    StreamReader sr = new StreamReader(responseStream);
        //    string responseXml = sr.ReadToEnd();

        //    XDocument doc = XDocument.Parse(responseXml);
        //    XNamespace dxpSpace = doc.Root.GetNamespaceOfPrefix("dxp");
        //    XNamespace defaultSpace = doc.Root.GetDefaultNamespace();

        //    var entries = from en in doc.Root.Descendants(defaultSpace + "entry")
        //                  select new AnalyticsAccountInfo
        //                  {
        //                      AccountID = en.Elements(dxpSpace + "property").Where(xe => xe.Attribute("name").Value == "ga:accountId").First().Attribute("value").Value,
        //                      AccountName = en.Elements(dxpSpace + "property").Where(xe => xe.Attribute("name").Value == "ga:accountName").First().Attribute("value").Value,
        //                      ID = en.Element(defaultSpace + "id").Value,
        //                      Title = en.Element(defaultSpace + "title").Value,
        //                      ProfileID = en.Elements(dxpSpace + "property").Where(xe => xe.Attribute("name").Value == "ga:profileId").First().Attribute("value").Value,
        //                      WebPropertyID = en.Elements(dxpSpace + "property").Where(xe => xe.Attribute("name").Value == "ga:webPropertyId").First().Attribute("value").Value
        //                  };

        //    return entries;
        //}

        //private XDocument getReport(AnalyticsAccountInfo account, IEnumerable dimensions, IEnumerable metrics, DateTime from, DateTime to)
        //{
        //    if (string.IsNullOrEmpty(_token))
        //    {
        //        _token = GetToken(_username, _password);
        //    }

        //    StringBuilder dims = new StringBuilder();

        //    foreach (Dimension item in dimensions)
        //    {
        //        dims.Append("ga:" + item.ToString() + ",");
        //    }

        //    StringBuilder mets = new StringBuilder();

        //    foreach (Metric item in metrics)
        //    {
        //        mets.Append("ga:" + item.ToString() + ",");
        //    }

        //    string requestUrl = string.Format(requestUrlFormat, "ga:" + account.ProfileID, dims.ToString().Trim(",".ToCharArray()), mets.ToString().Trim(",".ToCharArray()), from.ToString("yyyy-MM-dd"), to.ToString("yyyy-MM-dd"));

        //    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
        //    req.Headers.Add("Authorization: GoogleLogin auth=" + _token);
        //    HttpWebResponse response = (HttpWebResponse)req.GetResponse();

        //    Stream responseStream = response.GetResponseStream();
        //    string responseXml = new StreamReader(responseStream, Encoding.UTF8, true).ReadToEnd();
        //    XDocument doc = XDocument.Parse(responseXml);

        //    return doc;
        //}

        //public IEnumerable RequestReport(AnalyticsAccountInfo account, IEnumerable dimensions, IEnumerable metrics, DateTime from, DateTime to)
        //{
        //    XDocument doc = getReport(account, dimensions, metrics, from, to);
        //    XNamespace dxpSpace = doc.Root.GetNamespaceOfPrefix("dxp");
        //    XNamespace defaultSpace = doc.Root.GetDefaultNamespace();

        //    var gr = from r in doc.Root.Descendants(defaultSpace + "entry")
        //             select new GenericEntry
        //             {
        //                 Dimensions = new List> (
        //                     from rd in r.Elements(dxpSpace + "dimension")
        //                     select new KeyValuePair(
        //                                                         (Dimension)Enum.Parse(
        //                                                         typeof(Dimension),
        //                                                         rd.Attribute("name").Value.Replace("ga:", ""),
        //                                                         true),
        //                                                         rd.Attribute("value").Value)),
        //                 Metrics = new List> (
        //                     from rm in r.Elements(dxpSpace + "metric")
        //                     select new KeyValuePair(
        //                         (Metric)Enum.Parse(typeof(Metric), rm.Attribute("name").Value.Replace("ga:", ""), true),
        //                         rm.Attribute("value").Value))
        //             };

        //    return gr;
        //}

        //public IEnumerable GetUserCountByLocation(AnalyticsAccountInfo account, DateTime from, DateTime to)
        //{
        //    IEnumerable report = RequestReport(account, new Dimension[] { Dimension.city, Dimension.latitude, Dimension.longitude }, new Metric[] { Metric.visits }, from, to);

        //    var cr = from r in report
        //             select new CityReport
        //             {
        //                 City = r.Dimensions.First(d => d.Key == Dimension.city).Value,
        //                 Latitude = Convert.ToDouble(r.Dimensions.First(d => d.Key == Dimension.latitude).Value, ci),
        //                 Longitude = Convert.ToDouble(r.Dimensions.First(d => d.Key == Dimension.longitude).Value, ci),
        //                 Count = Convert.ToInt32(r.Metrics.First(m => m.Key == Metric.visits).Value)
        //             };

        //    return cr;
        //}

        //#endregion


    }
}