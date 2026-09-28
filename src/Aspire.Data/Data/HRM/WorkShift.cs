namespace Aspire.Data;

[Attr(DomainFolder = "HRM", Abbrv = "SFT", List = true)]
public class WorkShift : BaseEntity
{
    public long? TimeZoneId { get; set; }

    public TimeOnly? Sun_From { get; set; }
    public TimeOnly? Sun_To { get; set; }
    public TimeOnly? Mon_From { get; set; }
    public TimeOnly? Mon_To { get; set; }
    public TimeOnly? Tue_From { get; set; }
    public TimeOnly? Tue_To { get; set; }
    public TimeOnly? Wed_From { get; set; }
    public TimeOnly? Wed_To { get; set; }
    public TimeOnly? Thr_From { get; set; }
    public TimeOnly? Thr_To { get; set; }
    public TimeOnly? Fri_From { get; set; }
    public TimeOnly? Fri_To { get; set; }
    public TimeOnly? Sat_From { get; set; }
    public TimeOnly? Sat_To { get; set; }
}

// Code-Player-Start-DTO //
public class WorkShiftDTO
{
    public long? TimeZoneId { get; set; }
    public string? TimeZoneName { get; set; }
    public TimeOnly? Sun_From { get; set; }
    public TimeOnly? Sun_To { get; set; }
    public TimeOnly? Mon_From { get; set; }
    public TimeOnly? Mon_To { get; set; }
    public TimeOnly? Tue_From { get; set; }
    public TimeOnly? Tue_To { get; set; }
    public TimeOnly? Wed_From { get; set; }
    public TimeOnly? Wed_To { get; set; }
    public TimeOnly? Thr_From { get; set; }
    public TimeOnly? Thr_To { get; set; }
    public TimeOnly? Fri_From { get; set; }
    public TimeOnly? Fri_To { get; set; }
    public TimeOnly? Sat_From { get; set; }
    public TimeOnly? Sat_To { get; set; }
    public long? EntityStatusId { get; set; }
    public string? EntityStatusName { get; set; }
    public string? EntityStatusColor { get; set; }
    public long? EntityTypeId { get; set; }
    public string? EntityTypeName { get; set; }
    public string? EntityTypeColor { get; set; }
    public long? DataConfidentialityId { get; set; }
    public string? DataConfidentialityName { get; set; }
    public string? Name { get; set; }
    public string? NameAr { get; set; }
    public string? Description { get; set; }
    public string? DescriptionAr { get; set; }
    public long? Id { get; set; }
    public string? No { get; set; }
    public long? TenantId { get; set; }
    public string? TenantName { get; set; }
    public bool? IsActive { get; set; }
    public string? Msg { get; set; }
    public string? ActionComment { get; set; }
    public string? PhotoUrl { get; set; }
    public long? CreatedByUsrId { get; set; }
    public string? CreatedByUsrName { get; set; }
    public string? CreatedByUsrPhotoUrl { get; set; }
    public long? UpdatedByUsrId { get; set; }
    public string? UpdatedByUsrName { get; set; }
    public string? UpdatedByUsrPhotoUrl { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
// Code-Player-End-DTO //

/*
There are various times and types of work shifts to choose from when creating employee work schedules. Keep in mind that many types of work shifts are flexible and adaptable to the needs of your business, so you can usually adjust them to suit a preferred schedule. Take a look at these common times and types of shift work:

1. First shift
First shifts refer to day shifts, which usually means that employees work during regular business hours and have evenings and nights off. First shifts range from about 8 a.m. to 5 p.m., so work shifts will usually fall in that range, depending on lunch breaks and operating hours.
Common first shift work roles include:
    - Administrative staff
    - Customer service representatives
    - Sales representatives
    - Human resource staff
    - Bank and clerical positions
    - Some businesses that offer these types of jobs might have hours that extend beyond regular working hours. In those cases, employees might sometimes work the second shift instead of the first shift.

2. Second shift
Second shifts are midday shifts that often overlap with first and third shifts. Employees on these shifts have mornings and nights off from work, generally starting work anytime between 11 a.m. and 3 p.m. and finishing before midnight. One employee working the second shift might work from 11 a.m. to 7 p.m., while another may work from 3 p.m. to 12 a.m.. While these shifts are occasionally administrative, customer service or other roles, typical second shift jobs include:

    - Nurse and healthcare jobs
    - Hospitality and food service positions
    - Call center agents
    - Quality control jobs
    - Retail jobs

3. Third shift
Third shifts are also often referred to as graveyard or night shifts. Third shift employees sometimes overlap with second shift employees. 
They start work at around 10 p.m. and may finish work anywhere from 6 a.m. to 8 a.m.
Third shift position examples include:
    - Emergency responders, such as firefighters, police and ambulance nurses
    - Healthcare staff, such as doctors, nurses, lab workers and administrative support
    - Food service and hospitality positions, such as bartenders and hotel staff
    - Custodian jobs
    - Travel industry jobs, such as pilots and drivers

Graveyard shifts can be physically and mentally wearing on employees since it�s difficult to maintain a proper sleep schedule and work-life balance. 
Some employers offer additional pay for employees who work third shifts.

4. Standard shift
A standard shift refers to a regular business schedule where employees work Monday through Friday from 9 a.m. to 5 p.m., often referred to as a 9-to-5 job. This is a common schedule for businesses and offices that operate during regular business hours. Employees often work together and enjoy a reliable schedule and weekends off.

5. Fixed shift
Fixed shifts are similar to standard shifts in that schedules have consistent hours and days, but they may vary from regular business hours. Instead of Monday to Friday from 9 a.m. to 5 p.m., an employee might work Tuesday through Saturday from 7 a.m. to 3 p.m. This is a more flexible schedule style that still makes it easy for employers and employees to plan long-term.

6. Rotating shift
Rotating shifts mean that an employee rotates between first, second and third shifts. They might rotate on a daily, weekly or monthly basis, depending on your schedule policy. This is an effective schedule strategy for positions, such as healthcare workers and emergency responders, but may be more of a struggle for other employees who prefer a more consistent work shift.

7. Flex shift
Flex or flexible shifts are a type of alternative schedule in which an employer requires employees to work a certain amount of hours within a schedule range. For example, an employer might require an employee to work at least 20 of their 40 hours in the office, with the office being open from 8 a.m. to 6 p.m., Monday to Friday. The employee can then choose to complete their hours in the office or wherever they prefer. Employees enjoy the flexibility of this schedule, but it may be inconvenient when employers require more office collaboration.

8. Compressed shift
Compressed shifts are another flexible alternative to a conventional schedule. Instead of working five days per week and eight hours per day, employees might work four days of 10 hours. Keep in mind, employees who are paid hourly might qualify for overtime on this schedule.

9. Split shift
Employees with split shifts complete two shorter shifts each day with a significant period of time between. This is a common shift in businesses that experience multiple peak periods throughout the day, such as restaurants. A server or cook might work several hours over lunch and return for dinner service.

10. On-Call shift
On-call shifts are generally in addition to regular working hours. On-call shifts might be subject to different compensation, according to your policy, since it may require employees to work odd or last-minute hours. On-call shifts are common with urgent services, such as nurses, technicians and emergency mechanics.

11. Weekday or weekend shift
Weekend shifts are common for hospitality, retail and food service businesses. These shifts often work well for employees who have other commitments, such as school, family or another job. Some employers offer additional compensation to employees who work weekends, particularly if they opt to perform weekend work over weekday work.

*/