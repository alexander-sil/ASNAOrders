using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Drawing;
using Quartz;
using System.Threading.Tasks;
using ASNAOrders.Web.NotificationServiceExtensions;
using System.Threading;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Globalization;

namespace ASNAOrders.Agent
{
    public class Logic
    {
        private AgentService Service { get; set; }

        private OrderNotification Notification { get; set; }

        public Logic(AgentService service)
        {
            Service = service;
        }

        public static List<List<string>> Data { get; set; } = new List<List<string>>();

        public static void DeleteXmlsOnErrorOrWhenComplete(string folderPath = "Temp", string filePath = "unparcellated.xml")
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public void OutputMessage(string category, string txnId = null, string warningMessage = null, string logMessage = null, DateTime? date = null, OrderNotification order = null, Exception ex = null, FtpWebResponse webStatus = null)
        {
            if (category == "warning")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.WarngIcon.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(Properties.Settings.Default.NotificationDelay * 1000, Properties.Resources.WarningInfoTitleTray, warningMessage + Properties.Resources.MigrationIdTrayLog + txnId, System.Windows.Forms.ToolTipIcon.Warning);
                Service.AgentEventLog.WriteEntry(logMessage + Properties.Resources.MigrationAttemptDateTrayLog + date + Properties.Resources.MigrationIdTrayLog + txnId, System.Diagnostics.EventLogEntryType.Warning);
            }
            else if (category == "activeStockUpload")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ActiveIconStockUpload.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(Properties.Settings.Default.OrderTimeout * 1000, Properties.Resources.ActiveStockUploadTitleTray, Properties.Resources.MigrationIdTrayLog + txnId, System.Windows.Forms.ToolTipIcon.Info);
                Service.AgentEventLog.WriteEntry(Properties.Resources.MigrationStartedTitleLog + Properties.Resources.MigrationIdTrayLog + txnId, System.Diagnostics.EventLogEntryType.Information);
            }
            else if (category == "activeOrder")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ActiveIconOrder.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(Properties.Settings.Default.OrderTimeout * 1000, Properties.Resources.NewOrderTitleTray, Properties.Resources.NewOrderDescTray, System.Windows.Forms.ToolTipIcon.Info);
                Service.AgentEventLog.WriteEntry(Properties.Resources.NewOrderLog + Properties.Resources.OrderIdPrepend + order.OrderId, System.Diagnostics.EventLogEntryType.Information);

                Service.AgentNotifyIcon.BalloonTipClicked -= Service.AgentNotifyIconClicked;

