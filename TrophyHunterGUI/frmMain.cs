using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using System.IO;
using System.Data.SQLite;

namespace TrophyHunterGUI
{
    public partial class frmMain : Form
    {
        static IWebDriver driver;
        static string profileListUrl = "https://www.draugiem.lv/friends/?tab=2";
        static bool processRunning;
        static List<string> friendsList;

        #region Database
        static string DatabaseFile = "friendDB.db3";
        static SQLiteConnection dbConnection;
        private void ConnectToDB()
        {
            dbConnection = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\" + DatabaseFile + ";Version=3;");
            dbConnection.Open();
        }

        private void DisconnectFromDB()
        {
            dbConnection.Close();
            dbConnection = null;
        }

        private void ClearOldDB()
        {
            try
            {
                ConnectToDB();
                string sqlQuery = "DELETE FROM friends;";
                SQLiteCommand command = new SQLiteCommand(dbConnection);
                command.CommandText = sqlQuery;
                command.CommandType = System.Data.CommandType.Text;
                command.ExecuteNonQuery();

                sqlQuery = "VACUUM;";
                command = new SQLiteCommand(dbConnection);
                command.CommandText = sqlQuery;
                command.CommandType = System.Data.CommandType.Text;
                command.ExecuteNonQuery();
            }
            catch
            {
                LogMessage(string.Format("Error clearing old database "));
            }
            finally
            {
                DisconnectFromDB();
            }
        }

        private void ReadFriendsList()
        {
            try
            {
                ConnectToDB();
                string sqlQuery = "SELECT * FROM friends";
                SQLiteCommand command = new SQLiteCommand(sqlQuery, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string profile = reader.GetString(1);
                    friendsList.Add(profile);
                }
            }
            catch (Exception e)
            {
                LogMessage(string.Format("Error reading friend list: " + e.ToString()));
            }
            finally
            {
                DisconnectFromDB();
            }
        }
        #endregion

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            barCaptchaNotify.Icon = this.Icon;
            //ChangeProcessState(false);

            driver = new ChromeDriver();
            driver.Url = "https://www.draugiem.lv";
            LogMessage("Login in your profile");
        }

        //private void ChangeProcessState(bool processState)
        //{
        //    processRunning = processState;
        //    btnStop.Enabled = processRunning;
        //    btnStartHunting.Enabled = !processRunning;
        //    btnReadFriendList.Enabled = !processRunning;
        //}

        #region UI Buttons
        private void btnReadFriendList_Click(object sender, EventArgs e)
        {
            if (driver == null)
                return;

            barProgress.Maximum = 2;
            //ChangeProcessState(true);
            bWorkerRead.RunWorkerAsync();            
        }

        private void btnStartHunting_Click(object sender, EventArgs e)
        {
            if (driver == null)
                return;

            //ChangeProcessState(true);

            friendsList = new List<string>();
            friendsList.Clear();
            ReadFriendsList();

            barProgress.Maximum = friendsList.Count;

            bWorkerHunt.RunWorkerAsync();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                bWorkerHunt.CancelAsync();
                bWorkerHunt.Dispose();
            }
            catch { }

