package com.example.dispersion.service;

import com.example.dispersion.model.Spill;
import com.example.dispersion.model.Simulation;
import com.example.dispersion.repository.SimulationRepository;
import com.example.dispersion.repository.SpillRepository;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.apache.commons.math3.util.FastMath;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.client.RestTemplate;

import java.util.HashMap;
import java.util.Map;

@Service
public class DispersionService {
    private final SpillRepository spillRepository;
    private final SimulationRepository simulationRepository;
    private final RestTemplate restTemplate = new RestTemplate();
    private final ObjectMapper objectMapper = new ObjectMapper();

    @Value("${tides.api.url}")
    private String noaaApiUrl;

    public DispersionService(SpillRepository spillRepository, SimulationRepository simulationRepository) {
        this.spillRepository = spillRepository;
        this.simulationRepository = simulationRepository;
    }

    @Transactional
    public Simulation simulateSpill(Spill spill) {
        // Save spill
        Spill savedSpill = spillRepository.save(spill);

        // Parse lat/lon from spillLocation (format: "lat,lon")
        String[] coords;
        double lat, lon;
        try {
            coords = spill.getSpillLocation().split(",");
            lat = Double.parseDouble(coords[0].trim());
            lon = Double.parseDouble(coords[1].trim());
        } catch (Exception e) {
            throw new IllegalArgumentException("Invalid spillLocation format: " + e.getMessage());
        }

        // Hardcoded station ID for demo (e.g., San Francisco); replace with logic to
        // find nearest station based on lat/lon
        String stationId = "9414290"; // Example: San Francisco

        // Fetch weather (meteorological data: wind, temp, etc.)
        String weatherUrl = noaaApiUrl + "?station=" + stationId
                + "&product=met&datum=MLLW&time_zone=lst_ldt&units=metric&format=json&date=today";
        String weatherData;
        try {
            weatherData = restTemplate.getForObject(weatherUrl, String.class);
        } catch (Exception e) {
            weatherData = "{\"error\": \"Weather fetch failed: " + e.getMessage() + "\"}";
        }

        // Fetch tides (water level data)
        String tidesUrl = noaaApiUrl + "?station=" + stationId
                + "&product=water_level&datum=MLLW&time_zone=lst_ldt&units=metric&format=json&date=today";
        String tidesData;
        try {
            tidesData = restTemplate.getForObject(tidesUrl, String.class);
        } catch (Exception e) {
            tidesData = "{\"error\": \"Tides fetch failed: " + e.getMessage() + "\"}";
        }

        // Basic fluid dynamics: Simple diffusion model (extend with real math,
        // incorporating tides/weather data)
        double diffusion = FastMath.sqrt(2 * spill.getVolume()); // Placeholder; integrate wind/current from API data
                                                                 // later

        // Create JSON model output safely
        Map<String, Object> modelMap = new HashMap<>();
        modelMap.put("diffusion", diffusion);
        modelMap.put("lat", lat);
        modelMap.put("lon", lon);
        String modelOutput;
        try {
            modelOutput = objectMapper.writeValueAsString(modelMap);
        } catch (JsonProcessingException e) {
            modelOutput = "{\"error\": \"JSON processing failed\"}";
        }

        // Save simulation
        Simulation simulation = new Simulation();
        simulation.setSpill(savedSpill);
        simulation.setDispersionModel(modelOutput);
        simulation.setWeatherData(weatherData);
        simulation.setTidalData(tidesData);
        return simulationRepository.save(simulation);
    }
}
