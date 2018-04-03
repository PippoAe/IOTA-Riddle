# IOTA-Riddle     
Create an IOTA seed from an input file.       
This will be used for small riddles that, if solved correctly, reveal the seed to a reward.        

How the seed from the input file is generated using SHA512 Hashing:        

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
