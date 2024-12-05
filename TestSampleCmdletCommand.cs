using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace myModule
{
    [Cmdlet(VerbsCommon.Get, "PetFortune")]
    [OutputType(typeof(string))]
    public class GetPetFortuneCmdletCommand : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public int FavoriteNumber { get; set; }

        [Parameter(
            Position = 1,
            ValueFromPipelineByPropertyName = true)]
        [ValidateSet("Cat", "Dog", "Horse")]
        public string FavoritePet { get; set; } = "Dog";

        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("Starting your pet's fortune reading!");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            string fortune = GetPetFortune(FavoritePet, FavoriteNumber);
            WriteObject(fortune);
        }

        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
            WriteVerbose("Pet fortune reading complete!");
        }

        // This method generates a fun fortune based on the pet and favorite number
        private string GetPetFortune(string pet, int number)
        {
            string fortune = $"{pet}'s Lucky Fortune:";
            Random random = new Random();

            // Add a fun pet-specific fortune
            switch (pet.ToLower())
            {
                case "cat":
                    fortune += $"\nA good day for {pet}s to seek out new adventures! Your lucky number {number} will bring you a nap in a sunny spot.";
                    fortune += $"\nExpect a mysterious event to occur, possibly involving a string or laser pointer.";
                    break;
                case "dog":
                    fortune += $"\nToday is a great day for {pet}s to fetch opportunities! Your lucky number {number} will bring you a treat and some belly rubs.";
                    fortune += $"\nBeware of squirrels—your instincts are spot-on today.";
                    break;
                case "horse":
                    fortune += $"\nToday, {pet}s will gallop ahead of the competition. Your lucky number {number} will lead to a perfect trot!";
                    fortune += $"\nYou'll find success in the most unexpected places, but only if you can avoid stepping on that pesky rake.";
                    break;
                default:
                    fortune += $"\nYour {pet} will enjoy a day of rest, where dreams of treats and running free in the fields fill their thoughts.";
                    break;
            }

            // Add a fun calculation based on the favorite number
            int luckySum = number + random.Next(1, 10); // Adding some randomness for fun
            fortune += $"\nLucky Number Bonus: {luckySum}. This will bring you a special surprise today—possibly involving snacks!";
            
            return fortune;
        }
    }
}
