package name.hdrorz.workday.service;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.Set;
import java.util.TreeSet;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class WorkDayService {

    public final static DateTimeFormatter DayFormatter = DateTimeFormatter.ofPattern("yyyy-MM-dd");

    private final static LocalTime LatestTime = LocalTime.of(15, 0, 1);

    private static WorkDayService ourInstance = new WorkDayService();

    public static WorkDayService getInstance() {
        return ourInstance;
    }

    private static Set<LocalDate> NonWorkdays = new TreeSet<LocalDate>();

    private WorkDayService() {
    }

    public static void SetNonWorkdays(Stream<String> values) {
        NonWorkdays = values.filter(str -> !str.isEmpty()).map(str -> LocalDate.from(DayFormatter.parse(str.trim()))).collect(Collectors.toCollection(TreeSet::new));
    }

    public static LocalDateTime GetDateTime() {
        return LocalDateTime.now();
    }

    public static LocalDate GetWorkDay(LocalDateTime date) {
        return GetWorkday(date, 0);
    }

    public static LocalDate GetCurrWorkDay() {
        return GetWorkday(LocalDateTime.now(), 0);
    }

    public static LocalDate GetLastWorkday(LocalDateTime date) {
        return GetWorkday(date, -1);
    }

    public static LocalDate GetNextWorkday(LocalDateTime date) {
        return GetWorkday(date, 1);
    }

    public static LocalDate GetPointWorkday(LocalDateTime datetime, int day) {
        return GetWorkday(datetime, day);
    }

    private static LocalDate GetWorkday(LocalDateTime date, int days) {
        int pos = 1;    // 1代表days为非负数，-1代表days为负数
        if (days < 0) {
            pos = -1;
            days *= -1;
        }

        LocalDate curWorkday = date.toLocalDate();
        // 如果时间已经超过当天的15:00:01，则作为下一工作日算
        if (date.toLocalTime().isAfter(LatestTime)) {
            curWorkday = curWorkday.minusDays(-1);
        }

        // 获取当前工作日
        // 判断当天是不是工作日
        boolean notWorkday = NonWorkdays.contains(curWorkday);
        while (notWorkday) {
            curWorkday = curWorkday.minusDays(-1); // 向后推一天
            notWorkday = NonWorkdays.contains(curWorkday);
        }

        // 计算工作日
        for (int i = 1; i <= days; i++) {
            do {
                curWorkday = curWorkday.minusDays(-pos); // 向前或向后推一天

                notWorkday = NonWorkdays.contains(curWorkday);
            } while (notWorkday);
        }

        return curWorkday;
    }

}
