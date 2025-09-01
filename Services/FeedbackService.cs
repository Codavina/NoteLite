using System;
using System.Windows.Forms;


namespace NoteLite.Services
{
    public class FeedbackService:IFeedbackService
    {
        public void SendFeedback()
        {
            string to = "moussamed104@gmail.com"; // Change it to your real email
            string subject = Uri.EscapeDataString("Feedback - NoteLite");
            string body = Uri.EscapeDataString("Please write your feedback below:\n\n");

            string mailto = $"mailto:{to}?subject={subject}&body={body}";

            try
            {
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = mailto,
                    UseShellExecute = true // Necessary to open browser or mail properly
                };

                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open mail client. " + ex.Message);
            }
        }
    }
}
