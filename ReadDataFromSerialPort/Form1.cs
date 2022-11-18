using System.IO.Ports;
using System.Linq.Expressions;

namespace ReadDataFromSerialPort
{
    public partial class Form1 : Form
    {
        private SerialPort Port;
        private DateTime datetime;
        private string in_data;
        public Form1()
        {
            InitializeComponent();
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            Port = new SerialPort();
            Port.BaudRate = 9600;
            Port.PortName = port_tb.Text;
            Port.Parity = Parity.None;
            Port.DataBits = 8;
            Port.StopBits = StopBits.One;
            Port.DataReceived += Port_DataReceived;
            try{
                Port.Open();
                data_tb.Text = "";
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message, "Error");
                    
            }
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            

            this.Invoke(new EventHandler(displaydata_event));
            in_data = Port.ReadLine();
            
        }

        private void displaydata_event(object? sender, EventArgs e)
        {
            datetime = DateTime.Now;
            string time = datetime.Hour + ":" + datetime.Minute + ":" + datetime.Second;
            data_tb.AppendText( time + "\t\t\t" + in_data+"\n");

            //int data_value = Convert.ToInt32(in_data);
            //light_pb.Value = data_value;

            //int data_value = Convert.ToInt32(in_data);
            //temp_pb.Value = data_value;
        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Port.Close();
            }
            catch(Exception ex2)
            {
                MessageBox.Show(ex2.Message, "Error");     
            }
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.FilterIndex = 2;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, data_tb.Text);
                }
            }
        }
    }
}