using CIS_560_Final_Project.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using CIS_560_Final_Project.Entities;
using Microsoft.AspNetCore.Identity;

namespace CIS_560_Final_Project.Data
{
    public class DbInitializer
    {

        // Seed database
        public static async Task Initialize(SiteContext context, UserManager<Users> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();


            if (!context.Games.Any())
            {
                var games = new Games[]
                {
                    new Games{name="League of Legends", Abv="LoL", Company="RiotGames"},
                    new Games{name="Counter Strike Global Offensive", Abv="CSGO", Company="Valve"},
                    new Games{name="Dota 2", Abv="Dota", Company="Valve"},
                    new Games{name="Heroes of the Storm", Abv="HOTS", Company="Blizzard"},
                    new Games{name="Rocket League", Abv="RL", Company="Psyonix"},
                    new Games{name="Overwatch", Abv="OW", Company="Blizzard"}
                };

                foreach (Games g in games)
                {
                    context.Games.Add(g);
                }
                context.SaveChanges();
            }

            if (!context.Schools.Any())
            {
                var schools = new Schools[]
                {
                new Schools{Name="Kansas State University",Address1="3260 College Ave",City="Manhattan",State=States.KS, Population = 24766},
                new Schools{Name="University of Kansas",Address1="1535 W 15th St",City="Lawerence",State=States.KS, Population = 28401},
                new Schools{ Name = "Pede Cras Vulputate Inc.", Address1 = "P.O. Box 666, 2163 A St.", City = "Hillsboro", State = States.OR, Population = 12009 },
                new Schools{ Name = "Lectus Nullam Suscipit Ltd", Address1 = "912-5958 Donec Av.", City = "Anchorage", State = States.AK, Population = 19252 },
                new Schools{ Name = "Dapibus Rutrum Justo Company", Address1 = "P.O. Box 243, 8575 Turpis. St.", City = "Columbus", State = States.GA, Population = 7332 },
                new Schools{ Name = "Tincidunt Associates", Address1 = "P.O. Box 749, 2462 Et, St.", City = "Minneapolis", State = States.MN, Population = 24364 },
                new Schools{ Name = "Sollicitudin Adipiscing Ligula Associates", Address1 = "Ap #128-8145 Nunc Ave", City = "Jonesboro", State = States.AR, Population = 19046 },
                new Schools{ Name = "In Consectetuer PC", Address1 = "214-2873 Mi St.", City = "Davenport", State = States.IA, Population = 20782 },
                new Schools{ Name = "Tellus Eu Company", Address1 = "943-2163 Eget Rd.", City = "Green Bay", State = States.WI, Population = 21770 },
                new Schools{ Name = "Odio Vel Limited", Address1 = "P.O. Box 709, 1687 Integer St.", City = "Little Rock", State = States.AR, Population = 7050 },
                new Schools{ Name = "Nec Euismod In LLP", Address1 = "5876 Eu Rd.", City = "Toledo", State = States.OH, Population = 7951 },
                new Schools{ Name = "Lacinia Incorporated", Address1 = "497-6915 Amet, Avenue", City = "Norman", State = States.OK, Population = 6664 },
                new Schools{ Name = "Dui Limited", Address1 = "P.O. Box 810, 6573 Aliquet St.", City = "Saint Louis", State = States.MO, Population = 1870 },
                new Schools{ Name = "Turpis Nulla Aliquet Incorporated", Address1 = "Ap #937-1000 Quisque Avenue", City = "Tampa", State = States.FL, Population = 4514 },
                new Schools{ Name = "Mi Tempor Limited", Address1 = "434-6289 Tempus Rd.", City = "Hillsboro", State = States.OR, Population = 15580 },
                new Schools{ Name = "Cras Inc.", Address1 = "716-4888 A, St.", City = "Jonesboro", State = States.AR, Population = 18230 },
                new Schools{ Name = "Ante LLC", Address1 = "Ap #885-2647 Mauris. St.", City = "Racine", State = States.WI, Population = 12748 },
                new Schools{ Name = "Tellus Suspendisse Inc.", Address1 = "6341 Phasellus Ave", City = "Missoula", State = States.MT, Population = 5998 },
                new Schools{ Name = "Semper Industries", Address1 = "261-6353 Convallis Rd.", City = "Iowa City", State = States.IA, Population = 2589 },
                new Schools{ Name = "Bibendum Donec Felis Corp.", Address1 = "2521 Eros Avenue", City = "Saint Louis", State = States.MO, Population = 8621 },
                new Schools{ Name = "Lectus Convallis Associates", Address1 = "881-9559 Vivamus Rd.", City = "Burlington", State = States.VT, Population = 23497 },
                new Schools{ Name = "Aliquam Enim Company", Address1 = "302-3878 Eget Street", City = "Spokane", State = States.WA, Population = 19761 },
                new Schools{ Name = "Erat Eget Tincidunt LLP", Address1 = "P.O. Box 215, 8619 Facilisis. Avenue", City = "Bellevue", State = States.NE, Population = 24643 },
                new Schools{ Name = "Turpis Non Enim Institute", Address1 = "P.O. Box 927, 1519 Suspendisse Rd.", City = "Sacramento", State = States.CA, Population = 3952 },
                new Schools{ Name = "Dolor Egestas Institute", Address1 = "Ap #967-3875 Donec Ave", City = "Honolulu", State = States.HI, Population = 11386 },
                new Schools{ Name = "Mauris Ipsum Institute", Address1 = "P.O. Box 747, 5355 Risus. Rd.", City = "Columbia", State = States.MD, Population = 7857 },
                new Schools{ Name = "Pharetra Associates", Address1 = "360-7577 Amet, Rd.", City = "Flint", State = States.MI, Population = 24270 },
                new Schools{ Name = "In At Pede Corporation", Address1 = "P.O. Box 739, 5481 Ad Rd.", City = "Tucson", State = States.AZ, Population = 23034 },
                new Schools{ Name = "Magna Foundation", Address1 = "2349 Dictum St.", City = "Indianapolis", State = States.IN, Population = 24357 },
                new Schools{ Name = "Accumsan Limited", Address1 = "163-5335 Quisque St.", City = "Louisville", State = States.KY, Population = 15830 },
                new Schools{ Name = "In Dolor Fusce LLC", Address1 = "P.O. Box 276, 6883 Libero Ave", City = "Frederick", State = States.MD, Population = 6465 },
                new Schools{ Name = "In Dolor Fusce Company", Address1 = "733-9889 Suscipit Road", City = "Gulfport", State = States.MS, Population = 8893 },
                new Schools{ Name = "Dolor Egestas Rhoncus Incorporated", Address1 = "626-6487 Nunc Road", City = "Dover", State = States.DE, Population = 9663 },
                new Schools{ Name = "Ornare Egestas Ligula Ltd", Address1 = "566-1653 Parturient Rd.", City = "Gary", State = States.IN, Population = 9011 },
                new Schools{ Name = "Per Conubia Nostra Limited", Address1 = "P.O. Box 148, 7004 Justo. St.", City = "Olathe", State = States.KS, Population = 18208 },
                new Schools{ Name = "Mi Felis Adipiscing PC", Address1 = "P.O. Box 610, 2986 Fringilla Av.", City = "Milwaukee", State = States.WI, Population = 19962 },
                new Schools{ Name = "Sed Libero LLP", Address1 = "9772 Nunc Ave", City = "Nampa", State = States.ID, Population = 16402 },
                new Schools{ Name = "Rutrum Corp.", Address1 = "Ap #977-9447 Netus Ave", City = "Gresham", State = States.OR, Population = 14644 },
                new Schools{ Name = "Mi Ac Mattis Company", Address1 = "P.O. Box 820, 5849 Amet St.", City = "Tucson", State = States.AZ, Population = 2459 },
                new Schools{ Name = "Fringilla Ornare Institute", Address1 = "P.O. Box 260, 5818 Sem Rd.", City = "Denver", State = States.CO, Population = 5255 },
                new Schools{ Name = "Consequat Associates", Address1 = "444-3710 Tristique St.", City = "Nashville", State = States.TN, Population = 22147 },
                new Schools{ Name = "Ornare Elit Ltd", Address1 = "P.O. Box 582, 7369 Sit Street", City = "Newark", State = States.DE, Population = 14733 },
                new Schools{ Name = "Enim LLP", Address1 = "Ap #313-1515 Donec Av.", City = "Tallahassee", State = States.FL, Population = 22244 },
                new Schools{ Name = "Mus Proin Vel Industries", Address1 = "111 Dolor. Rd.", City = "Rock Springs", State = States.WY, Population = 6646 },
                new Schools{ Name = "Ipsum LLC", Address1 = "Ap #366-5708 Eu Rd.", City = "Des Moines", State = States.IA, Population = 10197 },
                new Schools{ Name = "Mi Duis Risus Corp.", Address1 = "Ap #204-8580 Egestas. St.", City = "Worcester", State = States.MA, Population = 2424 },
                new Schools{ Name = "Ligula Institute", Address1 = "6356 Quisque Road", City = "Knoxville", State = States.TN, Population = 6648 },
                new Schools{ Name = "Cras Vulputate Velit Incorporated", Address1 = "978-6670 Metus. St.", City = "Wichita", State = States.KS, Population = 23549 },
                new Schools{ Name = "Nulla Eget Metus Company", Address1 = "Ap #870-118 Quisque Street", City = "Denver", State = States.CO, Population = 14180 },
                new Schools{ Name = "Lacus Quisque PC", Address1 = "Ap #575-1457 Mi Rd.", City = "Little Rock", State = States.AR, Population = 14360 },
                new Schools{ Name = "Morbi Sit Limited", Address1 = "615-6549 Tempor Road", City = "Anchorage", State = States.AK, Population = 20567 },
                new Schools{ Name = "Varius Foundation", Address1 = "3926 Vulputate, St.", City = "San Francisco", State = States.CA, Population = 14778 },
                new Schools{ Name = "Felis Adipiscing Limited", Address1 = "3259 Cras Av.", City = "Gaithersburg", State = States.MD, Population = 2480 },
                new Schools{ Name = "Eget Ipsum Donec Limited", Address1 = "594-803 Penatibus St.", City = "Stamford", State = States.CT, Population = 6315 },
                new Schools{ Name = "Consectetuer Adipiscing Elit Industries", Address1 = "Ap #158-3847 Venenatis Avenue", City = "Little Rock", State = States.AR, Population = 2236 },
                new Schools{ Name = "Nec Tellus Nunc Corporation", Address1 = "Ap #290-5711 In Rd.", City = "Glendale", State = States.AZ, Population = 3694 },
                new Schools{ Name = "Eu Corp.", Address1 = "Ap #708-8916 Sed, Avenue", City = "Springdale", State = States.AR, Population = 19488 },
                new Schools{ Name = "Odio Industries", Address1 = "6000 Odio. Av.", City = "Kenosha", State = States.WI, Population = 23789 },
                new Schools{ Name = "Sagittis Augue Eu Incorporated", Address1 = "5055 Aliquam St.", City = "Fairbanks", State = States.AK, Population = 20752 },
                new Schools{ Name = "Amet Consectetuer LLP", Address1 = "6616 Tortor. St.", City = "Olympia", State = States.WA, Population = 3917 },
                new Schools{ Name = "Hendrerit Neque Inc.", Address1 = "1053 A St.", City = "Birmingham", State = States.AL, Population = 15803 },
                new Schools{ Name = "Hendrerit Donec Porttitor LLC", Address1 = "291-1250 Interdum. Rd.", City = "Indianapolis", State = States.IN, Population = 16962 },
                new Schools{ Name = "Metus Vivamus LLC", Address1 = "456-2699 Vitae St.", City = "Toledo", State = States.OH, Population = 1567 },
                new Schools{ Name = "Lacus Vestibulum Lorem Corp.", Address1 = "8445 Risus. St.", City = "Provo", State = States.UT, Population = 14903 },
                new Schools{ Name = "Mi Enim Condimentum Consulting", Address1 = "P.O. Box 285, 1580 Penatibus Rd.", City = "Fort Worth", State = States.TX, Population = 8796 },
                new Schools{ Name = "Quis Arcu Vel LLC", Address1 = "7592 Tempus Street", City = "Fort Collins", State = States.CO, Population = 11836 },
                new Schools{ Name = "Suspendisse Dui Fusce Limited", Address1 = "P.O. Box 278, 7721 Dolor St.", City = "Minneapolis", State = States.MN, Population = 14871 },
                new Schools{ Name = "Ut Sagittis Associates", Address1 = "743-9952 Purus, St.", City = "Chattanooga", State = States.TN, Population = 19992 },
                new Schools{ Name = "Eleifend Nunc LLP", Address1 = "Ap #609-3354 Erat Street", City = "Sandy", State = States.UT, Population = 17988 },
                new Schools{ Name = "Tincidunt Donec Vitae LLP", Address1 = "6578 Vulputate, Ave", City = "Tacoma", State = States.WA, Population = 13910 },
                new Schools{ Name = "Libero Incorporated", Address1 = "P.O. Box 134, 492 Et Street", City = "Bear", State = States.DE, Population = 4498 },
                new Schools{ Name = "Sit Amet Luctus Incorporated", Address1 = "P.O. Box 206, 3870 Vel Rd.", City = "Rockville", State = States.MD, Population = 11127 },
                new Schools{ Name = "Velit Aliquam Incorporated", Address1 = "383-9114 Elit, Av.", City = "Kansas City", State = States.KS, Population = 16124 },
                new Schools{ Name = "Donec Corp.", Address1 = "P.O. Box 501, 8615 Magna. Street", City = "South Burlington", State = States.VT, Population = 2336 },
                new Schools{ Name = "Vitae Erat Vel Company", Address1 = "342-8445 Morbi Avenue", City = "Montpelier", State = States.VT, Population = 22107 },
                new Schools{ Name = "Et Malesuada Fames Corporation", Address1 = "293-4754 Adipiscing. St.", City = "Wichita", State = States.KS, Population = 14037 },
                new Schools{ Name = "Augue Ac Foundation", Address1 = "860-2908 Vel, St.", City = "Tulsa", State = States.OK, Population = 6814 },
                new Schools{ Name = "Libero Dui Ltd", Address1 = "1397 Praesent Rd.", City = "Augusta", State = States.ME, Population = 4852 },
                new Schools{ Name = "Eu Odio Tristique LLC", Address1 = "6585 Odio. Avenue", City = "Green Bay", State = States.WI, Population = 9323 },
                new Schools{ Name = "Aliquam Tincidunt LLP", Address1 = "475-3328 Faucibus Rd.", City = "Davenport", State = States.IA, Population = 19231 },
                new Schools{ Name = "Lectus LLC", Address1 = "616-8621 Lobortis, Road", City = "Racine", State = States.WI, Population = 19862 },
                new Schools{ Name = "Malesuada Fringilla Corporation", Address1 = "Ap #732-4001 Cras St.", City = "Evansville", State = States.IN, Population = 24277 },
                new Schools{ Name = "Ac Facilisis Facilisis Corporation", Address1 = "P.O. Box 779, 1502 Auctor Street", City = "Juneau", State = States.AK, Population = 24896 },
                new Schools{ Name = "Donec Incorporated", Address1 = "P.O. Box 364, 1100 Sem Ave", City = "Grand Island", State = States.NE, Population = 15293 },
                new Schools{ Name = "Aliquam Eros Turpis Ltd", Address1 = "Ap #251-2411 Cum Rd.", City = "Missoula", State = States.MT, Population = 8813 },
                new Schools{ Name = "Lorem Eget Mollis Foundation", Address1 = "Ap #367-9154 Turpis. Street", City = "Seattle", State = States.WA, Population = 3427 },
                new Schools{ Name = "Ullamcorper Eu Euismod Company", Address1 = "4349 Integer St.", City = "Essex", State = States.VT, Population = 11703 },
                new Schools{ Name = "Lobortis PC", Address1 = "427-5111 Metus. Road", City = "Salem", State = States.OR, Population = 23636 },
                new Schools{ Name = "Sit LLP", Address1 = "4976 Ridiculus Rd.", City = "Fort Smith", State = States.AR, Population = 1584 },
                new Schools{ Name = "Rhoncus Corp.", Address1 = "P.O. Box 227, 4892 Id, Street", City = "Bowling Green", State = States.KY, Population = 24493 },
                new Schools{ Name = "Amet Consectetuer Adipiscing Foundation", Address1 = "443-2718 Quam Road", City = "Norfolk", State = States.VA, Population = 2680 },
                new Schools{ Name = "Amet Consulting", Address1 = "433-6404 Augue Rd.", City = "Vancouver", State = States.WA, Population = 15877 },
                new Schools{ Name = "Facilisis Vitae Corporation", Address1 = "Ap #708-7402 Pharetra, Rd.", City = "Davenport", State = States.IA, Population = 17920 },
                new Schools{ Name = "Mus Foundation", Address1 = "P.O. Box 719, 9544 Integer Rd.", City = "Saint Paul", State = States.MN, Population = 21159 },
                new Schools{ Name = "Praesent Eu Consulting", Address1 = "Ap #760-5265 Vivamus Street", City = "Fort Worth", State = States.TX, Population = 12390 },
                new Schools{ Name = "Eu Company", Address1 = "Ap #189-3966 Ante Road", City = "Pocatello", State = States.ID, Population = 11704 },
                new Schools{ Name = "Gravida Sit Associates", Address1 = "Ap #822-4378 Nibh Avenue", City = "Richmond", State = States.VA, Population = 17532 },
                new Schools{ Name = "Iaculis Lacus Consulting", Address1 = "8411 Sed Avenue", City = "Savannah", State = States.GA, Population = 13223 },
                new Schools{ Name = "Mollis Vitae Ltd", Address1 = "265-614 Mollis Ave", City = "Metairie", State = States.LA, Population = 13297 },
                new Schools{ Name = "Netus Et Company", Address1 = "457-2811 Cursus Av.", City = "Kearney", State = States.NE, Population = 19307 },
                new Schools{ Name = "Malesuada Inc.", Address1 = "P.O. Box 139, 4778 Fringilla Rd.", City = "Tallahassee", State = States.FL, Population = 5057 },
                new Schools{ Name = "Scelerisque Incorporated", Address1 = "3653 Non St.", City = "Savannah", State = States.GA, Population = 3234 }
            };



                foreach (Schools s in schools)
                {
                    context.Schools.Add(s);
                }
                context.SaveChanges();
            }

            if (!context.Teams.Any())
            {
                var teams = new Teams[]
                {
                    new Teams
                    {
                        Name="KSU LOL",
                        Game=context.Games.Single(s=>s.name=="League of Legends"),
                        School=context.Schools.Single(s=>s.Name=="Kansas State University")
                    },
                    new Teams{Name="KU A-Team",Game=context.Games.Single(s=>s.name=="League of Legends"),School=context.Schools.Single(s=>s.Name=="University of Kansas")},
                    new Teams{ Name = "Cadillac", Game = context.Games.Single(s=>s.name=="Overwatch"), School=context.Schools.Single(s=>s.Name=="Sit LLP") },
    new Teams{ Name = "Ford", Game = context.Games.Single(s=>s.name=="Dota 2"), School=context.Schools.Single(s=>s.Name=="Tellus Suspendisse Inc.") },
    new Teams{ Name = "Porsche", Game = context.Games.Single(s=>s.name=="Counter Strike Global Offensive"), School=context.Schools.Single(s=>s.Name=="Magna Foundation") },
    new Teams{ Name = "Lincoln", Game = context.Games.Single(s=>s.name=="League of Legends"), School=context.Schools.Single(s=>s.Name=="Pede Cras Vulputate Inc.") },
    new Teams{ Name = "FAW", Game = context.Games.Single(s=>s.name=="League of Legends"), School=context.Schools.Single(s=>s.Name=="Malesuada Fringilla Corporation") },
    new Teams{ Name = "Killa Whales", Game = context.Games.Single(s=>s.name=="League of Legends"), School=context.Schools.Single(s=>s.Name=="Tellus Suspendisse Inc.") },
    new Teams{ Name = "Volkswagen", Game = context.Games.Single(s=>s.name=="Heroes of the Storm"), School=context.Schools.Single(s=>s.Name=="Magna Foundation") },
    new Teams{ Name = "Acura", Game = context.Games.Single(s=>s.name=="League of Legends"), School=context.Schools.Single(s=>s.Name=="Mus Proin Vel Industries") },
    new Teams{ Name = "Mercedes-Benz", Game = context.Games.Single(s=>s.name=="Dota 2"), School=context.Schools.Single(s=>s.Name=="Malesuada Fringilla Corporation") },
    new Teams{ Name = "Ishtar", Game = context.Games.Single(s=>s.name=="Overwatch"), School=context.Schools.Single(s=>s.Name=="Mus Proin Vel Industries") },
    new Teams{ Name = "Smart", Game = context.Games.Single(s=>s.name=="Counter Strike Global Offensive"), School=context.Schools.Single(s=>s.Name=="Magna Foundation") },
    new Teams{ Name = "Subaru", Game = context.Games.Single(s=>s.name=="Overwatch"), School=context.Schools.Single(s=>s.Name=="University of Kansas") },
    new Teams{ Name = "Peugeot", Game = context.Games.Single(s=>s.name=="Overwatch"), School=context.Schools.Single(s=>s.Name=="Sit LLP") },
    new Teams{ Name = "Fiat", Game = context.Games.Single(s=>s.name=="League of Legends"), School=context.Schools.Single(s=>s.Name=="Pede Cras Vulputate Inc.") },
    new Teams{ Name = "Citroën", Game = context.Games.Single(s=>s.name=="Heroes of the Storm"), School=context.Schools.Single(s=>s.Name=="University of Kansas") },
    new Teams{ Name = "Peugeot", Game = context.Games.Single(s=>s.name=="Dota 2"), School=context.Schools.Single(s=>s.Name=="Sit LLP") },
    new Teams{ Name = "Lexus", Game = context.Games.Single(s=>s.name=="Dota 2"), School=context.Schools.Single(s=>s.Name=="Pede Cras Vulputate Inc.") },
    new Teams{ Name = "Chrysler", Game = context.Games.Single(s=>s.name=="Overwatch"), School=context.Schools.Single(s=>s.Name=="Tellus Suspendisse Inc.") },
    new Teams{ Name = "JLR", Game = context.Games.Single(s=>s.name=="Rocket League"), School=context.Schools.Single(s=>s.Name=="Pede Cras Vulputate Inc.") },
    new Teams{ Name = "Seat", Game = context.Games.Single(s=>s.name=="Heroes of the Storm"), School=context.Schools.Single(s=>s.Name=="Tincidunt Associates") },
    new Teams{ Name = "Bibibibibi", Game = context.Games.Single(s=>s.name=="Heroes of the Storm"), School=context.Schools.Single(s=>s.Name=="Pede Cras Vulputate Inc.") },
    new Teams{ Name = "Acura", Game = context.Games.Single(s=>s.name=="Counter Strike Global Offensive"), School=context.Schools.Single(s=>s.Name=="Mus Proin Vel Industries") },
    new Teams{ Name = "Ka-chow", Game = context.Games.Single(s=>s.name=="Dota 2"), School=context.Schools.Single(s=>s.Name=="Kansas State University") },
    new Teams{ Name = "Citroën", Game = context.Games.Single(s=>s.name=="Heroes of the Storm"), School=context.Schools.Single(s=>s.Name=="Sit LLP") },
    new Teams{ Name = "Isuzu", Game = context.Games.Single(s=>s.name=="Dota 2"), School=context.Schools.Single(s=>s.Name=="Malesuada Fringilla Corporation") },
    new Teams{ Name = "Nissan", Game = context.Games.Single(s=>s.name=="Rocket League"), School=context.Schools.Single(s=>s.Name=="Kansas State University") },
    new Teams{ Name = "Seat", Game = context.Games.Single(s=>s.name=="Dota 2"), School=context.Schools.Single(s=>s.Name=="Tellus Suspendisse Inc.") },
    new Teams{ Name = "Nissan", Game = context.Games.Single(s=>s.name=="Rocket League"), School=context.Schools.Single(s=>s.Name=="Sit LLP") },
    new Teams{ Name = "Tata Motors", Game = context.Games.Single(s=>s.name=="Overwatch"), School=context.Schools.Single(s=>s.Name=="Pede Cras Vulputate Inc.") },
    new Teams{ Name = "Audi", Game = context.Games.Single(s=>s.name=="Rocket League"), School=context.Schools.Single(s=>s.Name=="Mi Enim Condimentum Consulting") }
                };
                foreach (Teams t in teams)
                {
                    context.Teams.Add(t);
                }
                context.SaveChanges();
            }

            if (!context.Tournaments.Any())
            {
                var tournaments = new Tournaments[]
                {
                    new Tournaments
                    {
                        Name="Midwest Regionals",
                        StartDate = new DateTime(2017, 12, 8),
                        EndDate = new DateTime(2017, 12, 10),
                        Game=context.Games.Single(s=>s.name=="League of Legends")
                    },
                    new Tournaments{ Name = "Rocket Fiesta Xmas Special", StartDate = new DateTime(2017,04,15), EndDate = new DateTime(2018,09,13), Game=context.Games.Single(s=>s.name=="Rocket League") },
                    new Tournaments{ Name = "DMAX 2017", StartDate = new DateTime(2017,05,30), EndDate = new DateTime(2017,06,10), Game=context.Games.Single(s=>s.name=="Dota 2") },
                    new Tournaments{ Name = "futbol leg", StartDate = new DateTime(2017,04,20), EndDate = new DateTime(2017,08,27), Game=context.Games.Single(s=>s.name=="Rocket League") },
                    new Tournaments{ Name = "OW-Maker (ooch owwie)", StartDate = new DateTime(2017,01,15), EndDate = new DateTime(2017,04,26), Game=context.Games.Single(s=>s.name=="Overwatch") }
  
                };
                foreach (Tournaments t in tournaments)
                {
                    context.Tournaments.Add(t);
                }
                context.SaveChanges();
            }
            
            if (!context.Matches.Any())
            {
                var matches = new Matches[]
                {
                    new Matches
                    {
                        MatchNumber = 1,
                        Team1ID = 1,
                        Team2ID = 2,
                        Winner = 1,
                        Datetime = new DateTime(2017, 12, 2),
                        TournamentID = 1
                    },
                    
                    new Matches
                    {
                        MatchNumber = 2,
                        Team1ID=1,
                        Team2ID=2,
                        Winner = 1,
                        Datetime = new DateTime(2017, 12, 3),
                        TournamentID = 1
                    },

                    new Matches
                    {
                        MatchNumber = 3,
                        Team1ID=1,
                        Team2ID=2,
                        Winner = 2,
                        Datetime = new DateTime(2017, 12, 4),
                        TournamentID=1
                    },

                    new Matches
                    {
                        MatchNumber = 4,
                        Team1ID=1,
                        Team2ID=2,
                        Winner = 1,
                        Datetime = new DateTime(2017, 12, 5),
                        TournamentID=1
                    },
                    
                    new Matches
                    {
                        MatchNumber = 1,
                        Team1ID=5,
                        Team2ID=7,
                        Winner = 1,
                        Datetime = new DateTime(2017, 12, 6),
                        TournamentID=2
                    },

                    new Matches
                    {
                        MatchNumber = 2,
                        Team1ID=6,
                        Team2ID=7,
                        Winner = 1,
                        Datetime = new DateTime(2017, 12, 7),
                        TournamentID=2
                    },

                    new Matches
                    {
                        MatchNumber = 3,
                        Team1ID=6,
                        Team2ID=5,
                        Winner = 2,
                        Datetime = new DateTime(2017, 12, 8),
                        TournamentID=2
                    },
                    new Matches
                    {
                        MatchNumber = 1,
                        Team1ID=11,
                        Team2ID=22,
                        Winner = 1,
                        Datetime = new DateTime(2017, 12, 9),
                        TournamentID=3
                    },

                    new Matches
                    {
                        MatchNumber = 2,
                        Team1ID=12,
                        Team2ID=21,
                        Winner = 1,
                        Datetime = new DateTime(2017, 12, 10),
                        TournamentID=3
                    },

                    new Matches
                    {
                        MatchNumber = 3,
                        Team1ID=12,
                        Team2ID=22,
                        Winner = 2,
                        Datetime = new DateTime(2017, 12, 10),
                        TournamentID=3
                    },
                    new Matches
                    {
                        MatchNumber = 1,
                        Team1ID=11,
                        Team2ID=12,
                        Winner = 1,
                        Datetime = new DateTime(2017, 12, 10),
                        TournamentID=4
                    },

                    new Matches
                    {
                        MatchNumber = 2,
                        Team1ID=11,
                        Team2ID=12,
                        Winner = 1,
                        Datetime = new DateTime(2017, 12, 10),
                        TournamentID=4
                    },

                    new Matches
                    {
                        MatchNumber = 3,
                        Team1ID=5,
                        Team2ID=7,
                        Winner = 2,
                        Datetime = new DateTime(2017, 12, 10),
                        TournamentID=4
                    }

                };

                foreach (Matches m in matches)
                {
                    context.Matches.Add(m);
                }
                context.SaveChanges();
            }
                     
            if (!context.Scrims.Any())
            {
                var scrims = new Scrims[]
                {
                    new Scrims
                    {
                        Team1ID=1,
                        Team2ID=2,
                        Winner = 1,
                        Datetime = new DateTime(2017, 11, 10)
                    },
                    new Scrims
                    {
                        Team1ID=1,
                        Team2ID=5,
                        Winner = 2,
                        Datetime = new DateTime(2017, 11, 5)
                    },
                    new Scrims
                    {
                        Team1ID=20,
                        Team2ID=5,
                        Winner = 2,
                        Datetime = new DateTime(2017, 12, 02)
                    },

                    new Scrims
                    {
                        Team1ID=30,
                        Team2ID=15,
                        Winner = 1,
                        Datetime = new DateTime(2017, 12, 06)
                    }
                };
                foreach (Scrims m in scrims)
                {
                    context.Scrims.Add(m);
                }
                context.SaveChanges();
            }
            
            if (!context.Players.Any())
            {
                var players = new Players[]
                {
                    new Players{ FirstName = "Adara", LastName = "Laith", Joined =  new DateTime(2017,03,23), Year =YearClassification.Freshman, IGN = "Nulla aliquet." },
                    new Players{ FirstName = "Hanae", LastName = "Quyn", Joined =  new DateTime(2017,01,29), Year =YearClassification.Freshman, IGN = "gravida non," },
                    new Players{ FirstName = "Joelle", LastName = "Merrill", Joined =  new DateTime(2017,01,29), Year =YearClassification.Junior, IGN = "hendrerit a," },
                    new Players{ FirstName = "Norman", LastName = "Hiram", Joined =  new DateTime(2017,05,22), Year =YearClassification.Senior, IGN = "elit. Curabitur" },
                    new Players{ FirstName = "Allen", LastName = "Tanek", Joined =  new DateTime(2016,12,30), Year =YearClassification.Junior, IGN = "neque. Nullam" },
                    new Players{ FirstName = "Aimee", LastName = "Odette", Joined =  new DateTime(2017,05,01), Year =YearClassification.Junior, IGN = "Donec fringilla." },
                    new Players{ FirstName = "Cadman", LastName = "Tatiana", Joined =  new DateTime(2017,04,29), Year =YearClassification.Senior, IGN = "ridiculus mus." },
                    new Players{ FirstName = "Sophia", LastName = "Arsenio", Joined =  new DateTime(2017,01,09), Year =YearClassification.Junior, IGN = "varius ultrices," },
                    new Players{ FirstName = "Tashya", LastName = "McKenzie", Joined =  new DateTime(2017,02,05), Year =YearClassification.Sophomore, IGN = "ut cursus" },
                    new Players{ FirstName = "Alyssa", LastName = "Sarah", Joined =  new DateTime(2017,01,14), Year =YearClassification.Sophomore, IGN = "feugiat non," },
                    new Players{ FirstName = "Aristotle", LastName = "James", Joined =  new DateTime(2017,01,25), Year =YearClassification.Junior, IGN = "lectus rutrum" },
                    new Players{ FirstName = "Francesca", LastName = "Gay", Joined =  new DateTime(2016,12,28), Year =YearClassification.Freshman, IGN = "Nunc mauris" },
                    new Players{ FirstName = "Baker", LastName = "Maggy", Joined =  new DateTime(2017,06,27), Year =YearClassification.Sophomore, IGN = "gravida non," },
                    new Players{ FirstName = "Yvonne", LastName = "Reed", Joined =  new DateTime(2017,06,02), Year =YearClassification.Junior, IGN = "Fusce dolor" },
                    new Players{ FirstName = "Bert", LastName = "April", Joined =  new DateTime(2017,05,12), Year =YearClassification.Senior, IGN = "rutrum magna." },
                    new Players{ FirstName = "Palmer", LastName = "Berk", Joined =  new DateTime(2017,03,12), Year =YearClassification.Junior, IGN = "malesuada malesuada." },
                    new Players{ FirstName = "Drew", LastName = "Jaden", Joined =  new DateTime(2017,07,21), Year =YearClassification.Senior, IGN = "in, tempus" },
                    new Players{ FirstName = "Tamara", LastName = "Danielle", Joined =  new DateTime(2017,05,19), Year =YearClassification.Senior, IGN = "sem. Nulla" },
                    new Players{ FirstName = "Elizabeth", LastName = "Kylynn", Joined =  new DateTime(2017,07,21), Year =YearClassification.Senior, IGN = "Integer mollis."}
                };
                foreach (Players m in players)
                {
                    context.Players.Add(m);
                }
                context.SaveChanges();
            }

            if (!context.Coaches.Any())
            {
                var coaches = new Coaches[]
                {
                new Coaches{ FirstName = "Kirstie", LastName = "Melodie", Joined =  new DateTime(2017,03,23), IsManager = true, YearsCoaching = 1},
                new Coaches{ FirstName = "Tamiko", LastName = "Dwight", Joined =  new DateTime(2017,01,29), IsManager = false, YearsCoaching = 2},
                new Coaches{ FirstName = "Wonda", LastName = "Larae", Joined =  new DateTime(2017,01,29) , IsManager = true, YearsCoaching = 1},
                new Coaches{ FirstName = "Azalee", LastName = "Jackson", Joined =  new DateTime(2017,05,22), IsManager = true, YearsCoaching = 3},
                new Coaches{ FirstName = "Doretta", LastName = "Vannessa", Joined =  new DateTime(2016,12,30), IsManager = false, YearsCoaching = 5},
                new Coaches{ FirstName = "Fidel", LastName = "Shawn", Joined =  new DateTime(2017,05,01), IsManager = true, YearsCoaching = 2},
                new Coaches{ FirstName = "Sunny", LastName = "Rochelle", Joined =  new DateTime(2017,04,29) , IsManager = true, YearsCoaching = 3},
                new Coaches{ FirstName = "Rashad", LastName = "Husfad", Joined =  new DateTime(2017,01,09) , IsManager = false, YearsCoaching = 4},
                new Coaches{ FirstName = "Doretta", LastName = "Poligo", Joined =  new DateTime(2017,02,05) , IsManager = true, YearsCoaching = 1}

                };
                foreach (Coaches m in coaches)
                {
                    context.Coaches.Add(m);
                }
                context.SaveChanges();
            }

            if (!context.TeamsMembers.Any())
            {
                var teamsMembers = new TeamsMembers[]
                {
                new TeamsMembers{ TeamsID = 2, MemberID = 1},
                new TeamsMembers{ TeamsID = 15, MemberID = 2},
                new TeamsMembers{ TeamsID = 13, MemberID = 3},
                new TeamsMembers{ TeamsID = 14, MemberID = 4},
                new TeamsMembers{ TeamsID = 3, MemberID = 5},
                new TeamsMembers{ TeamsID = 1, MemberID = 6},
                new TeamsMembers{ TeamsID = 5, MemberID = 7},
                new TeamsMembers{ TeamsID = 7, MemberID = 8},
                new TeamsMembers{ TeamsID = 4, MemberID = 9},
                new TeamsMembers{ TeamsID = 6, MemberID = 10},
                new TeamsMembers{ TeamsID = 2, MemberID = 11},
                new TeamsMembers{ TeamsID = 3, MemberID = 12},
                new TeamsMembers{ TeamsID = 1, MemberID = 13},
                new TeamsMembers{ TeamsID = 4, MemberID = 14},
                new TeamsMembers{ TeamsID = 5, MemberID = 15},
                new TeamsMembers{ TeamsID = 7, MemberID = 16},
                new TeamsMembers{ TeamsID = 1, MemberID = 17},
                new TeamsMembers{ TeamsID = 4, MemberID = 18},
                new TeamsMembers{ TeamsID = 5, MemberID = 19},
                new TeamsMembers{ TeamsID = 7, MemberID = 20},
                };
                foreach (TeamsMembers m in teamsMembers)
                {
                    context.TeamsMembers.Add(m);
                }
                context.SaveChanges();
            }

            //Create admin user and role
            await CreateAdmin(context, userManager, roleManager);
        }

        public static async Task CreateAdmin(SiteContext context, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Add roles
            string[] roles = new string[] { "Admin", "User" };
            foreach (string role in roles)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Add admin user
            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                var user = new Users
                {
                    Email = "admin@admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    EmailConfirmed = true,
                };
                var result = await userManager.CreateAsync(user, "Password123");

                var adminuser = await userManager.FindByEmailAsync("admin@admin.com");
                if (result.Succeeded)
                {
                    var addresult = await userManager.AddToRoleAsync(adminuser, "Admin");
                }
            }


        }

    }

}