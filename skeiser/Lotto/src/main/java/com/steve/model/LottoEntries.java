package com.steve.model;

import java.util.List;

public class LottoEntries {
	private int lottoEvent;
	private int winnerCount;
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
	public int getWinnerCount(){
		return(winnerCount);
	}
	public void setWinnerCount(int winnerCount){
 		this.winnerCount = winnerCount;
	}
}
