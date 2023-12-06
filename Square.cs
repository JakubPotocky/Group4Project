namespace WorldOfZuul
{
    public class Square //Class that represents square on the game map
    {
        public char? value; //Represents the content of the square
        public Building? obj; // The building object associated with the square

        public Square(char defValue)
        {
            this.value = defValue;
        }

        public void changeValue(char newValue) //method to change the value of the square
        {
            value = newValue;
        }
    }
}