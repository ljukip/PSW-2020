package com.example.recipe;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class PingPongEndpoint {
    GRPCClientService grpcClientService;

    @Autowired
    public PingPongEndpoint(GRPCClientService grpcClientService) {
        this.grpcClientService = grpcClientService;
    }

    @GetMapping("/request_medicine")
    public String ping(@RequestParam String medicine) {
        return medicine + " has been requested";
    }
}
