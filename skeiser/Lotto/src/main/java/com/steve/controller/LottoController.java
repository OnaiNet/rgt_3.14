package com.steve.controller;

import java.util.concurrent.atomic.AtomicLong;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.steve.model.Winner;

@RestController
public class LottoController {

    private static final String template = "Hello, %s!";
    private final AtomicLong counter = new AtomicLong();

    @RequestMapping(method = RequestMethod.GET)
    public Winner winner(@RequestParam(value="name", defaultValue="World") String name) {
        return new Winner(counter.incrementAndGet(),
                            String.format(template, name));
    }
    @RequestMapping(method = RequestMethod.POST)
    public Winner pickWinners(@RequestParam(value="name", defaultValue="World") String name) {
        return new Winner(counter.incrementAndGet(),
                            String.format(template, name));
    }
}
