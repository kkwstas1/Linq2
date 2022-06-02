class Comic
    {
        public string Name { get; set; }
        public int Issue { get; set; }

        public override string ToString() => $"{Name} (Issue # {Issue}";

         public static readonly IEnumerable<Comic> catalog = new List<Comic>
            {
            new Comic {Name ="Johnny America", Issue = 6},
            new Comic {Name ="Rock and Roll", Issue = 19},
            new Comic{Name ="Womans Work", Issue=36},
            new Comic{Name ="Hippie Madness", Issue=57},
            new Comic{Name ="Revenge of the new Wave Freak", Issue=68},
            new Comic{Name ="Black Monday", Issue=74},
            new Comic{Name ="Tribal Tattoo Madness", Issue=83},
            new Comic{Name ="Object Oriented", Issue=97}

        };

         public static readonly IReadOnlyDictionary<int, decimal> Prices = new Dictionary<int, decimal>
        {
            {6,3600M },
            {19, 500M},
            {36, 650M},
            {57,13525M },
            {68, 250M},
            {74, 75M},
            {83, 25.75M},
            {97, 35.25M}
        };
        public static readonly IEnumerable<Review> Reviews = new[]
        {
        new Review(){ Issue=36, Critic=Critics.MuddyCritic, Score=37.6},
        new Review(){ Issue=74, Critic=Critics.RottenTornadoes, Score=22.8},
        new Review(){ Issue=74, Critic=Critics.MuddyCritic, Score=84.2},
        new Review(){ Issue=83, Critic=Critics.RottenTornadoes, Score=89.4},
        new Review(){ Issue=97, Critic=Critics.MuddyCritic, Score=98.1}
        };
    }

 enum Critics
    {
        MuddyCritic,
        RottenTornadoes
    }
    
    enum PriceRange
    {
        Cheap,
        Expensive
    }
    
    class Review
    {
        public int Issue { get; set; }
        public Critics Critic { get; set; }
        public double Score { get; set;}

    }
    enum PriceRange
    {
        Cheap,
        Expensive
    }
    

 static class ComicAnalyzer
    {
         static private PriceRange CalculatePriceRange(Comic comic)
        {
            if (Comic.Prices[comic.Issue] < 100)
                return PriceRange.Cheap;
            else return
                    PriceRange.Expensive;

        }
        internal static IEnumerable<IGrouping<PriceRange, Comic>> GroupComicsByPrice(IEnumerable<Comic> comics, IReadOnlyDictionary<int,decimal> prices)
        {
            IEnumerable<IGrouping<PriceRange,Comic>> grouped=
                from Comic in comics
                orderby prices[Comic.Issue]
                group Comic by CalculatePriceRange(Comic) into pricegroup
                select pricegroup;
            return grouped;
       
        }
        internal static IEnumerable<string> GetReviews(IEnumerable<Comic> comics, IEnumerable<Review> reviews)
        {
            var joined =
                from Comic in comics
                join Review in reviews
                on Comic.Issue equals Review.Issue
                select $" {Review.Critic} rated # {Comic.Issue} {Comic.Name} {Review.Score:0.00}";

                return joined;

        }
        
       

    }
public static class Program
    {
        static bool GetReviews()
        {
            var reviews = ComicAnalyzer.GetReviews(Comic.catalog, Comic.Reviews);
                {
                foreach (var review in reviews)
                    Console.WriteLine(review);
                return false;

            }
        }
        static bool GroupComicsByPrice()
        {
            var groups = ComicAnalyzer.GroupComicsByPrice(Comic.catalog, Comic.Prices);
            foreach (var group in groups)
            {
                Console.WriteLine($" {group.Key} comics");
                foreach (var comic in group)
                    Console.WriteLine($"{comic.Issue} {comic.Name}: {Comic.Prices[comic.Issue]}");
            }
            return false;
        }



        static void Main(string[] args)
        {
            var done = false;
            while(!done)
            {
                Console.WriteLine(
                    "\n Press G to group comics or R to get reviews ot anything else to quit\n");
                switch(Console.ReadKey(true).KeyChar.ToString().ToUpper())
                {
                    case "G":
                        done = GroupComicsByPrice();
                        break;
                    case "R":
                        done=GetReviews();
                        break;
                    default:
                        done = true;
                        break;

                }
                    




            }
            
            




        }
    }
