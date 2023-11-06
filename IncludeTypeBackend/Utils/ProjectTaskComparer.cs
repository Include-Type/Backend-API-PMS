namespace IncludeTypeBackend.Utils;

/*
This Comparer derived class is used to compare two ProjectTask(s) based 
on their Deadline and Priority.
1st preference => Deadline
2nd preference => Priority
*/
public class ProjectTaskComparer : Comparer<ProjectTask>
{
    private readonly Dictionary<string, int> priorityPoints = new()
    {
        { "High", 10 },
        { "Medium", 5 },
        { "Low", 0 }
    };

    public override int Compare(ProjectTask x, ProjectTask y)
    {
        int PT1_POINTS = 0;
        int PT2_POINTS = 0;

        string[] formats = { "MM/dd/yyyy hh:mm tt", "MMM-dd-yyyy" };
        CultureInfo culture = CultureInfo.InvariantCulture;

        string x_dateStr = x.Deadline;
        DateTime x_date = DateTime.ParseExact(x_dateStr, formats, culture);

        string y_dateStr = y.Deadline;
        DateTime y_date = DateTime.ParseExact(y_dateStr, formats, culture);

        int res = DateTime.Compare(x_date, y_date);
        switch (res)
        {
            case (< 0):
                // x_date comes before y_date
                // Hence, x_date gets bonus points
                PT1_POINTS += 20 + priorityPoints[x.Priority];
                PT2_POINTS += priorityPoints[y.Priority];
                break;

            case 0:
                // x_date is same as y_date
                // Hence, no one gets bonus points
                PT1_POINTS += priorityPoints[x.Priority];
                PT2_POINTS += priorityPoints[y.Priority];
                break;

            case (> 0):
                // x_date comes after y_date
                // Hence, y_date gets bonus points
                PT1_POINTS += priorityPoints[x.Priority];
                PT2_POINTS += 20 + priorityPoints[y.Priority];
                break;
        }

        if (PT1_POINTS > PT2_POINTS)
        {
            // x has more priority
            // x should come before y
            return -1;
        }
        else if (PT2_POINTS > PT1_POINTS)
        {
            // y has more priority
            // y should come before x
            return 1;
        }
        else
        {
            // x and y have same priority
            // Order of x and y remains the same
            return 0;
        }
    }
}
