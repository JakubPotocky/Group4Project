namespace WorldOfZuul
{
    public class Square
    {
        public char? value;

        public Square(char defValue)
        {
            value = defValue;
        }

        public void changeValue(char newValue)
        {
            value = newValue;
        }
    }
}