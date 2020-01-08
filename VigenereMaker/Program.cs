using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereMaker
{
    class Program
    {
        static bool inverted_vignere = false;

        static void Main(string[] args)
        {
             var pattern0 = "<TextBlock Grid.Row=\"{ROW}\" Grid.Column=\"{COLUMN}\" Style=\"{StaticResource SquareCell}\">" + 
                "<TextBlock.Resources>" +
                    "<sys:Int32 x:Key=\"CurrentRow\">{ROW}</sys:Int32>" +
                    "<sys:Int32 x:Key=\"CurrentCol\">{COLUMN}</sys:Int32>" + 
                "</TextBlock.Resources>" + 
                "<TextBlock.Text>" + 
                    "<MultiBinding Converter=\"{StaticResource MatrixCell}\">" +
                        "<Binding Path=\"VigenereMatrix\"></Binding>" +
                        "<Binding Source=\"{StaticResource CurrentRow}\" ></Binding>" +
                        "<Binding Source=\"{StaticResource CurrentCol}\" ></Binding>" +
                   "</MultiBinding>"
                + "</TextBlock.Text>"
                + "<TextBlock.Foreground>"
                    + "<MultiBinding Converter=\"{StaticResource mcfg}\">"
                        +"<Binding Source=\"{StaticResource CurrentRow}\" ></Binding>"
                        +"<Binding Source=\"{StaticResource CurrentCol}\" ></Binding>"
                        + "<Binding Path=\"MatrixSelectedRow\"></Binding>"
                        + "<Binding Path=\"MatrixSelectedColumn\"></Binding>"
                    + "</MultiBinding>"
                + "</TextBlock.Foreground>"
            + "</TextBlock>";

            var sb = new System.Text.StringBuilder();
            for(var r = 0; r < 26; r++)
            {
                for(var c = 0; c < 26; c++)
                {
                    var line = pattern0.Replace("{ROW}", r.ToString()).Replace("{COLUMN}", c.ToString());
                    sb.AppendLine(line);
                }
            }
            File.WriteAllText(@"c:\temp\generated-markup_final_0.txt", sb.ToString());

        }

        static void Task1_Main(string[] args)
        {
            var pattern = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (args != null && args.Length > 0)
            {
                pattern = args[0].ToUpper();
                ValidatePattern(pattern);
            }
            GenerateVigenereSquare(pattern);
        }

        private static void ValidatePattern(string pattern)
        {
            // coming soon.
        }

        private static void GenerateVigenereSquare(string pattern)
        {
            var output_dir = @"c:\users\glenn\desktop\vig\";
            if (!Directory.Exists(output_dir))
            {
                Directory.CreateDirectory(output_dir);
            }
            var filename = string.Format("keyfile-{0:yyyyMMddHHmmsstt}.txt", DateTime.Now);

            var path = Path.Combine(output_dir, filename);
            var lines = new List<string>();

            lines.Add(pattern.Fluff());

            if (inverted_vignere)
            {
                for (var offset = 1; offset < pattern.Length; offset++)
                {
                    var chunk = pattern.Substring(0, offset);
                    var otherpart = pattern.Remove(0, offset);
                    var newpattern = otherpart + chunk;
                    lines.Add(newpattern.Fluff());
                }
            } 
            else
            {
                for (var offset = 1; offset < pattern.Length; offset++)
                {
                    var chunk = pattern.Substring(0, pattern.Length - offset);
                    var otherpart = pattern.Remove(0, pattern.Length - offset);
                    var newpattern = otherpart + chunk;
                    lines.Add(newpattern.Fluff());
                }
            }

            File.WriteAllLines(path, lines.ToArray());
        }
    }

    public static class StringExtensions
    {
        // add spaces between each letter.
        public static string Fluff(this string target)
        {
            var output = new string(target.Select(c => string.Format("{0} ", c)).Select(s => s.ToCharArray()).SelectMany(w => w).ToArray());
            return " " + output;
        }
    }
}
