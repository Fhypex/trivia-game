using System.Text.RegularExpressions;

public class QuestionDifficulity
{
    private readonly string difficulity;

    public QuestionDifficulity(string difficulity)
    {
        if(validate(difficulity.ToString()))
        {
            this.difficulity = difficulity.ToString();
        }
        else
        {
            throw new System.ArgumentException("Difficulity must be in the format of x.x");
        }
    }

    public string Value()
    {
        return difficulity;
    }


    public override bool Equals(object obj)
    {
        if(obj == null)
        {
            return false;
        }
        if(obj == this)
        {
            return true;
        }
        if(obj.GetType() != GetType())
        {
            return false;
        }
        QuestionDifficulity rhs = (QuestionDifficulity)obj;
        return difficulity == rhs.difficulity;
    }

    public override int GetHashCode()
    {
        return difficulity.GetHashCode();
    }

    public override string ToString()
    {
        return difficulity.ToString();
    }


    private bool validate(string input)
    {
        // Pattern: [0-9]{1}\.[0-9]{1} 
        string pattern = @"^[0-9]{1}\.[0-9]{1}$";

        // Check if the input matches the pattern
        if (Regex.IsMatch(input, pattern))
        {
            // Try to parse the numeric part to a float
            return true;
        }

        // Return false if the input doesn't match the pattern
        return false;
    }
}
