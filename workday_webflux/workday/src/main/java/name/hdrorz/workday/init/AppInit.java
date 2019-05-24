package name.hdrorz.workday.init;

import name.hdrorz.workday.service.WorkDayService;
import org.springframework.boot.ApplicationArguments;
import org.springframework.boot.ApplicationRunner;
import org.springframework.core.annotation.Order;
import org.springframework.stereotype.Component;

import java.io.IOException;
import java.nio.charset.Charset;
import java.nio.file.*;

@Component
@Order(1)
public class AppInit implements ApplicationRunner {
    @Override
    public void run(ApplicationArguments args) throws Exception {
        Path path = Paths.get("config/nonworkdaylist.txt");
        if (System.getProperties().getProperty("os.name").toUpperCase().contains("WINDOWS")) {
            path = Paths.get(AppInit.class.getResource("/nonworkdaylist.txt").toURI());
        }
        try {
            WorkDayService.SetNonWorkdays(Files.lines(path, Charset.forName("UTF-8")));
        } catch (IOException ex) {
            ex.printStackTrace();
            throw ex;
        }
    }


}
