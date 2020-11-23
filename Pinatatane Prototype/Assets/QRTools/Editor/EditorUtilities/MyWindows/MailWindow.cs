using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace GameplayFramework.Editor
{
    public class MailWindow : WindowTemplate<MailWindow>
    {
        public string From = "q.roussel@rubika-edu.com";
        public string To = "";
        public string Object = "";
        public string Message = "";
        public string PassWord = "";

        [MenuItem("Tools/QRTools/Mail Window")]
        public static void OpenWindow()
        {
            Open();
        }

        public override void Init()
        {
            base.Init();

            btns.AddBtn("Quentin", delegate { To = "q.roussel@rubika-edu.com"; });
            btns.AddBtn("Charlotte", delegate { To = "c.fargier@rubika-edu.com"; });
        }

        public override void BodyContent()
        {
            base.BodyContent();

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("From:", GUILayout.MaxWidth(75));
            From = EditorGUILayout.TextField(From);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("To:", GUILayout.MaxWidth(75));
            To = EditorGUILayout.TextField(To);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("Object:", GUILayout.MaxWidth(75));
            Object = EditorGUILayout.TextField(Object);
            EditorGUILayout.EndHorizontal();

            Message = EditorGUILayout.TextArea(Message, GUILayout.MaxHeight(75));

            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("PassWord:", GUILayout.MaxWidth(75));
            PassWord = EditorGUILayout.PasswordField(PassWord);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Send"))
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(string.Format(From));
                mail.To.Add(string.Format(To));
                mail.Subject = Object;
                mail.Body = Message;

                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential(string.Format(From), string.Format(PassWord)) as ICredentialsByHost;
                smtpServer.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { 
                    return true; 
                };
                smtpServer.Send(mail);
            }
        }
    }
}