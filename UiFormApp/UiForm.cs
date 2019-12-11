namespace UiFormApp
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Net;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class UiForm : Form
    {
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Stopwatch stopwatch;

        public UiForm()
        {
            this.InitializeComponent();

            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker helperBW = sender as BackgroundWorker;
            string url = (string)e.Argument;
            e.Result = this.DownloadString(url);
            if (helperBW.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {

                this.contentTxbLeft.Text = e.Result.ToString();
            }
            this.logLabelLeft.Text = $@"Downloaded in {stopwatch.ElapsedMilliseconds} ms";
        }

        private void DownloadBtnLeft_Click(object sender, System.EventArgs e)
        {
            var url = this.urlTextBoxLeft.Text;

            stopwatch.Reset();
            stopwatch.Start();

            backgroundWorker1.RunWorkerAsync(url);
        }

        private async void DownloadBtnRight_Click(object sender, System.EventArgs e)
        {
            var url = this.urlTextBoxRight.Text;
            stopwatch.Reset();
            stopwatch.Start();

            WebClient client = new WebClient();
            string source = await client.DownloadStringTaskAsync(url);

            this.contentTxbRight.Text = source;
            this.logLabelRight.Text = $@"Downloaded in {stopwatch.ElapsedMilliseconds} ms";
        }

        private string DownloadString(string url)
        {
            return new WebClient().DownloadString(url);
        }
    }
}