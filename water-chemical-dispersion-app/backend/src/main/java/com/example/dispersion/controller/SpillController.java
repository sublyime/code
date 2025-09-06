package com.example.dispersion.controller;

import com.example.dispersion.model.Spill;
import com.example.dispersion.model.Simulation;
import com.example.dispersion.service.DispersionService;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class SpillController {
    private final DispersionService dispersionService;

    public SpillController(DispersionService dispersionService) {
        this.dispersionService = dispersionService;
    }

    @PostMapping("/api/simulate")
    public Simulation simulate(@RequestBody Spill spill) {
        return dispersionService.simulateSpill(spill);
    }
}
