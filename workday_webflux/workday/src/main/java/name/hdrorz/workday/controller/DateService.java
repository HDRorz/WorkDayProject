package name.hdrorz.workday.controller;

import name.hdrorz.workday.service.WorkDayService;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import reactor.core.publisher.Mono;

import java.time.LocalDateTime;

import static name.hdrorz.workday.service.WorkDayService.DayFormatter;


@RestController
@RequestMapping("/api/DateService")
public class DateService {

    @GetMapping("/CurrWorkDay")
    public Mono<String> GetCurrWorkDay() {
        return Mono.just(DayFormatter.format(WorkDayService.GetCurrWorkDay()));
    }

    @GetMapping("/LocalDateTime")
    public Mono<String> GetLocalDateTime() {
        return Mono.just(DayFormatter.format(WorkDayService.GetDateTime()));
    }

    @GetMapping("/WorkDay")
    public Mono<String> GetWorkDay(LocalDateTime date) {
        return Mono.just(DayFormatter.format(WorkDayService.GetWorkDay(date)));
    }

    @GetMapping("/LastWorkday")
    public Mono<String> GetLastWorkday(LocalDateTime date) {
        return Mono.just(DayFormatter.format(WorkDayService.GetLastWorkday(date)));
    }

    @GetMapping("/NextWorkday")
    public Mono<String> GetNextWorkday(LocalDateTime date) {
        return Mono.just(DayFormatter.format(WorkDayService.GetNextWorkday(date)));
    }

    @GetMapping("/PointWorkday")
    public Mono<String> GetPointWorkday(LocalDateTime date, int day) {
        return Mono.just(DayFormatter.format(WorkDayService.GetPointWorkday(date, day)));
    }
}
