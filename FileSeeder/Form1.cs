using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Security.Cryptography;

//Turn a file into an IOTA seed.
namespace FileSeeder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            //Get FilePath from Drag&Drop
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            byte[] fileBytes = File.ReadAllBytes(FileList[0]);

            //Get SHA512 from file
            string FileHash = Convert.ToBase64String(GenerateSHA512(fileBytes));
            lblHash.Text = FileHash;

            //Create Seed from file
            lblSeed.Text = GenerateSeed(fileBytes);
        }

        //Generate SHA512 from file
        public static byte[] GenerateSHA512(byte[] input)
        {
            using (SHA512 sha512 = SHA512Managed.Create())
            {
                byte[] hash = sha512.ComputeHash(input);
                return hash;
            }
        }

        //This function is a first approach to generate a seed from a file.
        //This could be done alot better, but I have no clue how to do this yet :)
        //Suggestions from the community are absolutely welcome.
        public static string GenerateSeed(byte[] input)
        {
            //GetSHA512 Hash of InputFile
            byte[] inputFileHash = GenerateSHA512(input);

            //Convert Hash to base64 String
            string b64Hash = Convert.ToBase64String(inputFileHash);

            //Strip Hash from non seed chars
            string seedChunk = StripStringFromInvalidChars(b64Hash);

            //Rehash original hash until enough valid seed characters are gathered and a seed can be formed from it.
            while (seedChunk.Length <= 81)
            {
                //Rehash previous hash and add it to the chunk 
                inputFileHash = GenerateSHA512(inputFileHash);
                b64Hash = Convert.ToBase64String(inputFileHash);
                seedChunk += StripStringFromInvalidChars(b64Hash);
            }
            //Return the seed
            return seedChunk.Substring(0, 81);
        }


        //Remove all chars from the input that are not allowed in a seed.
        public static string StripStringFromInvalidChars(string input)
        {
            var allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ9";
            return new string(input.Where(c => allowedChars.Contains(c)).ToArray());
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //Copy seed to clipboard.
        private void lblSeed_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblSeed.Text);
        }
    }
}
