package com.example.recipe;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.client.RestTemplate;

@Controller
public class Dashboard {

	@GetMapping("/dashboard")
	public String dashboard(@RequestParam(name="pid", required=false, defaultValue="0") String pid, Model model) {
		
		final String uri = "http://localhost:55960/getPerscription/" + pid;
	    RestTemplate restTemplate = new RestTemplate();
	    String getresponse = restTemplate.getForObject(uri, String.class);      
 
		
		
		model.addAttribute("recept", getresponse);
		return "dashboard";
	}

}