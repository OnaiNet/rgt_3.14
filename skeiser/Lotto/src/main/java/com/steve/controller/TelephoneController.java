package com.steve.controller;

import java.io.IOException;
import java.util.concurrent.TimeoutException;

import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

import com.steve.model.Message;
import com.steve.mq.RabbitMQSend;

@RestController
@RequestMapping("/telephone")
public class TelephoneController {


    @RequestMapping(method = RequestMethod.POST)
    public String setMessage(@RequestBody Message payload) {
    	String message = payload.getMessage();
    	System.err.println("Message " +message);
    	RabbitMQSend sender = new  RabbitMQSend();
    	try {
			sender.send("\"message\":" +"\"" + message + "\"");
		} catch (TimeoutException | IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
    	return message;
    }
}
