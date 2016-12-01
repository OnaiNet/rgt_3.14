package com.steve.model;

import java.util.List;

public class LottoEntries {
	private int lottoEvent;
	private List<Entry> entries;
	public int getLottoEvent() {
		return lottoEvent;
	}
	public void setLottoEvent(int lottoEvent) {
		this.lottoEvent = lottoEvent;
	}
	public List<Entry> getEntries() {
		return entries;
	}
	public void setEntries(List<Entry> entries) {
		this.entries = entries;
	}
}
