namespace WorldOfZuul
{
    public class MayorPrompts
    {
        private static readonly Dictionary<string, string> _Prompts = new()
        {
            ["Introduction"]= @"Hey there, welcome to our game! Let me spin you a yarn about this city—it was kicking it for a hundred years, everyone going about their business in peace. Growing steadily, you know, like a city should. But then, bam! During Wolrd War II, five years back in 1941, everything went south. Turns out, Russia goofed up big time, and a rocket meant for who knows where landed smack on our city. Talk about a major oopsie.

So, here's the deal—I'm handing you the reins to rebuild this place. Why? 'Cause I reckon you've got the chops for it. As the city owner, your mission, should you choose to accept it, is to bring this once-happening spot back to life and make it all about sustainability and prosperity.

Where you decide to plop down each building is gonna shape not just the city skyline but also its whole vibe. It sounds like a big ask, but don't sweat it. I'm here, the mayor, your virtual sidekick, ready to drop hints and keep you on the path to success.

Throughout the game, I'll be keeping tabs on your moves, offering hints to make sure your city gets back on its feet. And hey, don't be afraid—I'm right here with you, just like I promised. I've got your back, and we're in this together. Let's make this city rise again and shine like never before! Good luck on this wild ride of rebuilding, and remember, I'm here to help you every step of the way!",
            ["Quest1"]= "Let the adventure begin! Start by giving your citizens a snug roof over their heads! We're starting with just 5 houses, but trust me, this is just the tip of the iceberg!",
            ["Quest2"]= "Time for a shopping spree! Get that grocery shop, and let the cash registers sing!",
            ["Quest3"]= "Oh no, we've got some folks out here braving the elements! Let's build 5 more houses, quick!",
            ["Quest4"]= "Alright, now that we've got a cozy critter community going, it's time to get those wallets jingling! Let's dive into the world of work and make that money magic happen!",
            ["Quest5"]= "Guess who just rolled into town? More eager souls ready to call this city home sweet home! Let's whip up 5 new houses for our ever-expanding crew!",
            ["Quest6"]="Time keeps on tickin' and the city keeps growin'! Let's set up another market for our bustling population!",
            ["Quest7"]="Someone needs to be in charge of these people and the city needs a government! Build a town hall!",
            ["Quest8"]= "Whoa, the city's booming! More people, more houses! It's like a rollercoaster ride of progress!",
            ["Quest9"]= "Sometimes life throws a curveball, but we're prepared! Let's build a hospital and keep our citizens safe and sound!",
            ["Quest10"]= "Bam! Another 5 houses in the mix! We're on fire, keep it up!",
            ["Quest11"]= "We've got folks of all ages in our city, and that means kiddos too! They need a place to play while their parents rake in the dough!",
            ["Quest12"]= "Kiddos coming in from kindergarten with growling tummies! Let's set up another market to keep them fueled and happy!",
            ["Quest13"]= "Safety first, folks! The Police Department is a must-have. Keep our city secure for our awesome citizens!",
            ["Quest14"]= "After a hard day's work, everyone needs a breather. Create a park, a sanctuary for relaxation and fun!",
            ["Quest15"]= "Amazing progress! People are loving your city, but we need even more houses for our ever-growing population!",
            ["Quest16"]= "Safety is key! Build a fire department to ensure our city stays lit (in a good way)!",
            ["Quest17"]= "Jobs, jobs, jobs! Let's set up some industry to keep our citizens busy and our economy thriving!",
            ["Quest18"]= "You are crashing it! New neighbors are in town, and they need a place to crash! Build 10 more houses for our growing community!",
            ["Quest19"]= "Time for the grand finale! A shopping hall is in order, where locals and travelers alike can shop 'til they drop!",
            ["Quest20"]= "Game on! Our citizens are sports fanatics. Create a space where they can kick, throw, and score to their heart's content!"
        };
        public static Dictionary<string, string> Prompts
        {
            get
            {
                return _Prompts;
            }
        }
    }
}