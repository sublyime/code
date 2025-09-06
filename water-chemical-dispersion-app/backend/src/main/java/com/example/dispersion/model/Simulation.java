package com.example.dispersion.model;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import org.hibernate.annotations.CreationTimestamp;
import org.hibernate.annotations.UpdateTimestamp;

import java.time.LocalDateTime;

@Entity
public class Simulation {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @ManyToOne
    @JoinColumn(name = "spill_id")
    private Spill spill;

    @Column(columnDefinition = "text")
    private String dispersionModel;

    @Column(columnDefinition = "text")
    private String weatherData;

    @Column(columnDefinition = "text")
    private String tidalData;

    @CreationTimestamp
    @Column(updatable = false)
    private LocalDateTime createdAt;

    @UpdateTimestamp
    private LocalDateTime updatedAt;

    // Getters and setters (add if missing)
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Spill getSpill() {
        return spill;
    }

    public void setSpill(Spill spill) {
        this.spill = spill;
    }

    public String getDispersionModel() {
        return dispersionModel;
    }

    public void setDispersionModel(String dispersionModel) {
        this.dispersionModel = dispersionModel;
    }

    public String getWeatherData() {
        return weatherData;
    }

    public void setWeatherData(String weatherData) {
        this.weatherData = weatherData;
    }

    public String getTidalData() {
        return tidalData;
    }

    public void setTidalData(String tidalData) {
        this.tidalData = tidalData;
    }

    public LocalDateTime getCreatedAt() {
        return createdAt;
    }

    public LocalDateTime getUpdatedAt() {
        return updatedAt;
    }
}
