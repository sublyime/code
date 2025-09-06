package com.example.dispersion.repository;

import com.example.dispersion.model.Simulation;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SimulationRepository extends JpaRepository<Simulation, Long> {
}
