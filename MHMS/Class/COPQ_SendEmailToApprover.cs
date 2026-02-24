using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using MHMS.Connection;
using ColorCode.Compilation.Languages;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace MHMS.Class
{
    public class COPQ_SendEmailToApprover
    {
        //SQL Connection
        SqlConnection con = new SqlConnection(SQLControl.MHMS_Conn);

        string Email = string.Empty;
        int NoOfCOPQPendingApproval;
        string innerString;
        private string section;
        string sectionHavePending;

        //Send email to Applying COPQ PIC ===================================================
        public async Task SendEmailToCOPQPIC()
        {
            try
            {
                await con.OpenAsync();

                // Fetch COPQ PIC email list
                using (SqlCommand selectPICCmd = new SqlCommand("SP_SelectCOPQApproverEmail", con))
                {
                    selectPICCmd.CommandType = CommandType.StoredProcedure;
                    selectPICCmd.Parameters.AddWithValue("@Procedure", "SelectCOPQPICEmail");
                    selectPICCmd.Parameters.AddWithValue("@Section", "");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectPICCmd))
                    using (DataTable emailTable = new DataTable())
                    {
                        adapter.Fill(emailTable);

                        if (emailTable.Rows.Count == 0)
                        {
                            MessageBox.Show("No COPQ PIC emails found.");
                            return;
                        }

                        string emailList = string.Join(", ", emailTable.AsEnumerable().Select(r => r["Email"].ToString()));

                        // Build email body
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine("Dear COPQ PIC's,<br><br>");
                        builder.AppendLine("Good day!<br><br>");
                        builder.AppendLine("This is to inform you that there are pending COPQ items requiring your approval.<br>");
                        builder.AppendLine("Below is a summary of the sections with pending approvals:<br>");

                        // Add table structure
                        builder.AppendLine("<table width='50%' cellpadding='0' cellspacing='0' style='border-collapse: collapse;'>");
                        builder.AppendLine("<tr><th style='border: 1px solid gray; padding:5px;'>Issuing Section</th>");
                        builder.AppendLine("<th style='border: 1px solid gray; padding:5px;'>No. of pending approval</th></tr>");

                        using (SqlCommand selectPendingCmd = new SqlCommand("SP_SelectCOPQPendingApproval", con))
                        {
                            selectPendingCmd.CommandType = CommandType.StoredProcedure;
                            selectPendingCmd.Parameters.AddWithValue("@Procedure", "SelectForApprovalByCOPQPIC");
                            selectPendingCmd.Parameters.AddWithValue("@Section", "");

                            using (SqlDataAdapter da = new SqlDataAdapter(selectPendingCmd))
                            using (DataTable pendingTable = new DataTable())
                            {
                                da.Fill(pendingTable);

                                foreach (DataRow dr in pendingTable.Rows)
                                {
                                    builder.AppendLine("<tr align='center'>");
                                    builder.AppendLine($"<td style='border: 1px solid gray; padding:5px;'>{dr["Section"]}</td>");
                                    builder.AppendLine($"<td style='border: 1px solid gray; padding:5px;'>{dr["NoOfPending"]}</td>");
                                    builder.AppendLine("</tr>");
                                }
                            }
                        }

                        builder.AppendLine("</table><br>");
                        builder.AppendLine("For your checking and approval, kindly open MHMS application.<br><br>");
                        builder.AppendLine("Link (リンク):<br>");
                        builder.AppendLine(@"<a href='\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe'>MHMS Application</a><br><br>");
                        builder.AppendLine("Thanks and Best Regards.<br><br>");
                        builder.AppendLine("<b>[This is an automatically generated e-mail]</b>");

                        // Send email
                        using (MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", emailList))
                        {
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph"); // Optional BCC
                            mail.Subject = $"[BIPH_MHMS] FY.{DateTime.Now.Year}: COPQ PIC Pending Approval as of {DateTime.Now:MM/dd/yyyy hh:mm tt}";
                            mail.Body = builder.ToString();
                            mail.IsBodyHtml = true;
                            mail.Priority = MailPriority.High;

                            using (SmtpClient client = new SmtpClient("10.113.10.1"))
                            {
                                client.Port = 25;
                                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                                client.UseDefaultCredentials = false;

                                await client.SendMailAsync(mail);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Email sending failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }


        //Send email to Applying SPV ===================================================
        public async Task SendEmailToSPV(string section)
        {
            await Task.Delay(60000);  // Simulating a delay of 60 seconds

            this.section = section;

            //Select COPQ PIC
            await con.OpenAsync();

            try
            {
                SqlCommand SelectCOPQPIC = new SqlCommand("SP_SelectCOPQApproverEmail", con);
                SelectCOPQPIC.CommandType = CommandType.StoredProcedure;
                SelectCOPQPIC.Parameters.AddWithValue("@Procedure", "SelectSPVEmail");
                SelectCOPQPIC.Parameters.AddWithValue("@Section", section);
                SqlDataAdapter sda = new SqlDataAdapter(SelectCOPQPIC);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    //List of COPQ PIC email
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());

                    foreach (DataRow row in dTable.Rows)
                    {
                        Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();


                        builder.Append("Dear SPV's,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("This is to inform you that there are pending COPQ items requiring your approval.");
                        builder.Append("<br>");
                        builder.Append("Below is a summary of pending approvals:");
                        builder.Append("<br>");


                        //Insert Table ====================================================

                        string mailBody = "<table width='50%' cellpadding='0' cellspacing='0' style='border-collapse: collapse;'>";
                        mailBody += "<tr>";
                        mailBody += "<th style='border: 1px solid gray; padding:5px;'>Issuing Section</th>";
                        mailBody += "<th style='border: 1px solid gray; padding:5px;'>No. of pending approval</th>";
                        mailBody += "</tr>";



                        //Select no. of COPQ pending approval for COPQ PIC 
                        SqlCommand SelectCOPQPendingApproval = new SqlCommand("SP_SelectCOPQPendingApproval", con);
                        SelectCOPQPendingApproval.CommandType = CommandType.StoredProcedure;
                        SelectCOPQPendingApproval.Parameters.AddWithValue("@Procedure", "SelectForApprovalBySPV");
                        SelectCOPQPendingApproval.Parameters.AddWithValue("@Section", section);
                        SqlDataAdapter da = new SqlDataAdapter(SelectCOPQPendingApproval);
                        DataTable dt = new DataTable();
                        await Task.Run(() => da.Fill(dt));

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                string Section = dr["Section"].ToString();
                                int NoOfCOPQPendingApproval = Convert.ToInt32(dr["NoOfPending"]);

                                mailBody += $"<tr align='Center'>"
                                    + $"<td style='border: 1px solid gray; padding:5px;'>{Section}</td>"
                                    + $"<td style='border: 1px solid gray; padding:5px;'>{NoOfCOPQPendingApproval}</td>"
                                    + "</tr>";
                            }

                        }

                        mailBody += "</table>";

                        builder.Append("" + mailBody);

                        //================================================================

                        builder.Append("<br>");
                        builder.Append("<br>");
                        builder.Append("For your checking and approval, kindly open MHMS application.");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");

                        builder.Append("Thanks and Best Regards.");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");

                        innerString = builder.ToString();


                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            //MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", "arvin.caparros@brother-biph.com.ph");
                            //mail.CC.Add(EmailListCC);
                            //mail.Bcc.Add("dianelleyasdane.estacio@brother-biph.com.ph");
                            //mail.Bcc.Add("jeancy.dimayuga@brother-biph.com.ph");
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");

                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";

                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": SPV - COPQ Pending Approval as of " + DateTime.Now.ToString();
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;

                            // Send the email
                            await client.SendMailAsync(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing: {ex.Message}");
            }
            finally
            {
                con.Close();  // Ensure the connection is closed
            }

        }


        //Send email to Applying MGR ===================================================
        public async Task SendEmailToApplyingMGR(string section)
        {
            await Task.Delay(60000);  // Simulating a delay of 60 seconds

            this.section = section;

            //Select COPQ PIC
            await con.OpenAsync();

            try
            {
                
                SqlCommand SelectCOPQPIC = new SqlCommand("SP_SelectCOPQApproverEmail", con);
                SelectCOPQPIC.CommandType = CommandType.StoredProcedure;
                SelectCOPQPIC.Parameters.AddWithValue("@Procedure", "SelectMGREmail");
                SelectCOPQPIC.Parameters.AddWithValue("@Section", section);
                SqlDataAdapter sda = new SqlDataAdapter(SelectCOPQPIC);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    //List of COPQ PIC email
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());

                    foreach (DataRow row in dTable.Rows)
                    {
                        Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();


                        builder.Append("Dear MGR,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("This is to inform you that there are pending COPQ items requiring your approval.");
                        builder.Append("<br>");
                        builder.Append("Below is a summary of pending approvals:");
                        builder.Append("<br>");

                        //Insert Table ====================================================

                        string mailBody = "<table width='50%' cellpadding='0' cellspacing='0' style='border-collapse: collapse;'>";
                        mailBody += "<tr>";
                        mailBody += "<th style='border: 1px solid gray; padding:5px;'>Issuing Section</th>";
                        mailBody += "<th style='border: 1px solid gray; padding:5px;'>No. of pending approval</th>";
                        mailBody += "</tr>";


                        //Select no. of COPQ pending approval for COPQ PIC 
                        SqlCommand SelectCOPQPendingApproval = new SqlCommand("SP_SelectCOPQPendingApproval", con);
                        SelectCOPQPendingApproval.CommandType = CommandType.StoredProcedure;
                        SelectCOPQPendingApproval.Parameters.AddWithValue("@Procedure", "SelectForApprovalByMGR");
                        SelectCOPQPendingApproval.Parameters.AddWithValue("@Section", section);
                        SqlDataAdapter da = new SqlDataAdapter(SelectCOPQPendingApproval);
                        DataTable dt = new DataTable();
                        await Task.Run(() => da.Fill(dt));

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                string Section = dr["Section"].ToString();
                                int NoOfCOPQPendingApproval = Convert.ToInt32(dr["NoOfPending"]);

                                mailBody += $"<tr align='Center'>"
                                    + $"<td style='border: 1px solid gray; padding:5px;'>{Section}</td>"
                                    + $"<td style='border: 1px solid gray; padding:5px;'>{NoOfCOPQPendingApproval}</td>"
                                    + "</tr>";
                            }
                        }

                        mailBody += "</table>";

                        builder.Append("" + mailBody);

                        //================================================================

                        builder.Append("<br>");
                        builder.Append("<br>");
                        builder.Append("For your checking and approval, kindly open MHMS application.");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");

                        builder.Append("Thanks and Best Regards.");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");

                        innerString = builder.ToString();


                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            //MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", "arvin.caparros@brother-biph.com.ph");
                            //mail.Bcc.Add("dianelleyasdane.estacio@brother-biph.com.ph");
                            //mail.Bcc.Add("jeancy.dimayuga@brother-biph.com.ph");
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");

                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";

                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": MGR - COPQ Pending Approval as of " + DateTime.Now.ToString();
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;

                            // Send the email
                            await client.SendMailAsync(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error sending email: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing: {ex.Message}");
            }
            finally
            {
                con.Close();  // Ensure the connection is closed
            }

            
        }

        //Send email to Receiving COPQ PIC ===================================================
        public async Task SendEmailToReceivingCOPQPIC(string section)
        {
            await Task.Delay(60000);  // Simulating a delay of 60 seconds

            this.section = section;

            await con.OpenAsync();

            try
            {
                SqlCommand SelectCOPQPIC = new SqlCommand("SP_SelectCOPQApproverEmail", con);
                SelectCOPQPIC.CommandType = CommandType.StoredProcedure;
                SelectCOPQPIC.Parameters.AddWithValue("@Procedure", "SelectReceivingCOPQPICEmail");
                SelectCOPQPIC.Parameters.AddWithValue("@Section", section);
                SqlDataAdapter sda = new SqlDataAdapter(SelectCOPQPIC);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    //List of COPQ PIC email
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());

                    foreach (DataRow row in dTable.Rows)
                    {
                        Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();


                        builder.Append("Dear COPQ PIC,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("This is to inform you that there are pending COPQ items requiring your approval.");
                        builder.Append("<br>");
                        builder.Append("Below is a summary of the sections with pending approvals:");
                        builder.Append("<br>");

                        //Insert Table ====================================================

                        string mailBody = "<table width='50%' cellpadding='0' cellspacing='0' style='border-collapse: collapse;'>";
                        mailBody += "<tr>";
                        mailBody += "<th style='border: 1px solid gray; padding:5px;'>Issuing Section</th>";
                        mailBody += "<th style='border: 1px solid gray; padding:5px;'>No. of pending approval</th>";
                        mailBody += "</tr>";


                        //Select no. of COPQ pending approval for COPQ PIC 
                        SqlCommand SelectCOPQPendingApproval = new SqlCommand("SP_SelectCOPQPendingApproval", con);
                        SelectCOPQPendingApproval.CommandType = CommandType.StoredProcedure;
                        SelectCOPQPendingApproval.Parameters.AddWithValue("@Procedure", "SelectForApprovalByReceivingCOPQPIC");
                        SelectCOPQPendingApproval.Parameters.AddWithValue("@Section", section);
                        SqlDataAdapter da = new SqlDataAdapter(SelectCOPQPendingApproval);
                        DataTable dt = new DataTable();
                        await Task.Run(() => da.Fill(dt));


                        foreach (DataRow dr in dt.Rows)
                        {
                            string Section = dr["Section"].ToString();
                            int NoOfCOPQPendingApproval = Convert.ToInt32(dr["NoOfPending"]);

                            mailBody += $"<tr align='Center'>"
                                + $"<td style='border: 1px solid gray; padding:5px;'>{Section}</td>"
                                + $"<td style='border: 1px solid gray; padding:5px;'>{NoOfCOPQPendingApproval}</td>"
                                + "</tr>";
                        }


                        mailBody += "</table>";

                        builder.Append("" + mailBody);

                        //================================================================

                        builder.Append("<br>");
                        builder.Append("<br>");
                        builder.Append("For your checking and approval, kindly open MHMS application.");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");

                        builder.Append("Thanks and Best Regards.");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");

                        innerString = builder.ToString();


                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            //MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", "arvin.caparros@brother-biph.com.ph");
                            //mail.Bcc.Add("dianelleyasdane.estacio@brother-biph.com.ph");
                            //mail.Bcc.Add("jeancy.dimayuga@brother-biph.com.ph");
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": COPQ PIC Pending Approval as of " + DateTime.Now.ToString();
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;

                            await client.SendMailAsync(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error sending email: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                con.Close();  // Ensure the connection is closed
            }


        }


        //Send email to Receiving COPQ PIC ===================================================
        public async Task SendEmailToReceivingCOPQProcessInCharge(string section)
        {
            await Task.Delay(60000);  // Simulating a delay of 60 seconds

            this.section = section;

            await con.OpenAsync();

            try
            {
                SqlCommand SelectCOPQPIC = new SqlCommand("SP_SelectCOPQApproverEmail", con);
                SelectCOPQPIC.CommandType = CommandType.StoredProcedure;
                SelectCOPQPIC.Parameters.AddWithValue("@Procedure", "SelectReceivingCOPQProcessInchargeEmail");
                SelectCOPQPIC.Parameters.AddWithValue("@Section", section);

                SqlDataAdapter sda = new SqlDataAdapter(SelectCOPQPIC);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    //List of COPQ PIC email
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());

                    foreach (DataRow row in dTable.Rows)
                    {
                        Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();


                        builder.Append("Dear COPQ Process In-charge,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("This is to inform you that there are pending COPQ items requiring your approval.");
                        builder.Append("<br>");
                        builder.Append("Below is a summary of the sections with pending approvals:");
                        builder.Append("<br>");

                        //Insert Table ====================================================
                        // Insert table with pending approval info
                        string mailBody = "<table width='50%' cellpadding='0' cellspacing='0' style='border-collapse: collapse;'>";
                        mailBody += "<tr>";
                        mailBody += "<th style='border: 1px solid gray; padding:5px;'>Issuing Section</th>";
                        mailBody += "<th style='border: 1px solid gray; padding:5px;'>No. of pending approval</th>";
                        mailBody += "</tr>";


                        //Select no. of COPQ pending approval for COPQ PIC 
                        SqlCommand SelectCOPQPendingApproval = new SqlCommand("SP_SelectCOPQPendingApproval", con);
                        SelectCOPQPendingApproval.CommandType = CommandType.StoredProcedure;
                        SelectCOPQPendingApproval.Parameters.AddWithValue("@Procedure", "SelectForApprovalByReceivingProcessIncharge");
                        SelectCOPQPendingApproval.Parameters.AddWithValue("@Section", section);

                        SqlDataAdapter da = new SqlDataAdapter(SelectCOPQPendingApproval);
                        DataTable dt = new DataTable();
                        await Task.Run(() => da.Fill(dt));


                        foreach (DataRow dr in dt.Rows)
                        {
                            string Section = dr["Section"].ToString();
                            int NoOfCOPQPendingApproval = Convert.ToInt32(dr["NoOfPending"]);

                            mailBody += $"<tr align='Center'>"
                                + $"<td style='border: 1px solid gray; padding:5px;'>{Section}</td>"
                                + $"<td style='border: 1px solid gray; padding:5px;'>{NoOfCOPQPendingApproval}</td>"
                                + "</tr>";
                        }


                        mailBody += "</table>";

                        builder.Append("" + mailBody);

                        //================================================================

                        builder.Append("<br>");
                        builder.Append("<br>");
                        builder.Append("For your checking and approval, kindly open MHMS application.");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");

                        builder.Append("Thanks and Best Regards.");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");

                        innerString = builder.ToString();


                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            //MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", "arvin.caparros@brother-biph.com.ph");
                            //mail.Bcc.Add("dianelleyasdane.estacio@brother-biph.com.ph");
                            //mail.Bcc.Add("jeancy.dimayuga@brother-biph.com.ph");
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");

                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";

                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": COPQ Process In-charge Pending Approval as of " + DateTime.Now.ToString();
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;

                            // Send the email
                            await client.SendMailAsync(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error sending email: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                con.Close();  // Ensure the connection is closed
            }

        }

        //Send email to Receiving SPV ===================================================
        public async Task SendEmailToReceivingSPV(string section)
        {
            await Task.Delay(60000);  // Simulating a delay of 60 seconds

            this.section = section;

            await con.OpenAsync();

            try
            {
                SqlCommand SelectCOPQPIC = new SqlCommand("SP_SelectCOPQApproverEmail", con);
                SelectCOPQPIC.CommandType = CommandType.StoredProcedure;
                SelectCOPQPIC.Parameters.AddWithValue("@Procedure", "SelectReceivingSPVEmail");
                SelectCOPQPIC.Parameters.AddWithValue("@Section", section);
                SqlDataAdapter sda = new SqlDataAdapter(SelectCOPQPIC);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
              

                if (dTable.Rows.Count > 0)
                {
                    //List of COPQ PIC email
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());

                    foreach (DataRow row in dTable.Rows)
                    {
                        Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();


                        builder.Append("Dear SPV's,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("This is to inform you that there are pending COPQ items requiring your approval.");
                        builder.Append("<br>");
                        builder.Append("Below is a summary of the sections with pending approvals:");
                        builder.Append("<br>");

                        //Insert Table ====================================================

                        string mailBody = "<table width='50%' cellpadding='0' cellspacing='0' style='border-collapse: collapse;'>";
                        mailBody += "<tr>";
                        mailBody += "<th style='border: 1px solid gray; padding:5px;'>Section</th>";
                        mailBody += "<th style='border: 1px solid gray; padding:5px;'>No. of pending approval</th>";
                        mailBody += "</tr>";



                        //Select no. of COPQ pending approval for COPQ PIC 
                        SqlCommand SelectCOPQPendingApproval = new SqlCommand("SP_SelectCOPQPendingApproval", con);
                        SelectCOPQPendingApproval.CommandType = CommandType.StoredProcedure;
                        SelectCOPQPendingApproval.Parameters.AddWithValue("@Procedure", "SelectForApprovalByReceivingSPV");
                        SelectCOPQPendingApproval.Parameters.AddWithValue("@Section", section);
                        SqlDataAdapter da = new SqlDataAdapter(SelectCOPQPendingApproval);
                        DataTable dt = new DataTable();
                        await Task.Run(() => da.Fill(dt));


                        foreach (DataRow dr in dt.Rows)
                        {
                            string Section = dr["Section"].ToString();
                            int NoOfCOPQPendingApproval = Convert.ToInt32(dr["NoOfPending"]);

                            mailBody += $"<tr align='Center'>"
                                + $"<td style='border: 1px solid gray; padding:5px;'>{Section}</td>"
                                + $"<td style='border: 1px solid gray; padding:5px;'>{NoOfCOPQPendingApproval}</td>"
                                + "</tr>";
                        }

                        mailBody += "</table>";

                        builder.Append("" + mailBody);

                        //================================================================

                        builder.Append("<br>");
                        builder.Append("<br>");
                        builder.Append("For your checking and approval, kindly open MHMS application.");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");

                        builder.Append("Thanks and Best Regards.");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");

                        innerString = builder.ToString();


                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            //MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", "arvin.caparros@brother-biph.com.ph");
                            //mail.Bcc.Add("dianelleyasdane.estacio@brother-biph.com.ph");
                            //mail.Bcc.Add("jeancy.dimayuga@brother-biph.com.ph");
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                           
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                           
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": Receiving SPV - COPQ Pending Approval as of " + DateTime.Now.ToString();
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;
                            await client.SendMailAsync(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error sending email: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                con.Close();  // Ensure the connection is closed
            }

        }

        //Send email to Receiving MGR ===================================================
        public async Task SendEmailToReceivingMGR(string section)
        {
            await Task.Delay(60000);  // Simulating a delay of 60 seconds

            this.section = section;


            await con.OpenAsync();

            try
            {
                SqlCommand SelectCOPQPIC = new SqlCommand("SP_SelectCOPQApproverEmail", con);
                SelectCOPQPIC.CommandType = CommandType.StoredProcedure;
                SelectCOPQPIC.Parameters.AddWithValue("@Procedure", "SelectReceivingMGREmail");
                SelectCOPQPIC.Parameters.AddWithValue("@Section", section);
                SqlDataAdapter sda = new SqlDataAdapter(SelectCOPQPIC);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
               

                if (dTable.Rows.Count > 0)
                {
                    //List of COPQ PIC email
                    string EmailListTo = String.Join(", ", dTable.AsEnumerable().Select(row => row["Email"]).ToArray());

                    foreach (DataRow row in dTable.Rows)
                    {
                        Email = row["Email"].ToString();

                        //Email body start ====>>>
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine();


                        builder.Append("Dear MGR,");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Good day!");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("This is to inform you that there are pending COPQ items requiring your approval.");
                        builder.Append("<br>");
                        builder.Append("Below is a summary of the sections with pending approvals:");
                        builder.Append("<br>");

                        //Insert Table ====================================================

                        string mailBody = "<table width='50%' cellpadding='0' cellspacing='0' style='border-collapse: collapse;'>";
                        mailBody += "<tr>";
                        mailBody += "<th style='border: 1px solid gray; padding:5px;'>Issuing Section</th>";
                        mailBody += "<th style='border: 1px solid gray; padding:5px;'>No. of pending approval</th>";
                        mailBody += "</tr>";


                        //Select no. of COPQ pending approval for COPQ PIC 
                        SqlCommand SelectCOPQPendingApproval = new SqlCommand("SP_SelectCOPQPendingApproval", con);
                        SelectCOPQPendingApproval.CommandType = CommandType.StoredProcedure;
                        SelectCOPQPendingApproval.Parameters.AddWithValue("@Procedure", "SelectForApprovalByReceivingMGR");
                        SelectCOPQPendingApproval.Parameters.AddWithValue("@Section", section);

                        SqlDataAdapter da = new SqlDataAdapter(SelectCOPQPendingApproval);
                        DataTable dt = new DataTable();
                        await Task.Run(() => da.Fill(dt));

                        foreach (DataRow dr in dt.Rows)
                        {
                            string Section = dr["Section"].ToString();
                            int NoOfCOPQPendingApproval = Convert.ToInt32(dr["NoOfPending"]);

                            mailBody += $"<tr align='Center'>"
                                + $"<td style='border: 1px solid gray; padding:5px;'>{Section}</td>"
                                + $"<td style='border: 1px solid gray; padding:5px;'>{NoOfCOPQPendingApproval}</td>"
                                + "</tr>";
                        }

                        mailBody += "</table>";

                        builder.Append("" + mailBody);

                        //================================================================

                        builder.Append("<br>");
                        builder.Append("<br>");
                        builder.Append("For your checking and approval, kindly open MHMS application.");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("Link (リンク)：");
                        builder.Append("<br>");

                        builder.Append("<a href= " + @"\\apbiphsh07\D0_ShareBrotherGroup\19_BPS\17_Installer\MHMS\setup.exe>" + @"MHMS Application" + "</a>");

                        builder.Append("<br><br>");

                        builder.Append("Thanks and Best Regards.");
                        builder.Append("<br>");
                        builder.Append("<br>");

                        builder.Append("<b>[This is an automatic generated e-mail]</b>");

                        innerString = builder.ToString();


                        try
                        {
                            string CurrentYear = DateTime.Now.ToString("yyyy");
                            string CurrentDay = DateTime.Now.ToString("MM/dd/yy");

                            MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", EmailListTo);
                            //MailMessage mail = new MailMessage("mhms@brother-biph.com.ph", "arvin.caparros@brother-biph.com.ph");
                            //mail.Bcc.Add("dianelleyasdane.estacio@brother-biph.com.ph");
                            //mail.Bcc.Add("jeancy.dimayuga@brother-biph.com.ph");
                            mail.Bcc.Add("arvin.caparros@brother-biph.com.ph");
                            
                            SmtpClient client = new SmtpClient();
                            client.Port = 25;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = "10.113.10.1";
                            
                            mail.Subject = "[BIPH_MHMS] " + "FY." + CurrentYear + ": Receiving MGR - COPQ Pending Approval as of" + DateTime.Now.ToString();
                            mail.Priority = MailPriority.High;
                            mail.Body = innerString;
                            mail.IsBodyHtml = true;

                            await client.SendMailAsync(mail);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error sending email: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                con.Close();  // Ensure the connection is closed
            }
        }
    }
}
