# IOTA-Riddle     
Create an IOTA seed from an input file.       
This will be used for small riddles that, if solved correctly, reveal the seed to a reward when passed trough this function.     

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


How to solve a riddle:
1. Do whatever the file tells you to do.    
2. If you think you got it right, pass the file trough the above function. (or use the .exe here https://github.com/PippoAe/IOTA-Riddle/releases)      
3. A seed will be generated (clicking on the seed copies it to the clipboard).      
4. Log into the seed using the IOTA wallet.    
5. If you solved the riddle correctly, you should now see the reward that you can claim by sending it to an address of yours.
6. Bonus points: If it was a fun riddle and you want other people to solve it too, you can make the reward bigger by sending additional IOTA the address.    


How to make a riddle:
1. Create a new file (for example .txt, .png or whatever tickles your fancy)    
2. Fill the file with a riddle and drop the solved riddle into the above function to have a seed generated from the file.
(or use the .exe located on the release page, clicking on the seed copies it to the clipboard).        
3. Do a login on the iota wallet with the generated seed and generate a receiving address.       
4. Send an awesome reward to the receiving-address as a price for the future solver to claim.
5. Create a backup-copy of the solved riddle-file.     
6. Remove all the correct answers from the file so it can be solved again.
        Check a few times if the riddle can be solved correctly and ends up being the exact same as the solved file.      
7. Change the name of the file to the receiving address where you have sent the reward.
(This way, the people trying to solve it can see if the reward has already been claimed).      
8. Send the riddle to a friend, friends or the world.     
9. See how long it takes until the reward is claimed.
10. Smile.




Donate:ERVWXKFQQIRGKPVDYZTWVGAP9UGOPOYWLCKCPNDNWAIVPTVFYGYERWLMWULHBCLLXNUHZYQFARVUXJXK9WDJECDJOW

