using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ticketandvisitormgmtsys
{
    public partial class Admin : Form
    {
        private readonly XmlSerializer staffXmlSerializer;
        private List<staffmanagement> staffmanagementList;
        private List<ticketDetails> ticketdetailsList;
        private List<CustomerDetails> customerdetailsList;
        private readonly string folderPath;
        private readonly string staffXMLFilePath;
        private readonly string ticket;
        private readonly string ticketDetailsCSVFilePath;
        private readonly string customerCSVfilePath;
        public Admin()
        {
            InitializeComponent();

            staffmanagementList = new List<staffmanagement>();
            ticketdetailsList = new List<ticketDetails>();
            customerdetailsList = new List<CustomerDetails>();
            folderPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            ticketDetailsCSVFilePath = folderPath + "\\Ticket.csv";
            customerCSVfilePath = folderPath + "\\customerdetails.csv";
        }
        private void showAdmin(bool showTicketdetails = false, bool showStaffmanagement = false, bool showWeeklyreport = false, bool showDailyreport = false)
        {
            ticketDetailsPanel.Visible = showTicketdetails;
            staffmgmtpnel.Visible = showStaffmanagement;
            weeklyReportPanel.Visible = showWeeklyreport;
            dailyReportPanel.Visible = showDailyreport;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void staffmgmtbtn_Click(object sender, EventArgs e)
        {
            showAdmin(showStaffmanagement: true);
            staffmgmtpnel.BringToFront();
        }

        private void ticketdetailsbtn_Click(object sender, EventArgs e)
        {
            showAdmin(showTicketdetails: true);
            ticketDetailsPanel.BringToFront();
        }

        private void weeklyreportbtn_Click(object sender, EventArgs e)
        {
            showAdmin(showWeeklyreport: true);
            weeklyReportPanel.BringToFront();
        }

        private void dailyreportbtn_Click(object sender, EventArgs e)
        {
            showAdmin(showDailyreport: true);
            dailyReportPanel.BringToFront();
        }

        private void ticketDetailsPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void clearStaffInput()
        {
            usernameTxt.Text = "";
            passwordTxt.Text = "";
            confirmPasswordTxt.Text = "";
            nameTxt.Text = "";
            emailTxt.Text = "";
            mobileNoTxt.Text = "";
            roleComboBox.SelectedItem = "Staff";

        }

        private void clearTicketDetails()
        {
            ticketIDtxtbox.Text = "";
            categoryCombobox.SelectedItem = "Child";
            totalPeopletxtbox.Text = "";
            dayCombobox.SelectedItem = "Weekday";
            durationCombobox.SelectedItem = "1 hrs";
            priceTxtbox.Text = "";
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            staffmanagementList = new List<staffmanagement>();

            staffmanagement staff;

            staff = new staffmanagement
            {
                role = roleComboBox.SelectedItem.ToString(),
                userName = usernameTxt.Text,
                password = passwordTxt.Text,
                name = nameTxt.Text,
                email = emailTxt.Text,
                mobileNo = mobileNoTxt.Text
            };
            staffmanagementList.Add(staff);
            staffMgmtDataGrid.Rows.Add(staff.role, staff.userName, staff.password, staff.name, staff.email, staff.mobileNo);
            clearStaffInput();

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            clearStaffInput();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            staffMgmtDataGrid.Rows[0].Cells[0].Value = roleComboBox.SelectedItem.ToString();
            staffMgmtDataGrid.Rows[0].Cells[1].Value = usernameTxt.Text;
            staffMgmtDataGrid.Rows[0].Cells[2].Value = passwordTxt.Text;
            staffMgmtDataGrid.Rows[0].Cells[3].Value = nameTxt.Text;
            staffMgmtDataGrid.Rows[0].Cells[4].Value = emailTxt.Text;
            staffMgmtDataGrid.Rows[0].Cells[5].Value = mobileNoTxt.Text;
        }

        private void staffMgmtDataGrid_MouseClick(object sender, MouseEventArgs e)
        {
            roleComboBox.SelectedItem = staffMgmtDataGrid.Rows[0].Cells[0].Value.ToString();
            usernameTxt.Text = staffMgmtDataGrid.Rows[0].Cells[1].Value.ToString();
            passwordTxt.Text = staffMgmtDataGrid.Rows[0].Cells[2].Value.ToString();
            nameTxt.Text = staffMgmtDataGrid.Rows[0].Cells[3].Value.ToString();
            emailTxt.Text = staffMgmtDataGrid.Rows[0].Cells[4].Value.ToString();
            mobileNoTxt.Text = staffMgmtDataGrid.Rows[0].Cells[5].Value.ToString();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (staffMgmtDataGrid.RowCount > 0)
            {
                if (staffMgmtDataGrid.SelectedRows.Count > 0)
                {
                    var Result = MessageBox.Show("Are you sure to delete?", "Delete!!", MessageBoxButtons.YesNo);

                    if (Result == DialogResult.Yes)
                    {
                        staffMgmtDataGrid.Rows.RemoveAt(staffMgmtDataGrid.SelectedRows[0].Index);
                    }
                }
                else
                {
                    MessageBox.Show("Row has not been Selected", "Invalid Delete");
                }
            }
            else
            {
                MessageBox.Show("Table is empty", "Invalid Delete");
            }
        }

        private void importLabel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = "Staff.csv"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (File.Exists(ofd.FileName))
                    {
                        staffmanagementList = new List<staffmanagement>();
                        DataTable dt = new DataTable();
                        string[] rawCsvLines = File.ReadAllLines(ofd.FileName);

                        if (rawCsvLines.Length > 0)
                        {
                            staffmanagement staff;
                            for (int i = 1; i < rawCsvLines.Length; i++)
                            {
                                string[] data = rawCsvLines[i].Split(',');
                                staff = new staffmanagement
                                {
                                    userName = data[0],
                                    password = data[1],
                                    name = data[2],
                                    email = data[3],
                                    mobileNo = data[4],
                                    role = data[5]
                                };
                                staffmanagementList.Add(staff);
                                staffMgmtDataGrid.Rows.Add(staff.role, staff.userName, staff.password, staff.name, staff.email, staff.mobileNo);
                            }
                            staffMgmtDataGrid.Refresh();
                            staffMgmtDataGrid.ClearSelection();
                        }

                    }
                    else
                    {
                        string message = "Specified File not found";
                        string title = "File not Found";
                        MessageBoxButtons button = MessageBoxButtons.OK;
                        MessageBoxIcon icon = MessageBoxIcon.Error;
                        DialogResult result = MessageBox.Show(message, title, button, icon);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message);
                }
            }
        }

        private void exportLabel_Click(object sender, EventArgs e)
        {
            if (staffMgmtDataGrid.RowCount != 0)
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "CSV (*.csv)|*.csv",
                    FileName = "staff.csv"
                };
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Unable to have message" + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = staffMgmtDataGrid.ColumnCount;
                            int rowCount = staffMgmtDataGrid.RowCount;
                            string[] content = new string[rowCount + 1];
                            for (int i = 0; i < rowCount + 1; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    if (i == 0)
                                    {
                                        content[i] += staffMgmtDataGrid.Columns[j].HeaderText + ",";
                                    }
                                    else
                                    {
                                        content[i] += staffMgmtDataGrid.Rows[i - 1].Cells[j].Value.ToString() + ",";
                                    }
                                }
                            }
                            File.WriteAllLines(sfd.FileName, content, Encoding.UTF8);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("There is no export", "Info");
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            ticketdetailsList = new List<ticketDetails>();

            ticketDetails ticket;

            ticket = new ticketDetails
            {
                ticketId = ticketIDtxtbox.Text,
                category = categoryCombobox.SelectedItem.ToString(),
                totalPeople = totalPeopletxtbox.Text,
                day = dayCombobox.SelectedItem.ToString(),
                duration = durationCombobox.SelectedItem.ToString(),
                price = priceTxtbox.Text
            };
            ticketdetailsList.Add(ticket);
            ticketDetailsDataGridView.Rows.Add(ticket.ticketId, ticket.category, ticket.totalPeople, ticket.day, ticket.duration, ticket.price);
            clearTicketDetails();

        }

        private void delBtn_Click(object sender, EventArgs e)
        {

            if (ticketDetailsDataGridView.RowCount > 0)
            {
                if (ticketDetailsDataGridView.SelectedRows.Count > 0)
                {
                    var Result = MessageBox.Show("Are you sure to delete?", "Delete!!", MessageBoxButtons.YesNo);

                    if (Result == DialogResult.Yes)
                    {
                        ticketDetailsDataGridView.Rows.RemoveAt(ticketDetailsDataGridView.SelectedRows[0].Index);
                    }
                }
                else
                {
                    MessageBox.Show("Row has not been Selected", "Invalid Delete");
                }
            }
            else
            {
                MessageBox.Show("Table is empty", "Invalid Delete");
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {

            ticketDetailsDataGridView.Rows[0].Cells[0].Value = ticketIDtxtbox.Text;
            ticketDetailsDataGridView.Rows[0].Cells[1].Value = categoryCombobox.SelectedItem.ToString();
            ticketDetailsDataGridView.Rows[0].Cells[2].Value = totalPeopletxtbox.Text;
            ticketDetailsDataGridView.Rows[0].Cells[3].Value = dayCombobox.SelectedItem.ToString();
            ticketDetailsDataGridView.Rows[0].Cells[4].Value = durationCombobox.SelectedItem.ToString();
            ticketDetailsDataGridView.Rows[0].Cells[5].Value = priceTxtbox.Text;
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            clearTicketDetails();
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            if (ticketDetailsDataGridView.RowCount != 0)
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "CSV (*.csv)|*.csv",
                    FileName = "Ticket.csv"
                };
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Unable to have message" + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = ticketDetailsDataGridView.ColumnCount;
                            int rowCount = ticketDetailsDataGridView.RowCount;
                            string[] content = new string[rowCount + 1];
                            for (int i = 0; i < rowCount + 1; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    if (i == 0)
                                    {
                                        content[i] += ticketDetailsDataGridView.Columns[j].HeaderText + ",";
                                    }
                                    else
                                    {
                                        content[i] += ticketDetailsDataGridView.Rows[i - 1].Cells[j].Value.ToString() + ",";
                                    }
                                }
                            }
                            File.WriteAllLines(sfd.FileName, content, Encoding.UTF8);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("There is no export", "Info");
            }
        }

        private void importBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = "Ticket.csv"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (File.Exists(ofd.FileName))
                    {
                        ticketdetailsList = new List<ticketDetails>();
                        DataTable dt = new DataTable();
                        string[] rawCsvLines = File.ReadAllLines(ofd.FileName);

                        if (rawCsvLines.Length > 0)
                        {
                            ticketDetails ticket;
                            for (int i = 1; i < rawCsvLines.Length; i++)
                            {
                                string[] data = rawCsvLines[i].Split(',');
                                ticket = new ticketDetails
                                {
                                    ticketId = data[0],
                                    category = data[1],
                                    totalPeople = data[2],
                                    day = data[3],
                                    duration = data[4],
                                    price = data[5]
                                };
                                ticketdetailsList.Add(ticket);
                                ticketDetailsDataGridView.Rows.Add(ticket.ticketId, ticket.category, ticket.totalPeople, ticket.day, ticket.duration, ticket.price);
                            }
                            ticketDetailsDataGridView.Refresh();
                            ticketDetailsDataGridView.ClearSelection();
                        }

                    }
                    else
                    {
                        string message = "Specified File not found";
                        string title = "File not Found";
                        MessageBoxButtons button = MessageBoxButtons.OK;
                        MessageBoxIcon icon = MessageBoxIcon.Error;
                        DialogResult result = MessageBox.Show(message, title, button, icon);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex.Message);
                }
            }
        }

        private void generateDailyReportBtn_Click(object sender, EventArgs e)
        {
            List<ticketDetails> tickets = new List<ticketDetails>();
            List<CustomerDetails> allcustomers = new List<CustomerDetails>();
            List<DailyReport> dailyreports = new List<DailyReport>();

            try
            {
                if (File.Exists(ticketDetailsCSVFilePath))
                {
                    ticketdetailsList = new List<ticketDetails>();
                    DataTable dt = new DataTable();
                    string[] rawCsvLines = File.ReadAllLines(ticketDetailsCSVFilePath);

                    if (rawCsvLines.Length > 0)
                    {
                        ticketDetails ticket;
                        for (int i = 1; i < rawCsvLines.Length; i++)
                        {
                            string[] data = rawCsvLines[i].Split(',');
                            ticket = new ticketDetails
                            {
                                ticketId = data[0],
                                category = data[1],
                                totalPeople = data[2],
                                day = data[3],
                                duration = data[4],
                                price = data[5]
                            };
                            ticketdetailsList.Add(ticket);
                        }
                        tickets.AddRange(ticketdetailsList);
                    }

                }
                else
                {
                    string message = "Specified File not found";
                    string title = "File not Found";
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Error;
                    DialogResult result = MessageBox.Show(message, title, button, icon);

                }

                if (File.Exists(customerCSVfilePath))
                {
                    customerdetailsList = new List<CustomerDetails>();
                    DataTable dt = new DataTable();
                    string[] rawCsvLines = File.ReadAllLines(customerCSVfilePath);

                    if (rawCsvLines.Length > 0)
                    {
                        CustomerDetails Cust;
                        for (int i = 1; i < rawCsvLines.Length; i++)
                        {
                            string[] data = rawCsvLines[i].Split(',');
                            Cust = new CustomerDetails
                            {
                                customerId = data[0],
                                customerName = data[1],
                                mobileNo = data[2],
                                Date = Convert.ToDateTime(data[3]),
                                checkInTime = data[4],
                                checkOutTime = data[5],
                                Day = data[6],
                                duration = data[7],
                                totalPrice = data[8],
                                totalpeople = data[9],
                                ticketDetailsId = data[10],
                            };
                            customerdetailsList.Add(Cust);

                        }
                        allcustomers.AddRange(customerdetailsList);
                    }

                }
                else
                {
                    string message = "Specified File not found";
                    string title = "File not Found";
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Error;
                    DialogResult result = MessageBox.Show(message, title, button, icon);

                }


                int[] group = new int[3];

                for (int i = 0; i < customerdetailsList.Count; i++)
                {
                    bool match = false;
                    Console.WriteLine(customerdetailsList[i].ticketDetailsId);
                    for (int j = 0; j < ticketdetailsList.Count; j++)
                    {

                        if (customerdetailsList[i].ticketDetailsId == ticketdetailsList[j].ticketId &&
                            customerdetailsList[i].Date.Date == dailyDatePicker.Value.Date)
                        {
                            if (string.Compare(ticketdetailsList[j].category, "Group") == 0)
                            {

                                group[0] += Convert.ToInt32(ticketdetailsList[j].totalPeople);

                                match = true;
                            }
                            else if (string.Compare(ticketdetailsList[j].category, "Adult") == 0)
                            {
                                group[1] += Convert.ToInt32(ticketdetailsList[j].totalPeople);
                                match = true;
                            }
                            else if (string.Compare(ticketdetailsList[j].category, "Child (5-12)") == 0)
                            {
                                group[2] += Convert.ToInt32(ticketdetailsList[j].totalPeople);
                                match = true;
                            }
                            if (match) { break; }
                        }
                    }
                }

                DailyReport groupDailyReport = new DailyReport
                {
                    Date = DateTime.Now.Date,
                    Category = "Group",
                    TotalCustomer = group[0]
                };
                DailyReport adultDailyReport = new DailyReport
                {
                    Date = DateTime.Now.Date,
                    Category = "Adult",
                    TotalCustomer = group[1]
                };
                DailyReport childDailyReport = new DailyReport
                {
                    Date = DateTime.Now.Date,
                    Category = "Child (5-12)",
                    TotalCustomer = group[2]
                };


                dailyreports.Add(groupDailyReport);
                dailyreports.Add(adultDailyReport);
                dailyreports.Add(childDailyReport);
                dataGridViewDailyReport.DataSource = dailyreports;
                dataGridViewDailyReport.Refresh();
                generateDailyChart(dailyreports);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception" + ex, "title");
            }
        }
        private void generateDailyChart(List<DailyReport> dailyReports)
        {
            dailyreportchart.Series["TotalCustomer"].Points.Clear();
            for (int i = 0; i < dailyReports.Count; i++)
            {
                dailyreportchart.Series["TotalCustomer"].Points.AddXY(dailyReports[i].Category, dailyReports[i].TotalCustomer);
            }
        }

        private int GetDayIndex(string day)
        {
            switch (day)
            {
                case "Sun":
                    return 0;

                case "Mon":
                    return 1;

                case "Tue":
                    return 2;

                case "Wed":
                    return 3;

                case "Thu":
                    return 4;

                case "Fri":
                    return 5;

                case "Sat":
                    return 6;

                default:
                    return 0;
            }
        }


        private void GenerateWeeklyReportChart(List<WeeklyReport> weeklyReports)
        {
            totalEarningWeeklyChart.Series["TotalCustomer"].Points.Clear();
            totalVisitorWeeklyChart.Series["TotalIncome"].Points.Clear();
            for (int i = 0; i < weeklyReports.Count; i++)
            {
                totalEarningWeeklyChart.Series["TotalIncome"].Points.AddXY(weeklyReports[i].Day, weeklyReports[i].TotalIncome);
                totalVisitorWeeklyChart.Series["TotalCustomer"].Points.AddXY(weeklyReports[i].Day, weeklyReports[i].TotalCustomer);
            }
        }
        private void generateWeeklyReportBtn_Click(object sender, EventArgs e)
        {
            List<ticketDetails> allTicketDetails = new List<ticketDetails>();
            List<CustomerDetails> allCustomerDetails = new List<CustomerDetails>();
            List<WeeklyReport> weeklyReports = new List<WeeklyReport>();

            try
            {
                if (File.Exists(ticketDetailsCSVFilePath))
                {
                    string[] ticketDatas = File.ReadAllLines(ticketDetailsCSVFilePath);
                    ticketdetailsList = new List<ticketDetails>();

                    if (ticketDatas.Length > 0)
                    {
                        ticketdetailsList = new List<ticketDetails>();

                        for (int i = 1; i < ticketDatas.Length; i++)
                        {
                            string[] ticketData = ticketDatas[i].Split(',');
                            ticketDetails ticket = new ticketDetails
                            {

                                ticketId = ticketData[0],
                                category = ticketData[1],
                                totalPeople = ticketData[2],
                                day = ticketData[3],
                                duration = ticketData[4],
                                price = ticketData[5]
                            };
                            ticketdetailsList.Add(ticket);
                        }
                        allTicketDetails.AddRange(ticketdetailsList);
                    }
                }
                else
                {

                    string message = "Specified File not found";
                    string title = "File not Found";
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Error;
                    DialogResult result = MessageBox.Show(message, title, button, icon);

                }

                if (File.Exists(customerCSVfilePath))
                {
                    customerdetailsList = new List<CustomerDetails>();
                    string[] rawCsvLines = File.ReadAllLines(customerCSVfilePath);

                    if (rawCsvLines.Length > 0)
                    {
                        CustomerDetails Cust;
                        for (int i = 1; i < rawCsvLines.Length; i++)
                        {
                            string[] data = rawCsvLines[i].Split(',');
                            Cust = new CustomerDetails
                            {
                                customerId = data[0],
                                customerName = data[1],
                                mobileNo = data[2],
                                Date = Convert.ToDateTime(data[3]),
                                checkInTime = data[4],
                                checkOutTime = data[5],
                                Day = data[6],
                                duration = data[7],
                                totalPrice = data[8],
                                totalpeople = data[9],
                                ticketDetailsId = data[10],
                            };
                            customerdetailsList.Add(Cust);

                        }
                        allCustomerDetails.AddRange(customerdetailsList);
                    }

                }
                else
                {
                    string message = "Specified File not found";
                    string title = "File not Found";
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Error;
                    DialogResult result = MessageBox.Show(message, title, button, icon);

                }


                int[] earning = new int[7];
                int[] visit = new int[7];

                for (int i = 0; i < customerdetailsList.Count; i++)
                {
                    bool match = false;
                    for (int j = 0; j < customerdetailsList.Count; j++)
                    {
                        if (customerdetailsList[i].ticketDetailsId == ticketdetailsList[j].ticketId)
                        {
                            switch (customerdetailsList[i].Date.ToString("ddd"))
                            {
                                case "Sun":
                                    earning[0] += Convert.ToInt32(ticketdetailsList[j].price);
                                    visit[0] += Convert.ToInt32(ticketdetailsList[j].totalPeople);
                                    match = true;
                                    break;

                                case "Mon":
                                    earning[1] += Convert.ToInt32(ticketdetailsList[j].price);
                                    visit[1] += Convert.ToInt32(ticketdetailsList[j].totalPeople);
                                    match = true;
                                    break;

                                case "Tue":
                                    earning[2] += Convert.ToInt32(ticketdetailsList[j].price);
                                    visit[2] += Convert.ToInt32(ticketdetailsList[j].totalPeople);
                                    match = true;
                                    break;

                                case "Wed":
                                    earning[3] += Convert.ToInt32(ticketdetailsList[j].price);
                                    visit[3] += Convert.ToInt32(ticketdetailsList[j].totalPeople);
                                    match = true;
                                    break;

                                case "Thu":
                                    earning[4] += Convert.ToInt32(ticketdetailsList[j].price);
                                    visit[4] += Convert.ToInt32(ticketdetailsList[j].totalPeople);
                                    match = true;
                                    break;

                                case "Fri":
                                    earning[5] += Convert.ToInt32(ticketdetailsList[j].price);
                                    visit[5] += Convert.ToInt32(ticketdetailsList[j].totalPeople);
                                    match = true;
                                    break;

                                case "Sat":
                                    earning[6] += Convert.ToInt32(ticketdetailsList[j].price);
                                    visit[6] += Convert.ToInt32(ticketdetailsList[j].totalPeople);
                                    match = true;
                                    break;
                            }
                            if (match) { break; }
                        }
                    }
                }

                for (int i = 0; i < 7; i++)
                {
                    WeeklyReport weeklyReport = new WeeklyReport
                    {
                        Date = startDateWeekly.Value.Date.AddDays(i),
                        Day = startDateWeekly.Value.Date.AddDays(i).ToString("ddd"),
                        TotalCustomer = earning[GetDayIndex(startDateWeekly.Value.Date.AddDays(i).ToString("ddd"))],
                        TotalIncome = visit[GetDayIndex(startDateWeekly.Value.Date.AddDays(i).ToString("ddd"))],
                    };
                    weeklyReports.Add(weeklyReport);
                }
                foreach (WeeklyReport weeklyReport in weeklyReports)
                {
                    Console.WriteLine(weeklyReport.TotalCustomer);
                }
                weeklyReportDataGrid.DataSource = weeklyReports;
                weeklyReportDataGrid.Refresh();

                GenerateWeeklyReportChart(weeklyReports);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception");
            }
        }
    }
}
