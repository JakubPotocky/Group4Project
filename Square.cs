namespace WorldOfZuul
{
    public class Square
    {
        public char? value;

        public Square(char defValue)
        {
            this.value = defValue;
        }

        public void changeValue(char newValue)
        {
            value = newValue;
        }
    }
}