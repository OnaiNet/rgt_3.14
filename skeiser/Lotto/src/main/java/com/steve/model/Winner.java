package com.steve.model;

public class Winner {
	private final long id;
    private final String content;
    
    public Winner(long id, String content) {
        this.id = id;
        this.content = content;
    }
    
	public long getId() {
		return id;
	}
	public String getContent() {
		return content;
	}
}
