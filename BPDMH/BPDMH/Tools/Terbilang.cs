using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPDMH.Tools
{
    public class Terbilang
    {
//        http://davidnakoko.com/2013/09/c-fungsi-terbilang-yang-ada-minus-dan-koma/
        private double input = 0;
        private string output = "";

        public Terbilang()
        {

        }

        public Terbilang(long angka)
        {
            try
            {
                input = (double)angka;
                Proses();
            }
            catch
            {
                output = "";
            }
        }

        public Terbilang(int angka)
        {
            try
            {
                input = (double)angka;
                Proses();
            }
            catch
            {
                output = "";
            }
        }

        public Terbilang(string angka)
        {
            try
            {
                input = Convert.ToDouble(angka);
                Proses();
            }
            catch
            {
                output = "";
            }
//            finally
//            {
//                output = "";
//            }
        }

        public void SetAngka(double angka)
        {
            try
            {
                input = angka;
                Proses();
            }
            catch
            {
                output = "";
            }
        }

        private void Proses()
        {
            output = TerbilangLong2(input);
        }

        public string Hasil()
        {
            return output;
        }

        private string TerbilangLong2(double amount)
        {
            string word = "";
            double divisor = 1000000000000.00; double large_amount = 0;
            double tiny_amount = 0;
            double dividen = 0; double dummy = 0;
            string weight1 = ""; string unit = ""; string follower = "";
            string[] prefix = { "SE", "DUA ", "TIGA ", "EMPAT ", "LIMA ", "ENAM ", "TUJUH ", "DELAPAN ", "SEMBILAN " };
            string[] sufix = { "SATU ", "DUA ", "TIGA ", "EMPAT ", "LIMA ", "ENAM ", "TUJUH ", "DELAPAN ", "SEMBILAN " };
            large_amount = Math.Abs(Math.Truncate(amount));
            tiny_amount = Math.Round((Math.Abs(amount) - large_amount) * 100);
            if (large_amount > divisor)
                return "Melebihi Batas";
            while (divisor >= 1)
            {
                dividen = Math.Truncate(large_amount / divisor);
                large_amount = large_amount % divisor;
                unit = "";
                if (dividen > 0)
                {
                    if (divisor == 1000000000000.00)
                        unit = "TRILYUN ";
                    else
                        if (divisor == 1000000000.00)
                            unit = "MILYAR ";
                        else
                            if (divisor == 1000000.00)
                                unit = "JUTA ";
                            else
                                if (divisor == 1000.00)
                                    unit = "RIBU ";
                }
                weight1 = "";
                dummy = dividen;
                if (dummy >= 100)
                    weight1 = prefix[(int)Math.Truncate(dummy / 100) - 1] + "RATUS ";
                dummy = dividen % 100;
                if (dummy < 10)
                {
                    if (dummy == 1 && unit == "RIBU ")
                        weight1 += "SE";
                    else
                        if (dummy > 0)
                            weight1 += sufix[(int)dummy - 1];
                }
                else
                    if (dummy >= 11 && dummy <= 19)
                    {
                        weight1 += prefix[(int)(dummy % 10) - 1] + "BELAS ";
                    }
                    else
                    {
                        weight1 += prefix[(int)Math.Truncate(dummy / 10) - 1] + "PULUH ";
                        if (dummy % 10 > 0)
                            weight1 += sufix[(int)(dummy % 10) - 1];
                    }
                word += weight1 + unit;
                divisor /= 1000.00;
            }
            if (Math.Truncate(amount) == 0)
                word = "NOL ";
            follower = "";
            if (tiny_amount < 10)
            {
                if (tiny_amount > 0)
                    follower = "KOMA NOL " + sufix[(int)tiny_amount - 1];
            }
            else
            {
                follower = "KOMA " + sufix[(int)Math.Truncate(tiny_amount / 10) - 1];
                if (tiny_amount % 10 > 0)
                    follower += sufix[(int)(tiny_amount % 10) - 1];
            }
            word += follower;
            if (amount < 0)
            {
                word = "MINUS " + word + " RUPIAH";
            }
            else
            {
                word = word + " RUPIAH";
            }
            return word.Trim();
        }
    }
}
