package com.steve.controller;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import java.util.concurrent.atomic.AtomicLong;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.steve.model.Entry;
import com.steve.model.LottoWinners;
import com.steve.model.LottoEntries;

@RestController
@RequestMapping("/telephone")
public class TelephoneController {

    private static final String template = "Hello, %s!";
    private final AtomicLong counter = new AtomicLong();


    /*
    @RequestMapping(method = RequestMethod.POST)
    public Winner pickWinners(@RequestParam(value="name", defaultValue="World") String name) {
        return new Winner(counter.incrementAndGet(),
                            String.format(template, name));
    }
    */
    @RequestMapping(method = RequestMethod.POST)
    public LottoWinners pickWinners(@RequestBody LottoEntries payload) {
    	List<Entry> entries = new ArrayList<Entry>();
    	List<Entry> winners= new ArrayList<Entry>();
    	entries = payload.getEntries();
	int winnerCount = payload.getWinnerCount();
    	System.err.println("Entries" +entries.size() + " Winner Count" + winnerCount);
    	Random randomGenerator = new Random();
    	List<Integer>intList = new ArrayList<Integer>();
    	for (int idx = 1; idx <= winnerCount; ++idx){
    		int randomInt = 0;
    		if(randomInt == 0)
    			randomInt = showRandomInteger(1, entries.size(), randomGenerator);
    		while(intList.contains(new Integer(randomInt))){
    			randomInt = showRandomInteger(1, entries.size(), randomGenerator);
    		}
    	    System.err.println("Generated : " + randomInt);
    	    Entry w = entries.get(randomInt-1);
    	    winners.add(w);
    	    intList.add(new Integer(randomInt));
    	}
    	LottoWinners lottoWinners = new LottoWinners();
    	lottoWinners.setEntries(winners);

    	return lottoWinners;
    }
    /*
    @RequestMapping(method = RequestMethod.POST)
    public ResponseEntity<LottoEntries> runLotto(
            @RequestBody LottoEntries requestWrapper) {
    	List<Entry> entries = new ArrayList<Entry>();
        requestWrapper.getEntries().stream()
                .forEach(c -> c.setMiles(c.getMiles() + 100));

        // TODO: call persistence layer to update

        return new ResponseEntity<LottoEntries>(requestWrapper, HttpStatus.OK);
    }
    */
    private int showRandomInteger(int aStart, int aEnd, Random aRandom){
        if (aStart > aEnd) {
          throw new IllegalArgumentException("Start cannot exceed End.");
        }
        //get the range, casting to long to avoid overflow problems
        long range = (long)aEnd - (long)aStart + 1;
        // compute a fraction of the range, 0 <= frac < range
        long fraction = (long)(range * aRandom.nextDouble());
        int randomNumber =  (int)(fraction + aStart);  
        return(randomNumber);

      }
}
