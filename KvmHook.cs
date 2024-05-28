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
        public const string Rabot = " - выполнена перезагрузка ПО КВФ, работоспособность комплекса восстановлена.";
        public const string RabotVideo = " - выполнена перезагрузка ПО КВФ, работоспособность видеопотока восстановлена.";
        public const string IssMKL = " - направлена заявка подрядной организации (МКЛ)";
        public const string IssNetline = " - направлена заявка подрядной организации (Нетлайн)";
        public const string IssRCR = " - направлена АВР бригада РЦР";
        public const string IssRCR1 = " - отсутствие фиксаций и видеопотока";
        public const string Podr3 = "Группа сервиса РИЦ / 03 (ООО «РИЦ»)";
        public const string Podr4 = "Мониторинг ФВФ 1 линия / 03 (ООО «РИЦ»)";

        public static string WhoToDo(int theme, int podr)
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
                        case 2:
                            return IssRCR + Diag;
                        case 3:
                            return Rabot;
                    }
                    break;
                case 2:
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
                        case 2:
                            return IssRCR + Diag;
                        case 3:
                            return Rabot;
                    }
                    break;
                case 3:
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
                        case 2:
                            return IssRCR + Diag;
                        case 3:
                            return RabotVideo;
                    }
                    break;
                case 4:
                    switch (podr)
                    {
                        case 1:
                            if (MainForm.DeviceChosen.PodrOrg1Common == "МКЛ")
                            {
                                return IssMKL + Ochist;
                            }
                            else
                            {
                                return IssNetline + Ochist;
                            }
                        case 2:
                            return IssRCR + Ochist;
                        case 3:
                            return "ДИСПЕТЧЕР ПОЕДЕТ ЧИСТИТЬ ЧТО ЛИ?";
                    }
                    break;
                case 5:
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
                        case 2:
                            return IssRCR + Diag;
                        case 3:
                            return Rabot;
                    }
                    break;
                case 6:
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
                        case 2:
                            return IssRCR + Diag;
                        case 3:
                            return Rabot;
                    }
                    break;
                default:
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
                        case 2:
                            return IssRCR + Diag;
                        case 3:
                            return Rabot;
                    }
                    break;
            }
            return "";
        }

        public static void IssueToWeb(int theme, int podr)
        {
            Issue issue = new Issue
            {
                KE = MainForm.DeviceChosen.KvfNumber
            };
            switch (theme)
            {
                case 1:
                    issue.Theme = issue.KE + Tema1;
                    issue.Problem = Problem1;
                    break;
                case 2:
                    issue.Theme = issue.KE + Tema2;
                    issue.Problem = Problem2;
                    break;
                case 3:
                    issue.Theme = issue.KE + Tema3;
                    issue.Problem = Problem3;
                    break;
                case 4:
                    issue.Theme = issue.KE + Tema4;
                    issue.Problem = Problem4;
                    break;
                case 5:
                    issue.Theme = issue.KE + Tema5;
                    issue.Problem = Problem5;
                    break;
                case 6:
                    issue.Theme = issue.KE + Tema6;
                    issue.Problem = Problem6;
                    break;
                default:
                    issue.Theme = issue.KE + Tema0;
                    issue.Problem = Problem0;
                    break;
            }
            issue.Owner = MainForm.DeviceChosen.OrgOwner;
            issue.GK = MainForm.DeviceChosen.GK;
            issue.Org = MainForm.DeviceChosen.PodrOrg1;
            issue.Who = WhoToDo(theme, podr);
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
            int slpTime = 250;
            SendKeys.Send("+{TAB}");
            SendKeys.Send("+{TAB}");
            SendKeys.Send("{DOWN}");
            SendKeys.Send("{UP}");
            SendKeys.Send("{UP}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("{ENTER}");
            SendKeys.Send("{TAB}");
            Clipboard.SetData(DataFormats.Text, (Object)issue.KE);
            SendKeys.Send("+{INSERT}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("{ENTER}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("{TAB}");
            Clipboard.SetData(DataFormats.Text, (Object)issue.Theme);
            SendKeys.Send("+{INSERT}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("{TAB}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("{PGUP}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("+{PGDN}");
            Clipboard.SetData(DataFormats.Text, (Object)issue.Descr);
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("+{INSERT}");
            SendKeys.Send("{TAB}");
            System.Threading.Thread.Sleep(slpTime);
            Clipboard.SetData(DataFormats.Text, (Object)issue.Owner);
            SendKeys.Send("+{INSERT}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("{TAB}");
            System.Threading.Thread.Sleep(slpTime);
            Clipboard.SetData(DataFormats.Text, (Object)issue.GK);
            SendKeys.Send("+{INSERT}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("{TAB}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("+{TAB}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("{TAB}");
            Clipboard.SetData(DataFormats.Text, (Object)issue.Org);
            SendKeys.Send("+{INSERT}");
            System.Threading.Thread.Sleep(slpTime);
            SendKeys.Send("{TAB}");
        }

    }
}
