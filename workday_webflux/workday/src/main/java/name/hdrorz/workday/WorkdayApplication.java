package name.hdrorz.workday;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
public class WorkdayApplication {

    public static void main(String[] args) {
        SpringApplication.run(WorkdayApplication.class, args);
    }

    @Bean
    public ServerFilter serverFilter() {
        return new ServerFilter();
    }
}
