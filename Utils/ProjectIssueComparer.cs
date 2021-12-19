namespace IncludeTypeBackend.Utils;

/*
This Comparer derived class is used to compare two ProjectIssues(s) based 
on their Deadline and Priority.
1st preference => Deadline
2nd preference => Priority
*/
public class ProjectIssueComparer : Comparer<ProjectIssue>
{
    private readonly Dictionary<string, int> priorityPoints = new()
    {
        { "High", 10 },
        { "Medium", 5 },
        { "Low", 0 }
    };

    public override int Compare(ProjectIssue x, ProjectIssue y)
    {
        int PI1_POINTS = 0;
        int PI2_POINTS = 0;

        string format = "MMM-dd-yyyy";
        CultureInfo culture = CultureInfo.InvariantCulture;

        string x_dateStr = x.Deadline;
        DateTime x_date = DateTime.ParseExact(x_dateStr, format, culture);

        string y_dateStr = y.Deadline;
        DateTime y_date = DateTime.ParseExact(y_dateStr, format, culture);

        int res = DateTime.Compare(x_date, y_date);
        switch (res)
        {
            case (< 0):
                // x_date comes before y_date
                // Hence, x_date gets bonus points
                PI1_POINTS += 20 + priorityPoints[x.Priority];
                PI2_POINTS += priorityPoints[y.Priority];
                break;

            case 0:
                // x_date is same as y_date
                // Hence, no one gets bonus points
                PI1_POINTS += priorityPoints[x.Priority];
                PI2_POINTS += priorityPoints[y.Priority];
                break;

            case (> 0):
                // x_date comes after y_date
                // Hence, y_date gets bonus points
                PI1_POINTS += priorityPoints[x.Priority];
                PI2_POINTS += 20 + priorityPoints[y.Priority];
                break;
        }

        if (PI1_POINTS > PI2_POINTS)
        {
            // x has more priority
            // x should come before y
            return -1;
        }
        else if (PI2_POINTS > PI1_POINTS)
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
