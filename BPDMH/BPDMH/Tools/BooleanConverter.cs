namespace BPDMH.Tools
{
    public class BooleanConverter
    {
        public static string ConvertBoolToYesNo(bool b)
        {
            if (b) { return "Pengirim"; }

            return "Penerima";
        } 
    }
}