            try
            {
                bWorkerRead.CancelAsync();
                bWorkerHunt.Dispose();
            }
            catch { }
        }
        #endregion

        #region UI
        private void radOnline_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                profileListUrl = "https://www.draugiem.lv/friends/?tab=2";
        }

        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                profileListUrl = "https://www.draugiem.lv/friends/?tab=2&all";
        }

        private void LogMessage(string msg)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string>(LogMessage), new object[] { msg });
                    return;
                }
                lstLog.Items.Add(msg);
            }
            catch { }
        }
        #endregion

        #region Process profiles
        //private void FetchMutualFriends()
        //{
        //    driver.Url = profileListUrl;

        //    DateTime start = DateTime.Now;
        //    bool hasMore = false;
        //    do
        //    {
        //        IWebElement moreButton = null;
        //        try
        //        {
        //            moreButton = driver.FindElement(By.XPath("//button[.='Vairāk']"));
        //        }
        //        catch { }

        //        if (moreButton != null)
        //        {
        //            try
        //            {
        //                moreButton.Click();
        //            }
        //            catch { }

        //            hasMore = true;
        //        }
        //        else
        //        {
        //            hasMore = false;
        //        }

        //    } while (hasMore);

        //    IReadOnlyCollection<IWebElement> friendList = driver.FindElements(By.ClassName("userPicture"));

        //    if (friendList.Count > 0)
        //    {
        //        ClearOldDB();
        //    }

        //    DateTime end = DateTime.Now;
        //    TimeSpan time = end - start;
        //    LogMessage(string.Format("Mutual friends list fetched in {0}h {1}m {2}s {3}ms", time.Hours, time.Minutes, time.Seconds, time.Milliseconds));
        //    LogMessage(string.Format("Friend count found: " + friendList.Count));

        //    start = DateTime.Now;
        //    FetchProfileIDs(friendList);
        //    end = DateTime.Now;
        //    time = end - start;
        //    LogMessage(string.Format("Database records saved in {0}h {1}m {2}s {3}ms", time.Hours, time.Minutes, time.Seconds, time.Milliseconds));
        //}

        private void FetchProfileIDs(IReadOnlyCollection<IWebElement> elementList)
        {
            // Optimized database insert using transaction
            ConnectToDB();
            using (SQLiteTransaction mytransaction = dbConnection.BeginTransaction())
            {
                using (SQLiteCommand mycommand = new SQLiteCommand(dbConnection))
                {
                    SQLiteParameter myparam = new SQLiteParameter();
                    myparam.ParameterName = "@ProfileID";

                    mycommand.CommandText = "INSERT INTO friends(profileID) VALUES(@ProfileID)";
                    mycommand.Parameters.Add(myparam);

                    for (int i = 0; i < elementList.Count; i++)
                    {
                        IWebElement elem = elementList.ElementAt<IWebElement>(i);
                        IWebElement imgElem = elem.FindElement(By.XPath(".//img"));

                        string profileID = GetProfileID(imgElem.GetAttribute("src"));
                        if (profileID != string.Empty)
                        {
                            myparam.Value = profileID;
                            mycommand.ExecuteNonQuery();
                        }
                    }
                }
                mytransaction.Commit();
            }
            DisconnectFromDB();
        }

        /// <summary>
        /// Try to get Draugiem.lv profile ID from passed profile image link. Returns <c>string.Empty</c> if no ID found
        /// </summary>
        /// <returns>Returns <c>string.Empty</c> if no ID found</returns>
        private string GetProfileID(string link)
        {
            string[,] idSeperators = new string[,]{
                {"/sm_", "."}
            };

            int iterationCount = idSeperators.GetUpperBound(0);

            for (int i = 0; i <= iterationCount; i++)
            {
                int start = -1;
                int end = -1;

                // try next combination if current does not exist in string
                if (!link.Contains(idSeperators[i, 0]))
                    continue;

                start = link.IndexOf(idSeperators[i, 0]) + idSeperators[i, 0].Length;
                if (start > -1)
                {
                    end = link.IndexOf(idSeperators[i, 1], start);
                    if (end != -1)
                    {
                        return link.Substring(start, end - start);
                    }
                    else
                    {
                        end = link.IndexOf("\"", start);
                        if (end != -1)
                        {
                            return link.Substring(start, end - start);
                        }
                        return link.Substring(start);
                    }
                }
            }

            // if nothing found, return null value for later processing
            return string.Empty;
        }
        #endregion

        #region Read friends list thread
        private void bWorkerRead_DoWork(object sender, DoWorkEventArgs e)
        {
            driver.Url = profileListUrl;
            bWorkerRead.ReportProgress(0);

            DateTime start = DateTime.Now;
            bool hasMore = false;
            do
            {
                IWebElement moreButton = null;
                try
                {
                    moreButton = driver.FindElement(By.XPath("//button[.='Vairāk']"));
                }
                catch { }

                if (moreButton != null)
                {
                    try
                    {
                        moreButton.Click();
                    }
                    catch { }

                    hasMore = true;
                }
                else
                {
                    hasMore = false;
                }

            } while (hasMore);

            IReadOnlyCollection<IWebElement> friendList = driver.FindElements(By.ClassName("userPicture"));

            if (friendList.Count > 0)
            {
                ClearOldDB();
            }

            DateTime end = DateTime.Now;
            TimeSpan time = end - start;
            LogMessage(string.Format("Mutual friends list fetched in {0}h {1}m {2}s {3}ms", time.Hours, time.Minutes, time.Seconds, time.Milliseconds));
            LogMessage(string.Format("Friend count found: " + friendList.Count));
            bWorkerRead.ReportProgress(1);

            start = DateTime.Now;
            FetchProfileIDs(friendList);
            end = DateTime.Now;
            time = end - start;
            LogMessage(string.Format("Database records saved in {0}h {1}m {2}s {3}ms", time.Hours, time.Minutes, time.Seconds, time.Milliseconds));
            bWorkerRead.ReportProgress(2);
        }

        private void bWorkerRead_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                barProgress.Value = e.ProgressPercentage;
                switch (e.ProgressPercentage)
                {
                    case 0:
                        lblProgress.Text = "Fetching user list";
                        break;
                    case 1:
                        lblProgress.Text = "Saving user list";
                        break;
                    case 2:
                        lblProgress.Text = "List fetched!";
                        break;
                    default:
                        lblProgress.Text = string.Format("{0}/{1}", e.ProgressPercentage, 2);
                        break;
                }
            }
            catch { }
        }

        private void bWorkerRead_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                LogMessage("Worker Cancelled!");
                //ChangeProcessState(false);
                return;
            }
            else if (!(e.Error == null))
            {
                LogMessage("Worker Error: " + e.Error.Message);
                //ChangeProcessState(false);
                return;
            }
            else
            {
                if (bWorkerRead.CancellationPending)
                {
                    LogMessage("Worker Cancelled!");
                    //ChangeProcessState(false);
                    return;
                }
            }
        }
        #endregion

        #region Hunt trophies thread
        private void bWorkerHunt_DoWork(object sender, DoWorkEventArgs e)
        {
            int friendIndex = (int)numContinueFrom.Value;

            if ((bWorkerHunt.CancellationPending == true))
            {
                e.Cancel = true;
                return;
            }

            if (friendIndex > (friendsList.Count - 1))
            {
                LogMessage("All friends processed! Well done :)");
                e.Cancel = true;
                return;
            }

            string profileUrl = "https://www.draugiem.lv/user/" + friendsList[friendIndex];
            driver.Url = profileUrl;

            bool captcha = false;
            do // loop if captcha found & wait for user input
            {
                IWebElement captchaElement = null;
                try
                {
                    captchaElement = driver.FindElement(By.Name("captcha"));
                }
                catch { }

                if (captchaElement != null)
                {
                    captcha = true;
                    barCaptchaNotify.ShowBalloonTip(500);
                    LogMessage("Captcha found! Waiting for input");
                    Thread.Sleep(2000);
                }
                else
                {
                    captcha = false;
                }
            } while (captcha);

            //Find some element on page
            IWebElement element = null;
            try
            {
                element = driver.FindElement(By.Id("rio_trophy_back"));
            }
            catch { }

            if (element != null)
            {
                LogMessage(string.Format("{1}/{2}: {0}   Click :)", profileUrl, friendIndex + 1, friendsList.Count));

                try
                {
                    element.Click();
                }
                catch { }
            }
            else
            {
                LogMessage(string.Format("{1}/{2}: {0}  Not Found :(", profileUrl, friendIndex + 1, friendsList.Count));
            }

            bWorkerHunt.ReportProgress(friendIndex + 1);
        }

        private void bWorkerHunt_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                barProgress.Value = e.ProgressPercentage;
                lblProgress.Text = string.Format("{0}/{1}", e.ProgressPercentage, friendsList.Count);
            }
            catch { }
        }

        private void bWorkerHunt_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                LogMessage("Worker Cancelled!");
                //ChangeProcessState(false);
                return;
            }
            else if (!(e.Error == null))
            {
                LogMessage("Worker Error: " + e.Error.Message);
                //ChangeProcessState(false);
                return;
            }
            else
            {
                if (bWorkerHunt.CancellationPending)
                {
                    LogMessage("Worker Cancelled!");
                    //ChangeProcessState(false);
                    return;
                }

                numContinueFrom.Value += 1;
                bWorkerHunt.RunWorkerAsync();
                return;
            }
        }
        #endregion

        private void btnReopenChrome_Click(object sender, EventArgs e)
        {
            if (driver == null)
            {
                driver = new ChromeDriver();
                driver.Url = "https://www.draugiem.lv";
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                driver.Quit();
                driver.Dispose();
            }
            catch { }
        }
    }
}