                Service.AgentNotifyIcon.BalloonTipClicked += OnOrderAccept;
            }
            else if (category == "eFileNotFound")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ErrorIcon.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(Properties.Settings.Default.NotificationDelay * 1000, Properties.Resources.ErrorMessageInfoTitleTray, Properties.Resources.ErrorMessageInfoFNFDescTray, System.Windows.Forms.ToolTipIcon.Error);
                Service.AgentEventLog.WriteEntry(Properties.Resources.ErrorMessageInfoFNFLog + Properties.Resources.ExceptionMessageTrayLog + ex.Message + Properties.Resources.StackTraceTrayLog + ex.StackTrace + Properties.Resources.MigrationAttemptDateTrayLog + date + Properties.Resources.MigrationIdTrayLog + txnId, System.Diagnostics.EventLogEntryType.Error);
            }
            else if (category == "eWeb")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ErrorIcon.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(Properties.Settings.Default.NotificationDelay * 1000, Properties.Resources.ErrorMessageInfoTitleTray, Properties.Resources.ErrorMessageInfoWebDescTray, System.Windows.Forms.ToolTipIcon.Error);
                Service.AgentEventLog.WriteEntry(Properties.Resources.ErrorMessageInfoWebLog + Properties.Resources.ExceptionMessageTrayLog + ex.Message + Properties.Resources.StackTraceTrayLog + $"{ex.StackTrace}{Environment.NewLine}{webStatus.StatusDescription}" + Properties.Resources.MigrationAttemptDateTrayLog + date + Properties.Resources.MigrationIdTrayLog + txnId, System.Diagnostics.EventLogEntryType.Error);
            }
            else if (category == "eSql_Syntax")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ErrorIcon.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(Properties.Settings.Default.NotificationDelay * 1000, Properties.Resources.ErrorMessageInfoTitleTray, Properties.Resources.ErrorMessageInfoSqlExcSyntaxDescTray, System.Windows.Forms.ToolTipIcon.Error);
                Service.AgentEventLog.WriteEntry(Properties.Resources.ErrorMessageSqlExcSyntaxLog + Properties.Resources.ExceptionMessageTrayLog + ex.Message + Properties.Resources.StackTraceTrayLog + ex.StackTrace + Properties.Resources.MigrationAttemptDateTrayLog + date + Properties.Resources.MigrationIdTrayLog + txnId, System.Diagnostics.EventLogEntryType.Error);
            }
            else if (category == "eSql_Server")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ErrorIcon.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(Properties.Settings.Default.NotificationDelay * 1000, Properties.Resources.ErrorMessageInfoTitleTray, Properties.Resources.ErrorMessageInfoSqlExcServerDescTray, System.Windows.Forms.ToolTipIcon.Error);
                Service.AgentEventLog.WriteEntry(Properties.Resources.ErrorMessageInfoSqlExcServerLog + Properties.Resources.ExceptionMessageTrayLog + ex.Message + Properties.Resources.StackTraceTrayLog + ex.StackTrace + Properties.Resources.MigrationAttemptDateTrayLog + date + Properties.Resources.MigrationIdTrayLog + txnId, System.Diagnostics.EventLogEntryType.Error);
            }
            else if (category == "eInvOp")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ErrorIcon.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(Properties.Settings.Default.NotificationDelay * 1000, Properties.Resources.ErrorMessageInfoTitleTray, Properties.Resources.ErrorMessageInfoInvOpDescTray, System.Windows.Forms.ToolTipIcon.Error);
                Service.AgentEventLog.WriteEntry(Properties.Resources.ErrorMessageInfoInvOpLog + Properties.Resources.ExceptionMessageTrayLog + ex.Message + Properties.Resources.StackTraceTrayLog + ex.StackTrace + Properties.Resources.MigrationAttemptDateTrayLog + date + Properties.Resources.MigrationIdTrayLog + txnId, System.Diagnostics.EventLogEntryType.Error);
            }
            else if (category == "eIndexOutOfRange")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ErrorIcon.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(Properties.Settings.Default.NotificationDelay * 1000, Properties.Resources.ErrorMessageInfoTitleTray, Properties.Resources.ErrorMessageInfoIndexOORDescTray, System.Windows.Forms.ToolTipIcon.Error);
                Service.AgentEventLog.WriteEntry(Properties.Resources.ErrorMessageInfoIndexOORLog + Properties.Resources.ExceptionMessageTrayLog + ex.Message + Properties.Resources.StackTraceTrayLog + ex.StackTrace + Properties.Resources.MigrationAttemptDateTrayLog + date + Properties.Resources.MigrationIdTrayLog + txnId, System.Diagnostics.EventLogEntryType.Error);
            }
            else if (category == "eXmlRoot")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ErrorIcon.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(Properties.Settings.Default.NotificationDelay * 1000, Properties.Resources.ErrorMessageInfoTitleTray, Properties.Resources.ErrorMessageInfoXMLRootDescTray, System.Windows.Forms.ToolTipIcon.Error);
                Service.AgentEventLog.WriteEntry(Properties.Resources.ErrorMessageInfoXMLRootLog + Properties.Resources.ExceptionMessageTrayLog + ex.Message + Properties.Resources.StackTraceTrayLog + ex.StackTrace + Properties.Resources.MigrationAttemptDateTrayLog + date + Properties.Resources.MigrationIdTrayLog + txnId, System.Diagnostics.EventLogEntryType.Error);
            }
            else if (category == "eXml")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ErrorIcon.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(Properties.Settings.Default.NotificationDelay * 1000, Properties.Resources.ErrorMessageInfoTitleTray, Properties.Resources.ErrorMessageInfoXMLDescTray, System.Windows.Forms.ToolTipIcon.Error);
                Service.AgentEventLog.WriteEntry(Properties.Resources.ErrorMessageInfoXMLLog + Properties.Resources.ExceptionMessageTrayLog + ex.Message + Properties.Resources.StackTraceTrayLog + ex.StackTrace + Properties.Resources.MigrationAttemptDateTrayLog + date + Properties.Resources.MigrationIdTrayLog + txnId, System.Diagnostics.EventLogEntryType.Error);
            }
            else if (category == "eArgument")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ErrorIcon.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(Properties.Settings.Default.NotificationDelay * 1000, Properties.Resources.ErrorMessageInfoTitleTray, Properties.Resources.ErrorMessageInfoArgumentDescTray, System.Windows.Forms.ToolTipIcon.Error);
                Service.AgentEventLog.WriteEntry(Properties.Resources.ErrorMessageInfoArgumentLog + Properties.Resources.ExceptionMessageTrayLog + ex.Message + Properties.Resources.StackTraceTrayLog + ex.StackTrace + Properties.Resources.MigrationAttemptDateTrayLog + date + Properties.Resources.MigrationIdTrayLog + txnId, System.Diagnostics.EventLogEntryType.Error);
            }
            else if (category == "eArgumentInitialization")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ErrorIcon.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(Properties.Settings.Default.NotificationDelay * 1000, Properties.Resources.ErrorMessageInfoTitleTray, Properties.Resources.ErrorMessageInfoArgumentInitializationDescTray, System.Windows.Forms.ToolTipIcon.Error);
                Service.AgentEventLog.WriteEntry(Properties.Resources.ErrorMessageInfoArgumentInitializationLog + Properties.Resources.ExceptionMessageTrayLog + ex.Message + Properties.Resources.StackTraceTrayLog + ex.StackTrace + Properties.Resources.MigrationAttemptDateTrayLog + date + Properties.Resources.MigrationIdTrayLog + txnId, System.Diagnostics.EventLogEntryType.Error);
            }
            else if (category == "eGeneric")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ErrorIcon.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(Properties.Settings.Default.NotificationDelay * 1000, Properties.Resources.ErrorMessageInfoTitleTray, Properties.Resources.ErrorMessageInfoGenericDescTray, System.Windows.Forms.ToolTipIcon.Error);
                Service.AgentEventLog.WriteEntry(Properties.Resources.ErrorMessageInfoGenericLog + Properties.Resources.ExceptionMessageTrayLog + ex.Message + Properties.Resources.StackTraceTrayLog + ex.StackTrace + Properties.Resources.MigrationAttemptDateTrayLog + date + Properties.Resources.MigrationIdTrayLog + txnId, System.Diagnostics.EventLogEntryType.Error);
            }
            else if (category == "success")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ReadyIcon.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(Properties.Settings.Default.NotificationDelay * 1000, Properties.Resources.MigrationSuccessTitleTray, Properties.Resources.MigrationSuccessDescTrayLog + Properties.Resources.MigrationIdTrayLog + txnId, System.Windows.Forms.ToolTipIcon.Info);
                Service.AgentEventLog.WriteEntry(Properties.Resources.MigrationSuccessDescTrayLog + Properties.Resources.MigrationIdTrayLog + txnId + Properties.Resources.MigrationDatePrepend + date, System.Diagnostics.EventLogEntryType.Information);

                Thread.Sleep(Properties.Settings.Default.NotificationDelay * 1000);
            }
        }

        private void OnOrderAccept(object sender, EventArgs e)
        {
            try
            {
                string items = string.Empty;

                foreach (var item in Notification.Items)
                {
                    items += $"{item}{Environment.NewLine}";
                }

                var factory = new ConnectionFactory
                {
                    HostName = !string.IsNullOrWhiteSpace(Properties.Settings.Default.MQHostname) ? Properties.Settings.Default.MQHostname : Properties.Resources.RabbitmqLocal,
                    Port = Properties.Settings.Default.MQPort != 0 ? Properties.Settings.Default.MQPort : 5672,
                    VirtualHost = !string.IsNullOrWhiteSpace(Properties.Settings.Default.MQVHost) ? Properties.Settings.Default.MQVHost : Properties.Resources.AsnaOrders
                };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: Properties.Resources.OrdersQueueProperty,
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

                        string message = JsonConvert.SerializeObject(new OrderResponse()
                        {
                            PlaceId = Properties.Settings.Default.PlaceId
                        });

                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: string.Empty,
                                         routingKey: Properties.Resources.OrdersQueueProperty,
                                         basicProperties: null,
                                         body: body);
                    }
                }

                System.Windows.Forms.MessageBox.Show($"{Properties.Resources.OrderStringIdMsgbox} {Notification.OrderId}{Environment.NewLine}{Properties.Resources.OrderStringPriceMsgbox} {Notification.Price}{Environment.NewLine}{Properties.Resources.OrderStringCompositionMsgbox} {items}", Properties.Resources.OrderStringTitleMsgbox, System.Windows.Forms.MessageBoxButtons.OK);

                Service.AgentNotifyIcon.BalloonTipClicked -= OnOrderAccept;
                Service.AgentNotifyIcon.BalloonTipClicked += Service.AgentNotifyIconClicked;

                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ReadyIcon.GetHicon());
            }
            catch (Exception ex)
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ErrorIcon.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(Properties.Settings.Default.NotificationDelay * 1000, Properties.Resources.ErrorMessageInfoTitleTray, Properties.Resources.ErrorMessageInfoGenericDescTray, System.Windows.Forms.ToolTipIcon.Error);
                Service.AgentEventLog.WriteEntry(Properties.Resources.ErrorMessageInfoGenericLog + Properties.Resources.ExceptionMessageTrayLog + ex.Message + Properties.Resources.StackTraceTrayLog + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error);
            }

        }

        public void OnReceiveNotification(object sender, BasicDeliverEventArgs e)
        {
            Notification = JsonConvert.DeserializeObject<OrderNotification>(Encoding.UTF8.GetString(e.Body.ToArray()));
            OutputMessage(category:  "activeOrder", date: DateTime.Now, order: Notification);
        }

        public void Go()
        {
            Guid id = Guid.NewGuid();
            DateTime date = DateTime.Now;

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.DBConnectionString))
            {
                OutputMessage(category: "warning", txnId: id.ToString(), warningMessage: Properties.Resources.WarningInfoDBCredsNotSetDescTray, logMessage: Properties.Resources.WarningInfoDBCredsNotSetLog, date: date);
                return;
            }


            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.FtpServerUsername) || string.IsNullOrWhiteSpace(Properties.Settings.Default.FtpServerPassword) || string.IsNullOrWhiteSpace(Properties.Settings.Default.FtpServerAddress) || Properties.Settings.Default.FtpServerPort == 0)
            {
                OutputMessage(category: "warning", txnId: id.ToString(), warningMessage: Properties.Resources.WarningInfoFTPCredsNotSetDescTray, logMessage: Properties.Resources.WarningInfoFTPCredsNotSetLog, date: date);
                return;
            }

            OutputMessage(category: "activeStockUpload", txnId: id.ToString(), date: DateTime.Now);

            try
            {
                string query = Regex.Replace(Properties.Resources.StdQuery, @"\n", " ");

                List<List<string>> data = QueryData(Properties.Settings.Default.DBConnectionString, query, Properties.Resources.DbSelectedColumns.Split(',').ToArray()).ToList();
                string[][] parcellatedData = ConvertAndParcellate(data, Properties.Resources.DbSelectedColumns.Split(','), Properties.Settings.Default.LoadBalancingOneFile, Properties.Settings.Default.LoadBalancingRowsPerFile);

                UploadStanzaToFtp(PrepareStanzaFiles(parcellatedData).ToArray(), Properties.Settings.Default.FtpServerAddress, Properties.Settings.Default.FtpServerUsername, Properties.Settings.Default.FtpServerPassword);

                OutputMessage(category: "success", txnId: id.ToString(), date: date);
            }
            catch (FileNotFoundException ex)
            {
                OutputMessage(category: "eFileNotFound", txnId: id.ToString(), date: date, ex: ex);
            }
            catch (WebException ex)
            {
                
                var status = (FtpWebResponse)ex.Response;
                OutputMessage(category: "eWeb", txnId: id.ToString(), date: date, ex: ex, webStatus: status);

            }
            catch (InvalidOperationException ex)
            {
                OutputMessage(category: "eInvOp", txnId: id.ToString(), date: date, ex: ex);
            }
            catch (SqlException ex)
            {

                if (ex.Message.Contains("syntax"))
                {
                    OutputMessage(category: "eSql_Syntax", txnId: id.ToString(), date: date, ex: ex);
                }
                else if (ex.Message.Contains("server"))
                {
                    OutputMessage(category: "eSql_Server", txnId: id.ToString(), date: date, ex: ex);
                }
            }
            catch (IndexOutOfRangeException ex)
            {

                OutputMessage(category: "eIndexOutOfRange", txnId: id.ToString(), date: date, ex: ex);
            }
            catch (XmlException ex)
            {

                if (ex.Message.Contains("root"))
                {
                    OutputMessage(category: "eXmlRoot", txnId: id.ToString(), date: date, ex: ex);
                }
                else
                {
                    OutputMessage(category: "eXml", txnId: id.ToString(), date: date, ex: ex);
                }
            }
            catch (ArgumentException ex)
            {

                if (ex.Message.Contains("initialization"))
                {
                    OutputMessage(category: "eArgumentInitialization", txnId: id.ToString(), date: date, ex: ex);
                }
                else
                {
                    OutputMessage(category: "eArgument", txnId: id.ToString(), date: date, ex: ex);
                }
            }
            catch (Exception ex)
            {
                OutputMessage(category: "eGeneric", txnId: id.ToString(), date: date, ex: ex);
            }
            finally
            {
                DeleteXmlsOnErrorOrWhenComplete();
            }
        }


        public static string[][] ConvertAndParcellate(List<List<string>> rawData, string[] columnNames, bool oneFile, int linesPerPage)
        {
            if (oneFile)
            {
                XDocument root = ConvertDataToXml(rawData, columnNames);
                return new string[][] { new string[] { root.ToString() } };
            }
            else
            {
                XDocument root = ConvertDataToXml(rawData, columnNames);
                File.WriteAllText("unparcellated.xml", root.ToString());
                string[][] parcellatedStanzas = ParcellateStanzas(linesPerPage, "unparcellated.xml");

                if (File.Exists("unparcellated.xml")) File.Delete("unparcellated.xml");

                return parcellatedStanzas;
            }
        }

        public static XDocument ConvertDataToXml(List<List<string>> rawData, string[] columnNames)
        {
            XDocument root = new XDocument();
            int i = 1;
            XElement subroot = new XElement("Root");

            foreach (List<string> row in rawData)
            {
                XElement rowNode = new XElement($"Row{i}");

                int j = 0;
                foreach (string value in row)
                {

                    XElement valueNode = new XElement(columnNames[j]);

                    if (string.IsNullOrEmpty(value))
                    {
                        valueNode.Add("?");
                    }
                    else
                    {
                        valueNode.Add(value);
                    }
                    rowNode.Add(valueNode);

                    XElement placeIdNode = new XElement("place_id");

                    placeIdNode.Add(Properties.Settings.Default.PlaceId);

                    if (j < columnNames.Length) j++;
                }
                subroot.Add(rowNode);
                i++;
            }
            root.Add(subroot);

            return root;
        }

        public static List<XElement> ReadXml(string path)
        {
            XDocument doc = XDocument.Load(path);
            XElement root = doc.Element("Root");
            List<XElement> buffer = root.Elements().ToList();
            return buffer;
        }

        public static IEnumerable<IEnumerable<XElement>> LinesPerPageParcellation(List<XElement> data, int linesPerPage)
        {
            int allPages = data.Count / linesPerPage;

            for (int page = 1; page <= allPages; page++)
            {
                yield return data.Skip((page - 1) * linesPerPage).Take(linesPerPage);
            }
        }

        public static string[][] ParcellateStanzas(int linesPerPage, string path)
        {
            List<XElement> data = ReadXml(path);
            List<string[]> buffer = new List<string[]>();

            IEnumerable<IEnumerable<XElement>> parcellatedData = LinesPerPageParcellation(data, linesPerPage);

            foreach (IEnumerable<XElement> page in parcellatedData)
            {
                List<string> internalBuffer = new List<string>();

                foreach (XElement element in page)
                {
                    internalBuffer.Add(element.ToString());
                }

                buffer.Add(internalBuffer.ToArray());
            }

            return buffer.ToArray();
        }

        public static List<FileInfo> PrepareStanzaFiles(string[][] input)
        {
            List<FileInfo> buffer = new List<FileInfo>();

            if (!Directory.Exists("Temp"))
            {
                Directory.CreateDirectory("Temp");
                Directory.SetCurrentDirectory("Temp");
            }
            else
            {
                Directory.Delete("Temp", true);

                Directory.CreateDirectory("Temp");
                Directory.SetCurrentDirectory("Temp");
            }

            int stanzaIndex = 1;

            foreach (string[] i in input)
            {
                string secondaryBuffer = "";

                foreach (string j in i)
                {
                    secondaryBuffer += j + Environment.NewLine;
                }

                File.WriteAllText($"Data{stanzaIndex}.xml", secondaryBuffer);
                buffer.Add(new FileInfo($"Data{stanzaIndex}.xml"));

                stanzaIndex++;
            }

            Directory.SetCurrentDirectory("..");
            return buffer;
        }

        public static void UploadStanzaToFtp(FileInfo[] parcellatedXmlStanzas, string address, string username, string password)
        {
            foreach (FileInfo parcellatedStanza in parcellatedXmlStanzas)
            {
                using (var client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(username, password);
                    client.UploadFile($"ftp://{address}//{DateTime.Now.ToString("dd-MM-YYYY", new CultureInfo("nl-NL"))}_{parcellatedStanza.Name}", WebRequestMethods.Ftp.UploadFile, parcellatedStanza.FullName);
                }
            }
        }

        public static IEnumerable<List<string>> QueryData(string connectionStringParameter, string query, string[] columnsToRead)
        {
            Type[] sqlTypeCheckConds = new Type[]
            {
                typeof(Int64), typeof(Byte[]), typeof(String), typeof(Boolean), typeof(DateTime), typeof(Decimal), typeof(Double),
                typeof(Int32), typeof(Int16), typeof(DateTimeOffset), typeof(TimeSpan), typeof(Guid), typeof(Object)
            };

            Func<object, List<string>, object>[] sqlToCSharpConvFuncs = new Func<object, List<string>, object>[]
            {
                (object item, List<string> list) => { list.Add(((Int64)item).ToString()); return 0; }, (object item, List<string> list) => { list.Add(Numeral.HexConverter.GetString((Byte[])item)); return 0; },
                (object item, List<string> list) => { list.Add((string)item); return 0; }, (object item, List<string> list) => { list.Add(((bool)item).ToString()); return 0; },
                (object item, List<string> list) => { list.Add(((DateTime)item).ToString()); return 0; }, (object item, List<string> list) => { list.Add(((Decimal)item).ToString()); return 0; },
                (object item, List<string> list) => { list.Add(((Double)item).ToString()); return 0; },  (object item, List<string> list) => { list.Add(((Int32)item).ToString()); return 0; },
                (object item, List<string> list) => { list.Add(((Int16)item).ToString()); return 0; },  (object item, List<string> list) => { list.Add(((DateTimeOffset)item).ToString()); return 0; },
                (object item, List<string> list) => { list.Add(((TimeSpan)item).ToString()); return 0; },  (object item, List<string> list) => { list.Add(((Guid)item).ToString()); return 0; },
                (object item, List<string> list) => { list.Add(item == null ? "" : item.ToString()); return 0; },
            };

            string queryString = query;
            string connectionString = connectionStringParameter;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    List<List<string>> data = new List<List<string>>();

                    while (reader.Read())
                    {
                        List<string> temp = new List<string>();

                        for (int i = 0; i < columnsToRead.Length; i++)
                        {
                            var dataPart = reader[columnsToRead[i]];

                            int ordinal = reader.GetOrdinal(columnsToRead[i]);

                            if (reader.IsDBNull(ordinal))
                            {
                                temp.Add("NULL");
                            }

                            for (int j = 0; j < sqlTypeCheckConds.Length; j++)
                            {

                                if (dataPart.GetType() == sqlTypeCheckConds[j])
                                {
                                    sqlToCSharpConvFuncs[j](dataPart, temp);
                                }

                            }
                        }

                        yield return temp;
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
        }
    }
}