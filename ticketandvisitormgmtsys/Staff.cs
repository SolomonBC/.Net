using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ticketandvisitormgmtsys
{
    public partial class Staff : Form
    {
        private List<CustomerDetails> Customers;
        private List<ticketDetails> ticketDetails;
        private readonly string CustomerCSVFIlePath;
        private readonly string ticketDetailsCSVFilePath;
        private readonly string folderPath;
        public Staff()
        {
            InitializeComponent();
            Customers = new List<CustomerDetails>();
            ticketDetails = new List<ticketDetails>();
            folderPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            CustomerCSVFIlePath = folderPath + "\\Customers.csv";
            ticketDetailsCSVFilePath = folderPath + "\\ticketDetails.csv";
        }

        private void clearInputField()
        {
            customerIdtextBox.Text = "";
            customerNametextBox.Text = "";
            contactNotextBox.Text = "";
            totalPeopletextBox.Text = "";
            categorytextBox.Text = "";
            ticketDetailstextBox.Text = "";
            durationComboBox.SelectedItem = "";
            dayComboBox.SelectedItem = "";
            totalPricetextBox.Text = "";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ImportCustomerDetails_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = "customerdetails.csv"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (File.Exists(ofd.FileName))
                    {
                        Customers = new List<CustomerDetails>();
                        DataTable dt = new DataTable();
                        string[] rawCsvLines = File.ReadAllLines(ofd.FileName);

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
                                Customers.Add(Cust);
                                Console.WriteLine(Cust);
                                CustomerDataGridView.Rows.Add(Cust.customerId, Cust.customerName, Cust.mobileNo, 
                                    Cust.Date, Cust.checkInTime, Cust.checkOutTime, Cust.Day, Cust.duration, 
                                    Cust.totalPrice, Cust.totalpeople, Cust.ticketDetailsId);
                            }
                            CustomerDataGridView.Refresh();
                            CustomerDataGridView.ClearSelection();
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

        private void addBtn_Click(object sender, EventArgs e)
        {
            Customers = new List<CustomerDetails>();

            CustomerDetails cust;

            cust = new CustomerDetails
            {
                customerId = customerIdtextBox.Text,
                customerName = customerNametextBox.Text,
                Date = dateTimepicker.Value.Date,
                mobileNo = contactNotextBox.Text,
                checkInTime = checkIndateTimePicker.Value.ToString("hh:mm tt"),
                checkOutTime = checkOutdateTimePicker.Value.ToString("hh:mm tt"),
                ticketDetailsId = ticketDetailstextBox.Text,
                duration = durationComboBox.SelectedItem.ToString(),
                totalPrice = totalPricetextBox.Text,
                totalpeople = totalPeopletextBox.Text,
            };
            Customers.Add(cust);
            CustomerDataGridView.Rows.Add(cust.customerId, cust.customerName,
                cust.mobileNo, cust.Date, cust.checkInTime, cust.checkOutTime, 
                cust.duration, cust.totalPrice, cust.totalpeople,cust.totalpeople,
                cust.ticketDetailsId);

        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            CustomerDataGridView.SelectedRows[0].Cells[0].Value = customerIdtextBox.Text;
            CustomerDataGridView.SelectedRows[0].Cells[1].Value = customerNametextBox.Text;
            CustomerDataGridView.SelectedRows[0].Cells[2].Value = contactNotextBox.Text;
            CustomerDataGridView.SelectedRows[0].Cells[6].Value = dayComboBox.SelectedItem.ToString();
            CustomerDataGridView.SelectedRows[0].Cells[7].Value = durationComboBox.SelectedItem.ToString();
            CustomerDataGridView.SelectedRows[0].Cells[8].Value = totalPricetextBox.Text;
            CustomerDataGridView.SelectedRows[0].Cells[9].Value = totalPeopletextBox.Text;
            CustomerDataGridView.SelectedRows[0].Cells[10].Value = ticketDetailstextBox.Text;


        }

        private void CustomerDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            customerIdtextBox.Text = CustomerDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            customerNametextBox.Text = CustomerDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            contactNotextBox.Text = CustomerDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            dayComboBox.SelectedItem = CustomerDataGridView.SelectedRows[0].Cells[6].Value.ToString();
            durationComboBox.SelectedItem = CustomerDataGridView.SelectedRows[0].Cells[7].Value.ToString();
            totalPricetextBox.Text = CustomerDataGridView.SelectedRows[0].Cells[8].Value.ToString();
            totalPeopletextBox.Text = CustomerDataGridView.SelectedRows[0].Cells[9].Value.ToString();
            ticketDetailsId.HeaderText = CustomerDataGridView.SelectedRows[0].Cells[10].Value.ToString();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            clearInputField();
        }

        private void exportCsvBtn_Click(object sender, EventArgs e)
        {
            if (CustomerDataGridView.RowCount != 0)
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "CSV (*.csv)|*.csv",
                    FileName = "customerdetails.csv"
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
                            int columnCount = CustomerDataGridView.ColumnCount;
                            int rowCount = CustomerDataGridView.RowCount;
                            string[] content = new string[rowCount + 1];
                            for (int i = 0; i < rowCount + 1; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    if (i == 0)
                                    {
                                        content[i] += CustomerDataGridView.Columns[j].HeaderText + ",";
                                    }
                                    else
                                    {
                                        content[i] += CustomerDataGridView.Rows[i - 1].Cells[j].Value.ToString() + ",";
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

        private void importTicketBtn_Click(object sender, EventArgs e)
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
                        ticketDetails = new List<ticketDetails>();
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
                                    price=data[5]    
                                    
                                };
                                ticketDetails.Add(ticket);
                                Console.WriteLine(ticket);
                                TicketDataGridView.Rows.Add(ticket.ticketId, ticket.category, ticket.totalPeople, ticket.day,ticket.duration,ticket.price);
                            }
                            TicketDataGridView.Refresh();
                            TicketDataGridView.ClearSelection();
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

        private void deleteBtn_Click(object sender, EventArgs e)
        {

            if (CustomerDataGridView.RowCount > 0)
            {
                if (CustomerDataGridView.SelectedRows.Count > 0)
                {
                    var Result = MessageBox.Show("Are you sure to delete?", "Delete!!", MessageBoxButtons.YesNo);

                    if (Result == DialogResult.Yes)
                    {
                        CustomerDataGridView.Rows.RemoveAt(CustomerDataGridView.SelectedRows[0].Index);
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

        private void TicketDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            ticketDetailstextBox.Text = TicketDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            categorytextBox.Text = TicketDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            totalPeopletextBox.Text = TicketDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            dayComboBox.SelectedItem = TicketDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            durationComboBox.SelectedItem = TicketDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            totalPricetextBox.Text = TicketDataGridView.SelectedRows[0].Cells[5].Value.ToString();
        }
    }
}
