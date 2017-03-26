package com.steve.mq;

import java.io.IOException;
import java.util.concurrent.TimeoutException;

import com.rabbitmq.client.ConnectionFactory;
import com.rabbitmq.client.Connection;
import com.rabbitmq.client.Channel;
import com.steve.model.Message;

public class RabbitMQSend {

	public void send(String message) throws TimeoutException, IOException {

		ConnectionFactory factory = new ConnectionFactory();
		factory.setHost("localhost");
		factory.setUsername("rgt_314");
		factory.setPassword("chg@Test");
		Connection connection = factory.newConnection();
		Channel channel = connection.createChannel();

		channel.queueDeclare("rgtQueue", false, false, false, null);
			channel.basicPublish("", "rgtQueue", null, message.getBytes());
			//System.out.println(" [x] Sent '" + message + "'");
		channel.close();
		connection.close();

	}

}

