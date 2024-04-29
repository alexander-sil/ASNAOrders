using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Data;
using System.Collections;
using System.Security.Policy;
using System.ServiceProcess;
using System.Drawing;
using Numeral;

namespace ASNAOrders.Agent
{
    internal class Logic
    {
        private AgentService Service { get; set; }

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

        public void OutputMessage(string category, string trayTitle, string trayMessage, string eventLogMessage, string txnId = null, string exceptionMessage = null, string stackTrace = null, DateTime? date = null)
        {
            if (category == "activeStockUpload")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ActiveIconStockUpload.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(int.Parse(Properties.Settings.Default.NotificationDelay) * 1000, trayTitle, trayMessage + txnId, System.Windows.Forms.ToolTipIcon.Info);
                Service.AgentEventLog.WriteEntry(eventLogMessage + txnId, System.Diagnostics.EventLogEntryType.Information);
            } 
            else if (category == "activeOrder")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ActiveIconOrder.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(int.Parse(Properties.Settings.Default.NotificationDelay) * 1000, trayTitle, trayMessage + txnId, System.Windows.Forms.ToolTipIcon.Info);
                Service.AgentEventLog.WriteEntry(eventLogMessage + txnId, System.Diagnostics.EventLogEntryType.Information);
            }
            else if (category == "error")
            {
                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ErrorIcon.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(int.Parse(Properties.Settings.Default.NotificationDelay) * 1000, trayTitle, trayMessage + txnId, System.Windows.Forms.ToolTipIcon.Info);
                Service.AgentEventLog.WriteEntry(eventLogMessage + txnId + date, System.Diagnostics.EventLogEntryType.Information);
            }
        }

        public void Go()
        {
            Guid id = Guid.NewGuid();

            try
            {

                

                string query = Regex.Replace(Properties.Resources.StdQuery, @"\n", " ");

                List<List<string>> data = QueryData(Configuration.DBConnectionString, query, Configuration.DBSelectedColumns).ToList();
                string[][] parcellatedData = ConvertAndParcellate(data, Configuration.DBSelectedColumns, Configuration.OneFile, Configuration.LinesPerFile);

                UploadStanzaToFtp(PrepareStanzaFiles(parcellatedData).ToArray(), Configuration.FTPServerAddress, Configuration.FTPServerUsername, Configuration.FTPServerPassword);

                Service.AgentNotifyIcon.Icon = Icon.FromHandle(Properties.Resources.ReadyIcon.GetHicon());
                Service.AgentNotifyIcon.ShowBalloonTip(int.Parse(Properties.Settings.Default.NotificationDelay) * 1000, Properties.Resources.MigrationSuccessTitleTray, Properties.Resources.MigrationSuccessDescTrayLog + id.ToString(), System.Windows.Forms.ToolTipIcon.Info);
                Service.AgentEventLog.WriteEntry(Properties.Resources.MigrationSuccessDescTrayLog + id.ToString(), System.Diagnostics.EventLogEntryType.Information);
            }
            catch (FileNotFoundException ex)
            {
                DeleteXmlsOnErrorOrWhenComplete();
                Service.AgentEventLog.WriteEntry(Properties.Resources.ErrorMessageInfoFNFLog + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                
                return;
            }
            catch (WebException ex)
            {
                string status = ((FtpWebResponse)ex.Response).StatusDescription;
                
            }
            catch (InvalidOperationException ex)
            {
                DeleteXmlsOnErrorOrWhenComplete();
                MessageBox.Show($"Ошибка сети/валидации.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            catch (SqlException ex)
            {
                DeleteXmlsOnErrorOrWhenComplete();

                if (ex.Message.Contains("syntax"))
                {
                    MessageBox.Show($"Ошибка SQL. Неверный формат запроса.{Environment.NewLine}{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Message.Contains("server"))
                {
                    MessageBox.Show($"Ошибка SQL. Превышено допустимое время операции.{Environment.NewLine}{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return;
            }
            catch (IndexOutOfRangeException)
            {
                DeleteXmlsOnErrorOrWhenComplete();

                MessageBox.Show($"Ошибка парсинга списка столбцов.{Environment.NewLine}Неверный формат данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (XmlException ex)
            {
                DeleteXmlsOnErrorOrWhenComplete();

                if (ex.Message.Contains("root"))
                {
                    MessageBox.Show($"Ошибка XML.{Environment.NewLine}Неверный формат данных (корневой элемент)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Ошибка XML.{Environment.NewLine}Неверный формат данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return;
            }
            catch (ArgumentException ex)
            {
                DeleteXmlsOnErrorOrWhenComplete();

                if (ex.Message.Contains("initialization"))
                {
                    MessageBox.Show($"Ошибка парсинга строки подключения.{Environment.NewLine}Неверный формат данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Ошибка парсинга данных.{Environment.NewLine}{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return;
            }
            catch (Exception ex)
            {
                DeleteXmlsOnErrorOrWhenComplete();
                MessageBox.Show($"Неизвестная ошибка.{Environment.NewLine}{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DeleteXmlsOnErrorOrWhenComplete();
                (Application.OpenForms["MainForm"] as MainForm).StatusBar.Style = ProgressBarStyle.Blocks;
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
            } else
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
                    client.UploadFile($"ftp://{address}//{Configuration.FTPServerPrefix}_{parcellatedStanza.Name}", WebRequestMethods.Ftp.UploadFile, parcellatedStanza.FullName);
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
                (object item, List<string> list) => { list.Add(((Int64)item).ToString()); return 0; }, (object item, List<string> list) => { list.Add(HexConverter.GetString((Byte[])item)); return 0; },
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
