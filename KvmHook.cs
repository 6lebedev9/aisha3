using aisha3;
using Azure.Identity;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace aisha3
{
    public class KvmHook
    {
        public const string RTK = "02 (ПАО «Ростелеком»)";
        public const string Tema0 = " - комплекс недоступен";
        public const string Problem0 = " - комплекс недоступен;";
        public const string Tema1 = " - отсутствие фиксаций";
        public const string Problem1 = " - зафиксировано отсутствие фиксаций;";
        public const string Tema2 = " - отсутствие видеопотока";
        public const string Problem2 = " - зафиксировано отсутствие видеопотока;";
        public const string Tema3 = " - нестабильный видеопоток";
        public const string Problem3 = " - зафиксирован нестабильный видеопоток;";
        public const string Tema4 = " - очистка оптики";
        public const string Problem4 = " - зафиксирована необходимость очистки оптики;";
        public const string Tema5 = " - потеря сигнала геолокации";
        public const string Problem5 = " - зафиксирована потеря сигнала геолокации;";
        public const string Tema6 = " - ошибка ПО";
        public const string Problem6 = " - зафиксирована ошибка ПО КВФ;";
        public const string Diag = " на проведение диагностики;";
        public const string Ochist = " на проведение очистки оптики КВФ;";
        public const string Rabot = " работоспособность комплекса восстановлена.";
        public const string IssMKL = " - направлена заявка подрядной организации (МКЛ)";
        public const string IssNetline = " - направлена заявка подрядной организации (Нетлайн)";
        public const string IssRCR = " - направлена АВР бригада РЦР";
        public const string IssRCR1 = " - отсутствие фиксаций и видеопотока";
        public const string Podr3 = "Группа сервиса РИЦ / 03 (ООО «РИЦ»)";
        public const string Podr4 = "Мониторинг ФВФ 1 линия / 03 (ООО «РИЦ»)";

        public string WhoToDo(int theme, int podr)
        {
            switch (theme)
            {
                case 1:
                    switch (podr)
                    {
                        case 1:
                            if (MainForm.DeviceChosen.PodrOrg1Common == "МКЛ")
                            {
                                return IssMKL + Diag;
                            }
                            else
                            {
                                return IssNetline + Diag;
                            }
                            break;
                        case 2:
                            return IssRCR + Diag;
                            break;
                        case 3:
                            break;
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                default:
                    break;
            }
            return "";
        }

        private static void IssueToWeb(int theme, int podr)
        {
            Issue issue = new Issue();
            issue.KE = MainForm.DeviceChosen.KvfNumber;
            switch (theme)
            {
                case 1:
                    issue.Theme = Tema1;
                    issue.Problem = Problem1;
                    break;
                case 2:
                    issue.Theme = Tema2;
                    issue.Problem = Problem2;
                    break;
                case 3:
                    issue.Theme = Tema3;
                    issue.Problem = Problem3;
                    break;
                case 4:
                    issue.Theme = Tema4;
                    issue.Problem = Problem4;
                    break;
                case 5:
                    issue.Theme = Tema5;
                    issue.Problem = Problem5;
                    break;
                case 6:
                    issue.Theme = Tema6;
                    issue.Problem = Problem6;
                    break;
                default:
                    issue.Theme = Tema0;
                    issue.Problem = Problem0;
                    break;
            }
            issue.Owner = MainForm.DeviceChosen.OrgOwner;
            issue.GK = MainForm.DeviceChosen.GK;
            issue.Org = MainForm.DeviceChosen.PodrOrg1;
            DateTime todaydate = DateTime.Now;
            DateTime todayTimeMinus15 = todaydate.Add(new TimeSpan(0, -15, 0));
            string todayDate = todaydate.ToString("dd.MM.yy");
            string todayTime = todaydate.ToString("HH:mm");
            string todayTimeMinus = todayTimeMinus15.ToString("HH:mm");
            issue.Descr = MainForm.DeviceChosen.AddressRGIS + " - " + MainForm.DeviceChosen.Gps + " - " + MainForm.DeviceChosen.DeviceIP + 
                "\r\n" + todayTimeMinus + " " + todayDate + issue.Problem + 
                "\r\n" + todayTime + " " + todayDate + issue.Who;

            Process[] procs = Process.GetProcessesByName("Chrome");
            foreach (Process p in procs)
            {
                Program.ShowWindow(p.MainWindowHandle, 3);
                Program.SetForegroundWindow(p.MainWindowHandle);
            }
            int slpTime = 350;
            SendKeys.Send("+{TAB}");
            SendKeys.Send("+{TAB}");
            SendKeys.Send("{DOWN}");
            SendKeys.Send("{UP}");
            SendKeys.Send("{UP}");
            SendKeys.Send("{ENTER}");
            SendKeys.Send("{TAB}");
            Clipboard.SetData(DataFormats.Text, (Object)KE);
            SendKeys.Send("+{INSERT}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("{ENTER}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("{TAB}");
            Clipboard.SetData(DataFormats.Text, (Object)thema);
            SendKeys.Send("+{INSERT}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("{TAB}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("{PGUP}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("+{PGDN}");
            Clipboard.SetData(DataFormats.Text, (Object)opisanie);
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("+{INSERT}");
            SendKeys.Send("{TAB}");
            System.Threading.Thread.Sleep(slpTime);
            Clipboard.SetData(DataFormats.Text, (Object)expOrg);
            SendKeys.Send("+{INSERT}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("{TAB}");
            System.Threading.Thread.Sleep(slpTime);
            Clipboard.SetData(DataFormats.Text, (Object)kontrakt);
            SendKeys.Send("+{INSERT}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("{TAB}");
            SendKeys.Send("{END}");
            SendKeys.Send("+{HOME}");
            System.Threading.Thread.Sleep(slpTime);
            Clipboard.SetData(DataFormats.Text, (Object)ispOrg);
            SendKeys.Send("+{INSERT}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("{TAB}");
        }

        public static void Issue(int podryad, int tema)
        {
            switch (tema)
            {
                case 1:
                    switch (podryad)
                    {
                        case 1:
                            IssueToWeb(podr1, tema1);
                            break;
                        case 2:
                            IssueToWeb(podr2, tema1);
                            break;
                        case 3:
                            IssueToWeb(podr3, tema1);
                            break;
                        case 4:
                            IssueToWeb(podr4, tema1);
                            break;
                    }
                    break;
                case 2:
                    switch (podryad)
                    {
                        case 1:
                            IssueToWeb(podr1, tema2);
                            break;
                        case 2:
                            IssueToWeb(podr2, tema2);
                            break;
                        case 3:
                            IssueToWeb(podr3, tema2);
                            break;
                        case 4:
                            IssueToWeb(podr4, tema2);
                            break;
                    }
                    break;
                case 3:
                    switch (podryad)
                    {
                        case 1:
                            IssueToWeb(podr1, tema3);
                            break;
                        case 2:
                            IssueToWeb(podr2, tema3);
                            break;
                        case 3:
                            IssueToWeb(podr3, tema3);
                            break;
                        case 4:
                            IssueToWeb(podr4, tema3);
                            break;
                    }
                    break;
                case 4:
                    switch (podryad)
                    {
                        case 1:
                            IssueToWeb(podr1, tema4);
                            break;
                        case 2:
                            IssueToWeb(podr2, tema4);
                            break;
                        case 3:
                            IssueToWeb(podr3, tema4);
                            break;
                        case 4:
                            IssueToWeb(podr4, tema4);
                            break;
                    }
                    break;
                case 5:
                    switch (podryad)
                    {
                        case 1:
                            IssueToWeb(podr1, tema5);
                            break;
                        case 2:
                            IssueToWeb(podr2, tema5);
                            break;
                        case 3:
                            IssueToWeb(podr3, tema5);
                            break;
                        case 4:
                            IssueToWeb(podr4, tema5);
                            break;
                    }
                    break;
                case 6:
                    switch (podryad)
                    {
                        case 1:
                            IssueToWeb(podr1, tema6);
                            break;
                        case 2:
                            IssueToWeb(podr2, tema6);
                            break;
                        case 3:
                            IssueToWeb(podr3, tema6);
                            break;
                        case 4:
                            IssueToWeb(podr4, tema6);
                            break;
                    }
                    break;
                default:
                    switch (podryad)
                    {
                        case 1:
                            IssueToWeb(podr1, Tema0);
                            break;
                        case 2:
                            IssueToWeb(podr2, Tema0);
                            break;
                        case 3:
                            IssueToWeb(podr3, Tema0);
                            break;
                        case 4:
                            IssueToWeb(podr4, tema2);
                            break;
                    }
                    break;
            }
        }

    }
}
